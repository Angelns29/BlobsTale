using UnityEngine;

namespace TopDownCharacter2D.Attacks.Range
{

    [CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/Range")]
    public class RangedAttackConfig : AttackConfig
    {
        [Tooltip("The duration of a projectile before disappearing")] 
        public float duration;

        [Tooltip("The maximum angle variation of the projectile")]
        public float spread;

        [Tooltip("The number of projectile shot per attack")]
        public int numberOfProjectilesPerShot;

        [Tooltip("The angle between each projectile shot (ignored when there are only one)")]
        public float multipleProjectilesAngle;

        [Tooltip("The color of the projectile's sprite")]
        public Color projectileColor;
    }
}