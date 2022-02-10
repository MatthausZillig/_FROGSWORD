using System;
using UnityEngine;

namespace _Assets._Scripts.Systems
{
    public interface IAgentInput
    {
        Vector2 MovementVector { get; }

        event Action OnAttackPressed;
        event Action OnJumpPressed;
        event Action OnJumpReleased;
        event Action<Vector2> OnMovement;
        event Action OnWeaponChangePressed;
        event Action OnDashPressed;
    }
}

/*
 * Essa é uma interface com a lista de eventos de input
 */