using UnityEngine;

namespace _Assets._Scripts.Systems.States
{
   public class IdleState : State
   {
      public State MoveState;
      protected override void EnterState()
      {
         _agent.AnimationManager.PlayAnimation(AnimationType.idle);
         if(_agent.GroundDetector.IsGrounded)
            _agent.rb2d.velocity = Vector2.zero;
      }
   
      protected override void HandleMovement(Vector2 input)
      {
         if (Mathf.Abs(input.x) > 0)
         {
            _agent.TransitionToState(MoveState);
         }
      }
   }
}

/*
 * Classe que gerencia o state de Idle, ela herda de State e se utiliza do método EnterState para dar play na animação
 * de Idle, além de utilizar o HandleMovement para fazer a transição de estado caso o input de movimento seja computado.
 */
