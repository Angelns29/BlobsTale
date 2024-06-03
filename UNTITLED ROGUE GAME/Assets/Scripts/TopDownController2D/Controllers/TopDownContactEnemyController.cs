using UnityEngine;

namespace TopDownCharacter2D.Controllers
{
    public class TopDownContactEnemyController : TopDownEnemyController
    {
        [SerializeField] [Range(0f, 100f)] private float followRange;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Vector2 direction = Vector2.zero;
            if (DistanceToTarget() < followRange)
            {
                direction = DirectionToTarget();
            }

            OnMoveEvent.Invoke(direction);
        }
    }
}