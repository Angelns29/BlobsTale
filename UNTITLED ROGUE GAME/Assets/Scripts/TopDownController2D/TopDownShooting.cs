using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Attacks.Range;
using TopDownCharacter2D.Controllers;
using UnityEngine;

namespace TopDownCharacter2D
{
    [RequireComponent(typeof(TopDownCharacterController))]
    public class TopDownShooting : MonoBehaviour
    {
        [Header("Parameters")] [SerializeField] [Range(0.0f, 2f)] [Tooltip("The strength of the recoil after a shot")]
        private float recoilStrength = 1f;

        [SerializeField] [Tooltip("Define if the recoil is proportional to the size of the projectile")]
        private bool projectileSizeModifyRecoil = true;


        [SerializeField] [Tooltip("The start point of the projectiles")]
        private Transform projectileSpawnPosition;

        private Vector2 _aimDirection = Vector2.right;
        private ProjectileManager projectileManager;
        private TopDownCharacterController _controller;
        private Rigidbody2D _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _controller = GetComponent<TopDownCharacterController>();
        }

        private void Start()
        {
            projectileManager = ProjectileManager.instance;
            _controller.OnAttackEvent.AddListener(OnShoot);
            _controller.LookEvent.AddListener(OnAim);
        }

        public void OnAim(Vector2 newAimDirection)
        {
            _aimDirection = newAimDirection;
        }

        public void OnShoot(AttackConfig attackConfig)
        {
            RangedAttackConfig rangedAttackConfig = (RangedAttackConfig) attackConfig;
            float projectilesAngleSpace = rangedAttackConfig.multipleProjectilesAngle;
            float minAngle = -(rangedAttackConfig.numberOfProjectilesPerShot / 2f) * projectilesAngleSpace +
                             0.5f * rangedAttackConfig.multipleProjectilesAngle;

            for (int i = 0; i < rangedAttackConfig.numberOfProjectilesPerShot; i++)
            {
                float angle = minAngle + projectilesAngleSpace * i;
                float randomSpread = Random.Range(-rangedAttackConfig.spread, rangedAttackConfig.spread);
                angle += randomSpread;
                CreateProjectile(rangedAttackConfig, angle);
            }

            if (_rb != null)
            {
                AddRecoil(rangedAttackConfig);
            }
        }

        private void CreateProjectile(RangedAttackConfig rangedAttackConfig, float angle)
        {
            projectileManager.ShootBullet(projectileSpawnPosition.position, RotateVector2(_aimDirection.normalized, angle),
                rangedAttackConfig);
        }

        private static Vector2 RotateVector2(Vector2 v, float degrees)
        {
            return Quaternion.Euler(0, 0, degrees) * v;
        }

        private void AddRecoil(RangedAttackConfig rangedAttackConfig)
        {
            if (projectileSizeModifyRecoil)
            {
                _rb.AddForce(-(_aimDirection * (rangedAttackConfig.size * recoilStrength * 100f)), ForceMode2D.Impulse);
            }
            else
            {
                _rb.AddForce(-(_aimDirection * (recoilStrength * 100f)), ForceMode2D.Impulse);
            }
        }
    }
}