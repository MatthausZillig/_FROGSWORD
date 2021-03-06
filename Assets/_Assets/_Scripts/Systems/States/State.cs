using UnityEngine;
using UnityEngine.Events;

namespace _Assets._Scripts.Systems.States
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] protected State JumpState, FallState;
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
            TestJumpTransition();
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
            TestFallTransition();
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
        
        #region Tests
        private void TestJumpTransition()
        {
            if (_agent.GroundDetector.IsGrounded)
            {
                _agent.TransitionToState(JumpState);
            }
        }
        protected bool TestFallTransition()
        {
            if (_agent.GroundDetector.IsGrounded != false) return false;
            _agent.TransitionToState(FallState);
            return true;

        }
        #endregion
    }
}

/*
 * Essa ?? a classe abstrata que serve de base para as classes de estado (IdleState, MovementState, etc)
 * Ela instancia o Agent, dando acesso aos eventos de input e tudo o mais que possu??mos em Agent
 * com isso associa esses eventos ?? aos m??todos de Handle.
 */
