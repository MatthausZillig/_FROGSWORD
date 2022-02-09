using UnityEngine;
using UnityEngine.Events;

namespace _Assets._Scripts.Systems.States
{
    public abstract class State : MonoBehaviour
    {
        protected Agent _agent;

        public UnityEvent OnEnter, OnExit;

        public void InitializeState(Agent agent)
        {
            this._agent = agent;
        }
        #region Enter
        public void Enter()
        {
            this._agent.PlayerInput.OnAttackPressed += HandleAttack;
            this._agent.PlayerInput.OnDashPressed += HandleDash;
            this._agent.PlayerInput.OnJumpPressed += HandleJumpPressed;
            this._agent.PlayerInput.OnJumpReleased += HandleJumpReleased;
            this._agent.PlayerInput.OnMovement += HandleMovement;
            OnEnter?.Invoke();
            EnterState();
        }

        protected virtual void EnterState()
        {
        }
        #endregion
        
        #region Handles
        protected virtual void HandleMovement(Vector2 obj)
        {
        }

        protected virtual void HandleJumpReleased()
        {
        }

        protected virtual void HandleJumpPressed()
        {
        }

        protected virtual void HandleDash()
        {
        }

        protected virtual void HandleAttack()
        {
        }
        #endregion
        
        #region Update
        public virtual void StateUpdate()
        {
            
        }

        public virtual void StateFixedUpdate()
        {
            
        }
        #endregion

        #region Exit
        public void Exit()
        {
            this._agent.PlayerInput.OnAttackPressed -= HandleAttack;
            this._agent.PlayerInput.OnDashPressed -= HandleDash;
            this._agent.PlayerInput.OnJumpPressed -= HandleJumpPressed;
            this._agent.PlayerInput.OnJumpReleased -= HandleJumpReleased;
            this._agent.PlayerInput.OnMovement -= HandleMovement;
            OnExit?.Invoke();
            ExitState();
        }

        protected virtual void ExitState()
        {
           
        }
        #endregion
    }
}
