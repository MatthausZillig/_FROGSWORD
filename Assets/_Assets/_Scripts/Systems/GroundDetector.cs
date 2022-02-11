using UnityEngine;

namespace _Assets._Scripts.Systems
{
    public class GroundDetector : MonoBehaviour
    {
        [Header("Components:")]
        public Collider2D AgentCollider;
        public LayerMask GroundMask;

        public bool IsGrounded = false;

        [Header("Gismo parameters:")] 
        [Range(-2f, 2f)]
        public float BoxCastYOffset = -0.1f;
        [Range(-2f, 2f)]
        public float BoxCastXOffset = -0.1f;

        [Range(0, 2)] public float BoxCastWidth = 1, BoxCastHeight = 1;
        [Space(20)]
        public Color GizmoColorNotGrounded = Color.red, GizmoColorGrounded = Color.green;

        private void Awake()
        {
            if (AgentCollider == null)
                AgentCollider = GetComponent<Collider2D>();
        }
    
        public void CheckIsPrivate()
        {
            var raycastHit2D =
                Physics2D.BoxCast(AgentCollider.bounds.center + new Vector3(BoxCastXOffset, BoxCastYOffset, 0),
                    new Vector2(BoxCastWidth, BoxCastHeight), 0, Vector2.down, 0, GroundMask);
            IsGrounded = raycastHit2D.collider != null;
        }

        private void OnDrawGizmos()
        {
            if(AgentCollider == null)
                return;
            Gizmos.color = GizmoColorNotGrounded;
            if (IsGrounded == true)
                Gizmos.color = GizmoColorGrounded;
            Gizmos.DrawWireCube(AgentCollider.bounds.center + new Vector3(BoxCastXOffset, BoxCastYOffset, 0), new Vector3(BoxCastWidth, BoxCastHeight));
        }
    }
}
