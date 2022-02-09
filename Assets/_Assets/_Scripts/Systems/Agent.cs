using System;
using _Assets._Input;
using _Assets._Scripts.Systems.States;
using UnityEngine;

namespace _Assets._Scripts.Systems
{
    public class Agent : MonoBehaviour
    {
        [SerializeField] [Header("Rigidbody2D")]
        public Rigidbody2D rb2d;
        public IAgentInput PlayerInput;
        public AgentAnimation AnimationManager;
        public AgentRenderer AgentRenderer;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            PlayerInput = GetComponentInParent<IAgentInput>();
            AnimationManager = GetComponentInChildren<AgentAnimation>();
            AgentRenderer = GetComponentInChildren<AgentRenderer>();
        }

        private void Start()
        {
            PlayerInput.OnMovement += HandleMovement;
            PlayerInput.OnMovement += AgentRenderer.FaceDirection;
        }
        
        public void TransitionToState(State moveState, IdleState idleState)
        {
            throw new NotImplementedException();
        }

        private void HandleMovement(Vector2 input)
        {
            if (Mathf.Abs(input.x) > 0)
            {
                if (Mathf.Abs(rb2d.velocity.x) < 0.01f)
                {
                    AnimationManager.PlayAnimation(AnimationType.run);
                }
                rb2d.velocity = new Vector2(input.x * 5, rb2d.velocity.y);
            }
            else
            {
                if (Mathf.Abs(rb2d.velocity.x) > 0.01f)
                {
                    AnimationManager.PlayAnimation(AnimationType.idle);
                }
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
        
    }
}
