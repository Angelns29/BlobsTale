using UnityEngine;

namespace TopDownCharacter2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownKnockBack : MonoBehaviour
    {
        [SerializeField] private float knockBackPower;
        [SerializeField] private float timeBetweenKnockBack = .1f;

        private Rigidbody2D _rb;
        private float _timeSinceLastKnockBack = float.MaxValue;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_timeSinceLastKnockBack < timeBetweenKnockBack)
            {
                _timeSinceLastKnockBack += Time.deltaTime;
            }
        }

        public void ApplyKnockBack(Transform other)
        {
            if (_timeSinceLastKnockBack < timeBetweenKnockBack)
            {
                return;
            }

            Vector2 direction = -(other.position - transform.position).normalized;

            _rb.AddForce(direction * (knockBackPower * 100f), ForceMode2D.Impulse);
        }
    }
}