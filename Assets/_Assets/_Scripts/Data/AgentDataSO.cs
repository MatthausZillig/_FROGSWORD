using UnityEngine;

namespace _Assets._Scripts.Data
{
    [CreateAssetMenu(fileName = "AgentData", menuName = "Agent/new Data")]
    public class AgentDataSO : ScriptableObject
    {
        [Header("Movement Data")] [Space] 
        public float MaxSpeed = 6;
        public float Acceleration = 50;
        public float Deceleration = 50;

        [Header("Jump Data")] [Space] 
        public float JumpForce = 12;
        public float GravityMultiplier = 2;
        public float GravityModifier = 0.08f;
    }
}
