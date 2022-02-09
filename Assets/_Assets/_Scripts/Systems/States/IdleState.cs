using UnityEngine;

namespace _Assets._Scripts.Systems.States
{
   public class IdleState : State
   {
      public State MoveState;
      protected override void EnterState()
      {
         _agent.AnimationManager.PlayAnimation(AnimationType.idle);
      }
   
      protected override void HandleMovement(Vector2 input)
      {
         if (Mathf.Abs(input.x) > 0)
         {
            _agent.TransitionToState(MoveState, this);
         }
      }
   }
}
