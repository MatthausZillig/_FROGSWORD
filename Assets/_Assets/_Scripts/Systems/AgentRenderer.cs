using UnityEngine;

namespace _Assets._Scripts.Systems
{
    public class AgentRenderer : MonoBehaviour
    {
        public void FaceDirection(Vector2 input)
        {
            if (Mathf.Abs(input.x) < Mathf.Epsilon) return;
            transform.parent.localScale = new Vector2(Mathf.Sign(input.x) * Mathf.Abs(transform.localScale.x), 1f);
        } 
    }
}
