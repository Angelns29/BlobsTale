using TopDownCharacter2D.Health;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TopDownCharacter2D.UI
{
    public class EntityHealthGaugeUpdater : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private GameObject entityObject;

        [SerializeField] private UnityEvent onHealthUpdate;

        private HealthSystem _entityHealth;

        private void Awake()
        {
            _entityHealth = entityObject.GetComponent<HealthSystem>();
        }

        private void Start()
        {
            _entityHealth.OnDamage.AddListener(UpdateHealth);
            _entityHealth.OnHeal.AddListener(UpdateHealth);
        }

        private void UpdateHealth()
        {
            healthSlider.value = _entityHealth.CurrentHealth / _entityHealth.MaxHealth;
            onHealthUpdate.Invoke();
        }
    }
}