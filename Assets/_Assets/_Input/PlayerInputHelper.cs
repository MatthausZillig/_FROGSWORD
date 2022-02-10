using System;
using _Assets._Scripts.Systems;
using UnityEngine;
using UnityEngine.Events;

namespace _Assets._Input
{
    public class PlayerInputHelper : MonoBehaviour, IAgentInput
    {
        [SerializeField] private PlayerInputSO input;

        public Vector2 MovementVector => input.MovementVector;
        public event Action OnAttackPressed;
        public event Action OnJumpPressed;
        public event Action OnJumpReleased;
        public event Action<Vector2> OnMovement;
        public event Action OnWeaponChangePressed;
        public event Action OnDashPressed;

        public UnityEvent onMenuPressed;

        private void Awake()
        {
            input.ResetEvents();
            input.OnAttackPressed += () => OnAttackPressed?.Invoke();
            input.OnJumpPressed += () => OnJumpPressed?.Invoke();
            input.OnJumpReleased += () => OnJumpReleased?.Invoke();
            input.OnWeaponChangePressed += () => OnWeaponChangePressed?.Invoke();
            input.OnDashPressed += () => OnDashPressed?.Invoke();
            input.OnMovement += (vector) => OnMovement?.Invoke(vector);
            input.OnMenu += () => onMenuPressed?.Invoke();
        }
    }
}
