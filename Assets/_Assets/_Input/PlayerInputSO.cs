using System;
using _Assets._Scripts.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Assets._Input
{
   [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "Player Helpers/New PlayerInput")]
   public class PlayerInputSO : ScriptableObject, PlayerInputConfig.IPauseMenuActions, PlayerInputConfig.IPlayerMovementActions, IAgentInput
   {
      #region Variables

      PlayerInputConfig input;
      
      public event Action OnMenu;
      public event Action OnAttackPressed;
      public event Action OnJumpPressed;
      public event Action OnJumpReleased;
      public event Action<Vector2> OnMovement;
      public event Action OnWeaponChangePressed;
      public event Action OnDashPressed;
      public Vector2 MovementVector { get; private set; }

      #endregion
      private void OnEnable()
      {
         if (input != null) return;
         input = new PlayerInputConfig();
         input.PlayerMovement.SetCallbacks(this);
         input.PlayerMovement.SetCallbacks(this);
         input.PlayerMovement.Enable();
      }

      private void OnDisable()
      {
         input = null;
         ResetEvents();
      }

      public void ResetEvents()
      { 
         OnMenu = null;
         OnAttackPressed = null;
         OnJumpPressed = null;
         OnJumpReleased = null;
         OnMovement = null;
         OnWeaponChangePressed = null;
         OnDashPressed = null;
      }

      #region Inputs

      public void OnEnterMenu(InputAction.CallbackContext context)
      {
         if (context.phase != InputActionPhase.Performed) return;
         OnMenu?.Invoke();
         input.PlayerMovement.Disable();
         input.PauseMenu.Enable();
      }
      public void OnExitMenu(InputAction.CallbackContext context)
      {
         if (context.phase != InputActionPhase.Performed) return;
         OnMenu?.Invoke();
         input.PauseMenu.Disable();
         input.PlayerMovement.Enable();
      }
      
      public void OnMoveAgent(InputAction.CallbackContext context)
      {
         MovementVector = context.ReadValue<Vector2>();
         OnMovement?.Invoke(MovementVector);
      }

      public void OnJump(InputAction.CallbackContext context)
      {
         switch (context.phase)
         {
            case InputActionPhase.Performed:
               OnJumpPressed?.Invoke();
               break;
            case InputActionPhase.Canceled:
               OnJumpReleased?.Invoke();
               break;
            case InputActionPhase.Disabled:
               break;
            case InputActionPhase.Waiting:
               break;
            case InputActionPhase.Started:
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }
      }
      public void OnAttack(InputAction.CallbackContext context)
      {
         if (context.phase != InputActionPhase.Performed) return;
         OnAttackPressed?.Invoke();
      }

      public void OnDash(InputAction.CallbackContext context)
      {
         if (context.phase != InputActionPhase.Performed) return;
         OnDashPressed?.Invoke();
      }

      public void OnWeaponChange(InputAction.CallbackContext context)
      {
         if (context.phase != InputActionPhase.Performed) return;
         OnWeaponChangePressed?.Invoke();
      }

      #endregion
     
   }
}
