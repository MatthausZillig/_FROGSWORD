using UnityEngine;

namespace _Assets._Scripts.Systems.States
{
    public class JumpState : MovementState
    {
        #region Components and Variables
        private bool JumpPressed = false;
        #endregion
        
        #region Overrides
        protected override void EnterState()
        {
            _agent.AnimationManager.PlayAnimation(AnimationType.jump);
            var velocity = _agent.rb2d.velocity;
            MovementData.CurrentVelocity = velocity;
            MovementData.CurrentVelocity.y = _agent.AgentData.JumpForce;
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
        #endregion
        
        #region Helpers
        private void ControlJumpHeight()
        {
            if (JumpPressed != false) return;
            MovementData.CurrentVelocity = _agent.rb2d.velocity;
            MovementData.CurrentVelocity.y += _agent.AgentData.GravityMultiplier*Physics2D.gravity.y * Time.deltaTime;
            _agent.rb2d.velocity = MovementData.CurrentVelocity;
        }
        #endregion
    }
}
