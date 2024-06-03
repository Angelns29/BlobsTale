using System.Collections.Generic;
using System.Linq;
using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Health;
using TopDownCharacter2D.Items;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownCharacter2D.FX
{
    public class TopDownFx : MonoBehaviour
    {
        [SerializeField] private List<Effect> effects;
        private TopDownCharacterController _controller;

        private HealthSystem _healthSystem;
        private PickupItem _pickup;

        private void Awake()
        {
            _pickup = GetComponent<PickupItem>();
            _healthSystem = GetComponent<HealthSystem>();
            _controller = GetComponent<TopDownCharacterController>();
        }

        private void Start()
        {
            if (_pickup != null)
            {
                TryAddListener(_pickup.OnPickup, TriggerEvents.Pickup);
            }

            if (_healthSystem != null)
            {
                TryAddListener(_healthSystem.OnDamage, TriggerEvents.Damage);
                TryAddListener(_healthSystem.OnDeath, TriggerEvents.Death);
                TryAddListener(_healthSystem.OnHeal, TriggerEvents.Heal);
                TryAddListener(_healthSystem.OnInvincibilityEnd, TriggerEvents.InvincibilityEnd);
            }

            if (_controller != null)
            {
                TryAddListener(_controller.OnAttackEvent, TriggerEvents.Attack);
                TryAddListener(_controller.LookEvent, TriggerEvents.Look);
                TryAddListener(_controller.OnMoveEvent, TriggerEvents.Walk);
            }
        }

        private void TryAddListener(object evt, TriggerEvents evtTrigger)
        {
            if (effects.All(effect => effect.triggerEvent != evtTrigger))
            {
                return;
            }

            switch (evtTrigger)
            {
                case TriggerEvents.Attack:
                    ((UnityEvent<AttackConfig>) evt).AddListener(delegate(AttackConfig arg0)
                    {
                        TriggerEffects(evtTrigger, arg0);
                    });
                    break;
                case TriggerEvents.Look:
                case TriggerEvents.Walk:
                    ((UnityEvent<Vector2>) evt).AddListener(delegate { TriggerEffects(evtTrigger); });
                    break;
                default:
                    ((UnityEvent) evt).AddListener(delegate { TriggerEffects(evtTrigger); });
                    break;
            }
        }

        private void TriggerEffects(TriggerEvents triggerEvent, AttackConfig attackConfig)
        {
            foreach (Effect effect in effects)
            {
                if (effect.triggerEvent != triggerEvent)
                {
                    continue;
                }

                if (effect.particleSystem != null)
                {
                    CreateParticles(effect.particleSystem);
                }

                if (effect.soundEffect != null)
                {
                    StartSoundEffect(effect.soundEffect);
                }
            }
        }

        private void TriggerEffects(TriggerEvents triggerEvent)
        {
            foreach (Effect effect in effects)
            {
                if (effect.triggerEvent != triggerEvent)
                {
                    continue;
                }

                if (effect.particleSystem != null)
                {
                    CreateParticles(effect.particleSystem);
                }

                if (effect.soundEffect != null)
                {
                    StartSoundEffect(effect.soundEffect);
                }
            }
        }

        private static void CreateParticles(ParticleSystem ps)
        {
            ps.Stop();
            ps.Play();
        }

        private void StartSoundEffect(AudioClip clip)
        {
            SoundManager.PlaySoundEffect(clip);
        }
    }
}