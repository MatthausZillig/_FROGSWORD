using System;
using _Assets._Input;
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
        public AgentAnimation AnimationManager;
        public AgentRenderer AgentRenderer;
        [SerializeField] [Header("State")] public State currentState = null;
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
            currentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            currentState.StateFixedUpdate();
        }
        
        #region Transition State
        internal void TransitionToState(State desiredState)
        {
            if (desiredState == null)
                return;
            if (currentState != null)
                currentState.Exit();
            previousState = currentState;
            currentState = desiredState;
            currentState.Enter();
            DebuggingState();
            
        }
        #endregion
        
        #region Debugging
        private void DebuggingState()
        {
            if (previousState == null || previousState.GetType() != currentState.GetType())
            {
                stateName = currentState.GetType().ToString();
            }
        }
        #endregion
    }
}
