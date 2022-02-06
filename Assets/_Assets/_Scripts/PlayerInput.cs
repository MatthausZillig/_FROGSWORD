using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [field: Header("Inputs")] [field: SerializeField]
    public Vector2 MovementVector { get; private set; }
    public event Action OnAttack, OnJumpPressed, OnJumpReleased, OnWeaponChange;
    public event Action<Vector2> OnMovement;
    
    public KeyCode jumpKey, attackKey, menuKey;

    public UnityEvent OnMenuKeyPressed;

    private void Update()
    {
        GetMovementInput();
        
        GetJumpInput();
        
        GetAttackInput();
        
        GetWeaponSwapInput();
        
        GetMenuInput();
    }

    private void GetMenuInput()
    {
        throw new NotImplementedException();
    }

    private void GetWeaponSwapInput()
    {
        
    }

    private void GetAttackInput()
    {
        throw new NotImplementedException();
    }

    private void GetJumpInput()
    {
        throw new NotImplementedException();
    }

    private void GetMovementInput()
    {
        throw new NotImplementedException();
    }
}
