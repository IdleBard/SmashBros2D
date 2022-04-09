using UnityEngine;

namespace smash_bros
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GroundCollision : MonoBehaviour
    {
        [Header("Layers")]
        public LayerMask groundLayer;

        [Header("Collision Settings")]
        public  float   collisionRadius = 0.25f;

        // [Header("Debug Settings")]
        private Color debugCollisionColor = Color.red;

        private BoxCollider2D box;
        private Vector2 bottomOffset, rightOffset, leftOffset;

        private bool onGround    ;
        private bool onWall      ;
        private bool onRightWall ;
        private bool onLeftWall  ;
        private int  wallSide    ;

        private float friction;

        void Awake()
        {
            box = GetComponent<BoxCollider2D>();
        }

        void Start()
        {
            bottomOffset = new Vector2( 0, -box.size.y / 2 );
            rightOffset  = new Vector2(  box.size.x / 2, 0 );
            leftOffset   = new Vector2( -box.size.x / 2, 0 );
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            onGround    = false;
            onWall      = false;
            onRightWall = false;
            onLeftWall  = false;

            friction = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EvaluateCollision();
            RetrieveFriction(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision();
            RetrieveFriction(collision);
        }

        private void EvaluateCollision()
        {
            onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
            onWall   = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

            onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
            onLeftWall  = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

            wallSide = onRightWall ? -1 : 1;
        }

        private void RetrieveFriction(Collision2D collision)
        {
            PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;

            friction = 0;

            if(material != null)
            {
                friction = material.friction;
            }
        }



        public bool GetOnGround()
        {
            return onGround;
        }

        public bool GetOnWall()
        {
            return onWall;
        }

        public bool GetOnRightWall()
        {
            return onRightWall;
        }

        public bool GetOnLeftWall()
        {
            return onLeftWall;
        }

        public int GetWallSide()
        {
            return wallSide;
        }

        public float GetFriction()
        {
            return friction;
        }


        // Debug
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

            Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position  + rightOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position  + leftOffset, collisionRadius);
        }
    }
}