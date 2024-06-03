using System.Linq;
using UnityEngine;

namespace TopDownCharacter2D.Controllers
{
    public abstract class TopDownEnemyController : TopDownCharacterController
    {
        [Tooltip("The tag of the target of this enemy")] [SerializeField]
        private string targetTag = "Player";

        protected string TargetTag => targetTag;
        protected Transform ClosestTarget { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            ClosestTarget = FindClosestTarget();
        }

        protected virtual void FixedUpdate()
        {
            ClosestTarget = FindClosestTarget();
        }

        private Transform FindClosestTarget()
        {
            return GameObject.FindGameObjectsWithTag(targetTag)
                .OrderBy(o => Vector3.Distance(o.transform.position, transform.position))
                .First().transform;
        }

        protected float DistanceToTarget()
        {
            return Vector3.Distance(transform.position, ClosestTarget.transform.position);
        }

        protected Vector2 DirectionToTarget()
        {
            return (ClosestTarget.transform.position - transform.position).normalized;
        }
    }
}