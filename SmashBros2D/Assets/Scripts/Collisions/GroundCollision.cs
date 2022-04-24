using UnityEngine;

namespace SmashBros2D
{
    public class GroundCollision : MonoBehaviour
    {
        // [Header("Layers")]
        // public LayerMask groundLayer;

        [Header("Collision Settings")]
        [SerializeField, Range(0f, 90f)] private float maxGroudAngle  = 60f  ;
        [SerializeField, Range(0f, 90f)] private float minWallAngle   = 75f  ;
        [SerializeField, Range(0f, 90f)] private float maxWallAngle   = 90f  ;
        [SerializeField, Range(0f, 1f)]  private float uncertainty    =  .1f ;

        private bool onGround    ;
        private bool onWall      ;
        private bool onRightWall ;
        private bool onLeftWall  ;
        private int  wallSide    ;

        private float minLimitGround ;
        private float minLimitWall   ;
        private float maxLimitWall   ;

        private float friction;

        void Start()
        {
            minLimitGround = Mathf.Cos(Mathf.PI * maxGroudAngle / 180f);
            maxLimitWall   = Mathf.Cos(Mathf.PI * minWallAngle  / 180f);
            minLimitWall   = Mathf.Cos(Mathf.PI * maxWallAngle  / 180f);
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
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void EvaluateCollision(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;

                onGround |= normal.y  >= (minLimitGround - uncertainty) ;
                onWall   |= (normal.y >= (minLimitWall   - uncertainty) && normal.y < (maxLimitWall + uncertainty));

                onRightWall |= (onWall && normal.x < 0);
                onLeftWall  |= (onWall && normal.x > 0);

            }

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
    }
}