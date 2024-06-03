using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Stats;
using UnityEngine;

namespace TopDownCharacter2D
{
    [RequireComponent(typeof(CharacterStatsHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TopDownCharacterController))]
    public class TopDownMovement : MonoBehaviour
    {
        private TopDownCharacterController _controller;

        private Vector2 _movementDirection = Vector2.zero;
        private Rigidbody2D _rb;
        private CharacterStatsHandler _stats;

        private void Awake()
        {
            _controller = GetComponent<TopDownCharacterController>();
            _stats = GetComponent<CharacterStatsHandler>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _controller.OnMoveEvent.AddListener(Move);
        }

        private void FixedUpdate()
        {
            ApplyMovement(_movementDirection);
        }

        private void Move(Vector2 direction)
        {
            _movementDirection = direction;
        }

        private void ApplyMovement(Vector2 direction)
        {
            _rb.velocity += direction * _stats.CurrentStats.speed;
        }
    }
}