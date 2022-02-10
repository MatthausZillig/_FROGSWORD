using System;
using _Assets._Input;
using _Assets._Scripts.Systems.States;
using UnityEditor.Rendering;
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

        public State currentState = null, previousState = null;
        public State IdleState;

        [Header("State debugging:")]
        public string stateName = "";

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            PlayerInput = GetComponentInParent<IAgentInput>();
            AnimationManager = GetComponentInChildren<AgentAnimation>();
            AgentRenderer = GetComponentInChildren<AgentRenderer>();
            State[] states = GetComponentsInChildren<State>();
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

        private void DebuggingState()
        {
            if (previousState == null || previousState.GetType() != currentState.GetType())
            {
                stateName = currentState.GetType().ToString();
            }
        }

        private void Update()
        {
            currentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            currentState.StateFixedUpdate();
        }
    }
}
