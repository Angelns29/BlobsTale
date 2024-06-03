using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Health;
using UnityEngine;

namespace TopDownController2D.Scripts.TopDownCharacter2D.Animations
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(TopDownCharacterController))]
    public abstract class Animations : MonoBehaviour
    {
        protected Animator animator;
        protected TopDownCharacterController controller;
        
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<TopDownCharacterController>();
        }
    }
}