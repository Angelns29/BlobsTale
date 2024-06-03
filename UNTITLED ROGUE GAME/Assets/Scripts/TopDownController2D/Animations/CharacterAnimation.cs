using TopDownCharacter2D.Health;
using UnityEngine;

namespace TopDownController2D.Scripts.TopDownCharacter2D.Animations
{
    public class CharacterAnimation : Animations
    {
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsHurt = Animator.StringToHash("IsHurt");
        
        [SerializeField] private bool createDustOnWalk = true;
        [SerializeField] private ParticleSystem dustParticleSystem;
        
        private HealthSystem _healthSystem;

        private Vector2 velocity;


        protected override void Awake()
        {
            base.Awake();
            _healthSystem = GetComponent<HealthSystem>();
        }

        protected void Start()
        {
            controller.OnAttackEvent.AddListener(_ => Attacking());
            controller.OnMoveEvent.AddListener(Move);
            velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            if (_healthSystem != null)
            {
                _healthSystem.OnDamage.AddListener(Hurt);
                _healthSystem.OnInvincibilityEnd.AddListener(InvincibilityEnd);
            }
        }


        private void Move(Vector2 movementDirection)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
            CreateDustParticles();
            animator.SetBool("isMoving", true);
        }

        private void Attacking()
        {
            animator.SetTrigger(Attack);
        }

        private void Hurt()
        {
            animator.SetBool(IsHurt, true);
        }

        public void InvincibilityEnd()
        {
            animator.SetBool(IsHurt, false);
        }

        public void CreateDustParticles()
        {
            if (createDustOnWalk)
            {
                dustParticleSystem.Stop();
                dustParticleSystem.Play();
            }
        }
    }
}