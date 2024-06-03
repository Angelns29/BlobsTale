using TopDownCharacter2D.Health;
using UnityEngine;

namespace TopDownCharacter2D.Attacks.Melee
{
    public class MeleeAttackController : MonoBehaviour
    {
        private MeleeAttackConfig attackConfig;
        private Vector3 _endPosition;
        private Quaternion _endRotation;
        private bool _isReady;
        private Vector3 _startPosition;

        private Quaternion _startRotation;
        private float _timeActive;

        private Transform _transform;

        private void Update()
        {
            if (!_isReady)
            {
                return;
            }

            _timeActive += Time.deltaTime;

            //  Destroy the attack after the time of the attack speed
            if (_timeActive > attackConfig.speed)
            {
                DestroyAttack();
            }

            // Apply the swing and thrust transformations
            _transform.localRotation = Quaternion.Lerp(_startRotation, _endRotation,
                attackConfig.swingCurve.Evaluate(_timeActive / attackConfig.speed));
            _transform.localPosition = _transform.localRotation * Vector3.Lerp(_startPosition, _endPosition,
                attackConfig.thrustCurve.Evaluate(_timeActive / attackConfig.speed));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (attackConfig.target.value == (attackConfig.target.value | (1 << other.gameObject.layer)))
            {
                HealthSystem health = other.gameObject.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.ChangeHealth(-attackConfig.power);
                    TopDownKnockBack knockBack = other.gameObject.GetComponent<TopDownKnockBack>();
                    if (knockBack != null)
                    {
                        knockBack.ApplyKnockBack(transform);
                    }
                }
            }
        }

        public void InitializeAttack(MeleeAttackConfig attackConfig)
        {
            _transform = transform;
            this.attackConfig = attackConfig;

            ComputeSwingRotations();
            ComputeThrustPositions();
            ScaleAttack();

            _transform.localRotation = _startRotation;
            _transform.localPosition = _startPosition;

            _timeActive = 0f;
            _isReady = true;
        }

        private void ComputeSwingRotations()
        {
            Quaternion rotation = _transform.rotation;
            _startRotation = rotation * Quaternion.Euler(0, 0, -attackConfig.swingAngle);
            _endRotation = rotation * Quaternion.Euler(0, 0, attackConfig.swingAngle);
        }

        private void ComputeThrustPositions()
        {
            Vector3 position = _transform.localPosition;
            _startPosition = position;
            _endPosition = position + new Vector3(attackConfig.thrustDistance, 0, 0);
        }

        private void ScaleAttack()
        {
            transform.localScale = new Vector3(attackConfig.size, attackConfig.size, attackConfig.size);
        }

        private void DestroyAttack()
        {
            Destroy(gameObject);
        }
    }
}