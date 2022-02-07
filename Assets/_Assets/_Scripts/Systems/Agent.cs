using System;
using _Assets._Input;
using UnityEngine;

namespace _Assets._Scripts.Systems
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        public Rigidbody2D rb2d;

        public IAgentInput PlayerInput;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            PlayerInput = GetComponentInParent<IAgentInput>();
        }

        private void Start()
        {
            PlayerInput.OnMovement += HandleMovement;
        }

        private void HandleMovement(Vector2 input)
        {
            var movementInput = new Vector2(5 * input.x, rb2d.velocity.y);
            rb2d.velocity = movementInput;
        }
    }
}
