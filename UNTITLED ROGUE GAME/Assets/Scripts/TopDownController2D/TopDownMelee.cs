using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Attacks.Melee;
using TopDownCharacter2D.Controllers;
using UnityEngine;

namespace TopDownCharacter2D
{
    [RequireComponent(typeof(TopDownCharacterController))]
    public class TopDownMelee : MonoBehaviour
    {
        [SerializeField] private GameObject attackObject;

        [SerializeField] [Tooltip("The pivot point of the attack")]
        private Transform attackPivot;

        private Vector2 _attackDirection;


        private TopDownCharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<TopDownCharacterController>();
        }

        private void Start()
        {
            _controller.OnAttackEvent.AddListener(Attack);
            _controller.LookEvent.AddListener(Rotate);
        }

        private void Attack(AttackConfig config)
        {
            if (!(config is MeleeAttackConfig))
            {
                return;
            }

            InstantiateAttack((MeleeAttackConfig) config);
        }

        private void Rotate(Vector2 rotation)
        {
            _attackDirection = rotation;
        }

        private void InstantiateAttack(MeleeAttackConfig attackConfig)
        {
            attackPivot.localRotation = Quaternion.identity;
            GameObject obj = Instantiate(attackObject, attackPivot.position,
                Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, _attackDirection)), attackPivot);
            MeleeAttackController attackController = obj.GetComponent<MeleeAttackController>();
            attackController.InitializeAttack(attackConfig);
        }
    }
}