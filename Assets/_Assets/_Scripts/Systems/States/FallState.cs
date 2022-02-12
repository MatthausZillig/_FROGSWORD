using UnityEngine;

namespace _Assets._Scripts.Systems.States
{
    public class FallState : MovementState
    {
        [SerializeField] [Header("Jump State: ")]
        public new State JumpState;

        protected override void EnterState()
        {
            _agent.AnimationManager.PlayAnimation(AnimationType.fall);
        }

        protected override void HandleJumpPressed()
        {
            // Dont
        }

        public override void StateUpdate()
        {
            ApplyAdditionalGravity();
            CalculateVelocity();
            SetPlayerVelocity();

            if (_agent.GroundDetector.IsGrounded)
                _agent.TransitionToState(IdleState);
        }

        private void ApplyAdditionalGravity()
        {
            MovementData.CurrentVelocity = _agent.rb2d.velocity;
            MovementData.CurrentVelocity.y += _agent.AgentData.GravityModifier * Physics2D.gravity.y * Time.deltaTime;
            _agent.rb2d.velocity = MovementData.CurrentVelocity;
        }
    }
}