using System;
using UnityEngine;

namespace _Assets._Scripts.Systems.States
{
    public class MovementState : State
    {
        [SerializeField] protected MovementData MovementData;
        [Header("Settings")] [Space(10)] public State IdleState;
        
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
            if (TestFallTransition())
                return;
            CalculateVelocity();
            SetPlayerVelocity();
            if (Mathf.Abs(_agent.rb2d.velocity.x) < 0.01f)
            {
                _agent.TransitionToState(IdleState);
            }
        }

        protected void SetPlayerVelocity()
        {
            _agent.rb2d.velocity = MovementData.CurrentVelocity;
        }

        protected void CalculateVelocity()
        {
            CalculateSpeed(_agent.PlayerInput.MovementVector, MovementData);
            CalculateHorizontalDirection(MovementData);
            MovementData.CurrentVelocity =
                Vector3.right * MovementData.HorizontalMovementDirection * MovementData.CurrentSpeed;
            MovementData.CurrentVelocity.y = _agent.rb2d.velocity.y;
        }

        protected void CalculateHorizontalDirection(MovementData movementData)
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

        protected void CalculateSpeed(Vector2 playerInputMovementVector, MovementData movementData)
        {
            if (Mathf.Abs(playerInputMovementVector.x) > 0)
            {
                movementData.CurrentSpeed += _agent.AgentData.Acceleration * Time.deltaTime;
            }
            else
            {
                movementData.CurrentSpeed -= _agent.AgentData.Deceleration * Time.deltaTime;
            }

            movementData.CurrentSpeed = Mathf.Clamp(movementData.CurrentSpeed, 0, _agent.AgentData.MaxSpeed);
        }
    }
}

/*
 * Classe que gerencia o state de Movement, ela herda de State, nela também utilizamos o MovementData para pegar os
 * dados e fazer os cálculos necessários para a movimentação do usuário: Direction, Speed e com isso saber a Velocity.
 * No StateUpdate os métodos são chamados e setados, assim como é feita a transição de estado caso nenhum input de
 * movimento seja computado.
 */