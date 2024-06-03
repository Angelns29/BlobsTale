using UnityEngine;

namespace TopDownCharacter2D.UI
{
    public class GaugeFXHandler : MonoBehaviour
    {
        private static readonly int PlayEffect = Animator.StringToHash("PlayEffect");
        [SerializeField] private Animator effectAnimator;

        public void StartGaugeFX()
        {
            effectAnimator.SetTrigger(PlayEffect);
        }
    }
}