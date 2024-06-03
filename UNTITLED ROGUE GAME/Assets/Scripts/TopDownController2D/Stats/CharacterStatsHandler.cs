using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Attacks.Melee;
using TopDownCharacter2D.Attacks.Range;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownCharacter2D.Stats
{
    public class CharacterStatsHandler : MonoBehaviour, IDamageable
    {
        [SerializeField] [Tooltip("The default stats of this character")]
        public CharacterStats baseStats;

        public readonly ObservableCollection<CharacterStats>
            statsModifiers = new ObservableCollection<CharacterStats>();

        public CharacterStats CurrentStats { get; private set; }

        public float hp;
        public float maxhp;
        public Slider healthSlider;
        public Slider inventoryHealth;
        public SpriteRenderer weaponSprite;
        public int money;

        private void Awake()
        {
            UpdateCharacterStats(null, null);
            statsModifiers.CollectionChanged += UpdateCharacterStats;
            money = GameManager.Instance.playerMoney;
        }
        private void Start()
        {
            //inventoryHealth.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }
        private void FixedUpdate()
        {
            Debug.Log(money);
            CheckHealth();
            UpdateCharacterStats(null, null);
            statsModifiers.CollectionChanged += UpdateCharacterStats;
            ValueChangeCheck();
        }
        private void ValueChangeCheck()
        {
            inventoryHealth.value = healthSlider.value;
        }
        private void UpdateCharacterStats(object sender, NotifyCollectionChangedEventArgs e)
        {
            AttackConfig config = null;
            if (baseStats.attackConfig != null)
            {
                config = Instantiate(baseStats.attackConfig);
                weaponSprite.sprite = config.itemImage;
            }

            CurrentStats = new CharacterStats {attackConfig = config};
            UpdateStats((a, b) => b, baseStats);
            if (CurrentStats.attackConfig != null)
            {
                CurrentStats.attackConfig.target = baseStats.attackConfig.target;
            }

            foreach (CharacterStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
            {
                if (modifier.statsChangeType == StatsChangeType.Override)
                {
                    UpdateStats((o, o1) => o1, modifier);
                }
                else if (modifier.statsChangeType == StatsChangeType.Add)
                {
                    UpdateStats((o, o1) => o + o1, modifier);
                }
                else if (modifier.statsChangeType == StatsChangeType.Multiply)
                {
                    UpdateStats((o, o1) => o * o1, modifier);
                }
            }
            

            LimitAllStats();
        }

        private void UpdateStats(Func<float, float, float> operation, CharacterStats newModifier)
        {
            CurrentStats.maxHealth = (int) operation(CurrentStats.maxHealth, newModifier.maxHealth);
            CurrentStats.speed = operation(CurrentStats.speed, newModifier.speed);
            if (newModifier.attackConfig == null || CurrentStats.attackConfig == null)
            {
                return;
            }

            CurrentStats.attackConfig.delay =
                operation(CurrentStats.attackConfig.delay, newModifier.attackConfig.delay);
            CurrentStats.attackConfig.power =
                operation(CurrentStats.attackConfig.power, newModifier.attackConfig.power);
            CurrentStats.attackConfig.size = operation(CurrentStats.attackConfig.size, newModifier.attackConfig.size);
            CurrentStats.attackConfig.speed =
                operation(CurrentStats.attackConfig.speed, newModifier.attackConfig.speed);

            if (CurrentStats.attackConfig.GetType() != newModifier.attackConfig.GetType())
            {
                return;
            }

            switch (CurrentStats.attackConfig)
            {
                case RangedAttackConfig _:
                    ApplyRangedStats(operation,
                        newModifier); // This method is only called if the character uses a ranged weapon
                    break;
                case MeleeAttackConfig _:
                    ApplyMeleeStats(operation, newModifier);
                    break;
            }
        }

        private void LimitAllStats()
        {
            if (CurrentStats == null || CurrentStats.attackConfig == null)
            {
                return;
            }

            CurrentStats.attackConfig.delay =
                CurrentStats.attackConfig.delay < MinAttackDelay ? MinAttackDelay : CurrentStats.attackConfig.delay;
            CurrentStats.attackConfig.power = CurrentStats.attackConfig.power < MinAttackPower
                ? MinAttackPower
                : CurrentStats.attackConfig.power;
            CurrentStats.attackConfig.size = CurrentStats.attackConfig.size < MinAttackSize
                ? MinAttackSize
                : CurrentStats.attackConfig.size;
            CurrentStats.attackConfig.speed = CurrentStats.attackConfig.speed < MinAttackSpeed
                ? MinAttackSpeed
                : CurrentStats.attackConfig.speed;
            CurrentStats.speed = CurrentStats.speed < MinSpeed ? MinSpeed : CurrentStats.speed;
            CurrentStats.maxHealth = CurrentStats.maxHealth < MinMaxHealth ? MinMaxHealth : CurrentStats.maxHealth;
        }

        private void ApplyRangedStats(Func<float, float, float> operation, CharacterStats newModifier)
        {
            RangedAttackConfig currentRangedAttacks = (RangedAttackConfig) CurrentStats.attackConfig;

            if (!(newModifier.attackConfig is RangedAttackConfig))
            {
                return;
            }

            RangedAttackConfig rangedAttacksModifier = (RangedAttackConfig) newModifier.attackConfig;
            currentRangedAttacks.multipleProjectilesAngle =
                operation(currentRangedAttacks.multipleProjectilesAngle, rangedAttacksModifier.multipleProjectilesAngle);
            currentRangedAttacks.spread = operation(currentRangedAttacks.spread, rangedAttacksModifier.spread);
            currentRangedAttacks.duration = operation(currentRangedAttacks.duration, rangedAttacksModifier.duration);
            currentRangedAttacks.numberOfProjectilesPerShot = Mathf.CeilToInt(operation(currentRangedAttacks.numberOfProjectilesPerShot,
                rangedAttacksModifier.numberOfProjectilesPerShot));
            currentRangedAttacks.projectileColor = new Color(
                operation(currentRangedAttacks.projectileColor.r, rangedAttacksModifier.projectileColor.r),
                operation(currentRangedAttacks.projectileColor.g, rangedAttacksModifier.projectileColor.g),
                operation(currentRangedAttacks.projectileColor.b, rangedAttacksModifier.projectileColor.b),
                operation(currentRangedAttacks.projectileColor.a, rangedAttacksModifier.projectileColor.a));
        }
        
        private void ApplyMeleeStats(Func<float, float, float> operation, CharacterStats newModifier)
        {
            MeleeAttackConfig currentMeleeAttacks= (MeleeAttackConfig) CurrentStats.attackConfig;

            if (!(newModifier.attackConfig is MeleeAttackConfig))
            {
                return;
            }
            
            // NOTE: In case of a power up we ignore the curves
            
            MeleeAttackConfig meleeAttacksModifier = (MeleeAttackConfig) newModifier.attackConfig;
            currentMeleeAttacks.swingAngle =
                operation(currentMeleeAttacks.swingAngle, meleeAttacksModifier.swingAngle);
            
            currentMeleeAttacks.thrustDistance = 
                operation(currentMeleeAttacks.thrustDistance, meleeAttacksModifier.thrustDistance);
        }

        public void OnHurt(float damage)
        {
            hp -= damage;
            CheckHealth();
            healthSlider.value = hp / maxhp;
            var animator = healthSlider.GetComponent<Animator>();
            animator.SetTrigger("PlayEffect");
        }
        public void CheckHealth()
        {
            if (hp <= 0)
            {
                GameManager.Instance.SetGameOver();
            }
        }
        public void SetPosition()
        {
            transform.position = new Vector2(0, 0);
        }
        #region Stats limits

        private const float MinAttackDelay = 0.03f;
        private const float MinAttackPower = 0.5f;
        private const float MinAttackSize = 0.4f;
        private const float MinAttackSpeed = .1f;

        private const float MinSpeed = 1f;
        private const int MinMaxHealth = 1;

        #endregion
    }
}