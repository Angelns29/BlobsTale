using UnityEngine;

namespace TopDownCharacter2D.Attacks.Melee
{

    [CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/Melee")]
    public class MeleeAttackConfig : AttackConfig
    {
        [Tooltip("The angle of the horizontal swing of the attack")]
        public float swingAngle;

        [Tooltip("The thrust distance of the attack")]
        public float thrustDistance;

        [Tooltip("The curve used to control the horizontal swing of the sword")]
        public AnimationCurve swingCurve;

        [Tooltip("The curve used to control the thrust position of the sword")]
        public AnimationCurve thrustCurve;
    }
}