using UnityEngine;

namespace _Assets._Scripts.Systems.States
{
    public class JumpState : MovementState
    {
        public float JumpForce = 12;
        [SerializeField] [Header("Gravity Multiplier:")]
        public float GravityMultiplier = 2;
        [SerializeField] [Header("Fall State: ")]
        public State FallState;
        
        private bool JumpPressed = false;
        protected override void EnterState()
        {
            _agent.AnimationManager.PlayAnimation(AnimationType.jump);
            var velocity = _agent.rb2d.velocity;
            MovementData.CurrentVelocity = velocity;
            MovementData.CurrentVelocity.y = JumpForce;
            velocity = MovementData.CurrentVelocity;
            _agent.rb2d.velocity = velocity;
            JumpPressed = true;
        }

        protected override void HandleJumpPressed()
        {
            JumpPressed = true;
        }

        protected override void HandleJumpReleased()
        {
            JumpPressed = false;
        }

        public override void StateUpdate()
        {
            ControlJumpHeight();
            CalculateVelocity();
            SetPlayerVelocity();
            if (_agent.rb2d.velocity.y <= 0)
            {
                _agent.TransitionToState(FallState);
            }
        }
        
        private void ControlJumpHeight()
        {
            if (JumpPressed != false) return;
            MovementData.CurrentVelocity = _agent.rb2d.velocity;
            MovementData.CurrentVelocity.y += GravityMultiplier*Physics2D.gravity.y * Time.deltaTime;
            _agent.rb2d.velocity = MovementData.CurrentVelocity;
        }
    }
}
