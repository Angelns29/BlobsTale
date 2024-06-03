using UnityEngine;

namespace TopDownCharacter2D.Attacks
{

    public abstract class AttackConfig : ItemSO
    {
        [Tooltip("The scale of the attack")]
        public float size;

        [Tooltip("The time between two attacks")]
        public float delay;
        
        [Tooltip("The damage dealt by an attack")]
        public float power = 0;
        
        [Tooltip("The speed of the attack")]
        public float speed;
        
        [Tooltip("The possible targets for this attack")]
        public LayerMask target;


        private void Start()
        {
            if (power == 0)
            {
                power = Random.Range(Rarity * 1 + 1, Rarity * 5 + 5);
            }
        }
    }
}