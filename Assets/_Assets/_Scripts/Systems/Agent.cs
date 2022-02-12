using System;
using _Assets._Input;
using _Assets._Scripts.Data;
using _Assets._Scripts.Systems.States;
using UnityEditor.Rendering;
using UnityEngine;

namespace _Assets._Scripts.Systems
{
    public class Agent : MonoBehaviour
    {
        #region Components and Debugging
        [SerializeField] [Header("Components")]
        public Rigidbody2D rb2d;
        public IAgentInput PlayerInput;
        public AgentDataSO AgentData;
        public AgentAnimation AnimationManager;
        public AgentRenderer AgentRenderer;
        public GroundDetector GroundDetector;
        
        [SerializeField] [Header("State")] public State CurrentState = null;
        public State previousState = null;
        public State IdleState;

        [Header("State debugging:")]
        public string stateName = "";
        #endregion

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            PlayerInput = GetComponentInParent<IAgentInput>();
            AnimationManager = GetComponentInChildren<AgentAnimation>();
            AgentRenderer = GetComponentInChildren<AgentRenderer>();
            GroundDetector = GetComponentInChildren<GroundDetector>();
            
            var states = GetComponentsInChildren<State>();
            foreach (var state in states)
            {
                state.InitializeState(this);
            }
        }

        private void Start()
        {
            PlayerInput.OnMovement += AgentRenderer.FaceDirection;
            TransitionToState(IdleState);
        }
        
        private void Update()
        {
            CurrentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            GroundDetector.CheckIsGrounded();
            CurrentState.StateFixedUpdate();
        }
        
        #region Transition State
        internal void TransitionToState(State desiredState)
        {
            if (desiredState == null)
                return;
            if (CurrentState != null)
                CurrentState.Exit();
            previousState = CurrentState;
            CurrentState = desiredState;
            CurrentState.Enter();
            DebuggingState();
            
        }
        #endregion
        
        #region Debugging
        private void DebuggingState()
        {
            if (previousState == null || previousState.GetType() != CurrentState.GetType())
            {
                stateName = CurrentState.GetType().ToString();
            }
        }
        #endregion
    }
}
