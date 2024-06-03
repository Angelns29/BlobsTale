using TopDownCharacter2D.Attacks;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownCharacter2D.Controllers
{
    public class MoveEvent : UnityEvent<Vector2> { }

    public class AttackEvent : UnityEvent<AttackConfig> { }

    public class LookEvent : UnityEvent<Vector2> { }

    public class RangeWeaponEvent : UnityEvent<bool> { }

    public class MeleeWeaponEvent : UnityEvent<bool> { }
}