using System;
using UnityEngine;

namespace _Assets._Scripts.Systems.States
{
    public class MovementState : State
    {
        [SerializeField] protected MovementData MovementData;
        public State IdleState;

        public float Acceleration, Deceleration, MaxSpeed;

        private void Awake()
        {
            MovementData = GetComponentInParent<MovementData>();
        }

        protected override void EnterState()
        {
            _agent.AnimationManager.PlayAnimation(AnimationType.run);

            MovementData.HorizontalMovementDirection = 0;
            MovementData.CurrentSpeed = 0;
            MovementData.CurrentVelocity = Vector2.zero;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            CalculateVelocity();
            SetPlayerVelocity();
            if (Mathf.Abs(_agent.rb2d.velocity.x) < 0.01f)
            {
                _agent.TransitionToState(IdleState);
            }
        }

        private void SetPlayerVelocity()
        {
            _agent.rb2d.velocity = MovementData.CurrentVelocity;
        }

        private void CalculateVelocity()
        {
            CalculateSpeed(_agent.PlayerInput.MovementVector, MovementData);
            CalculateHorizontalDirection(MovementData);
            MovementData.CurrentVelocity =
                Vector3.right * MovementData.HorizontalMovementDirection * MovementData.CurrentSpeed;

            var movementDataCurrentVelocity = MovementData.CurrentVelocity;
            movementDataCurrentVelocity.y = _agent.rb2d.velocity.y;
        }

        private void CalculateHorizontalDirection(MovementData movementData)
        {
            if (_agent.PlayerInput.MovementVector.x > 0)
            {
                movementData.HorizontalMovementDirection = 1;
            }
            else if (_agent.PlayerInput.MovementVector.x < 0)
            {
                movementData.HorizontalMovementDirection = -1;
            }
        }

        private void CalculateSpeed(Vector2 playerInputMovementVector, MovementData movementData)
        {
            if (Mathf.Abs(playerInputMovementVector.x) > 0)
            {
                movementData.CurrentSpeed += Acceleration * Time.deltaTime;
            }
            else
            {
                movementData.CurrentSpeed -= Deceleration * Time.deltaTime;
            }

            movementData.CurrentSpeed = Mathf.Clamp(movementData.CurrentSpeed, 0, MaxSpeed);
        }
    }
}