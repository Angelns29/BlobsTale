using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownCharacter2D.Controllers
{
    public class TopDownInputController : TopDownCharacterController
    {
        private Camera _camera;

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
        }

        #region Methods called by unity input events

        public void OnMove(InputValue value)
        {
            Vector2 moveInput = value.Get<Vector2>().normalized;
            OnMoveEvent.Invoke(moveInput);
        }

        public void OnLook(InputValue value)
        {
            Vector2 newAim = value.Get<Vector2>();
            if (!(newAim.normalized == newAim))
            {
                Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
                newAim = (worldPos - (Vector2) transform.position).normalized;
            }

            if (newAim.magnitude >= .9f)
            {
                LookEvent.Invoke(newAim);
            }
        }

        
        public void OnFire(InputValue value)
        {
            IsAttacking = value.isPressed;
        }

        #endregion
    }
}