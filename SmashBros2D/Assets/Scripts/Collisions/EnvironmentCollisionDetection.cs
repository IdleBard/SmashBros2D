using UnityEngine;

namespace SmashBros2D
{
    public class EnvironmentCollisionDetection : MonoBehaviour
    {
        // [Header("Layers")]
        // public LayerMask groundLayer;

        [Header("Collision Settings")]
        [SerializeField, Range(0f, 90f)] private float maxGroudAngle  = 60f  ;
        [SerializeField, Range(0f, 90f)] private float minWallAngle   = 75f  ;
        [SerializeField, Range(0f, 90f)] private float maxWallAngle   = 90f  ;
        [SerializeField, Range(0f, 1f)]  private float uncertainty    =  .1f ;

        private EnvironmentCollision _env;
        public EnvironmentCollision env { get => _env ; }

        private float _minLimitGround ;
        private float _minLimitWall   ;
        private float _maxLimitWall   ;


        void Start()
        {
            _minLimitGround = Mathf.Cos(Mathf.PI * maxGroudAngle / 180f);
            _maxLimitWall   = Mathf.Cos(Mathf.PI * minWallAngle  / 180f);
            _minLimitWall   = Mathf.Cos(Mathf.PI * maxWallAngle  / 180f);

            _env.Clear();
            _env.wallSide = 1;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            env.Clear();
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

                _env.onGround |= normal.y  >= (_minLimitGround - uncertainty) ;
                _env.onWall   |= (normal.y >= (_minLimitWall   - uncertainty) && normal.y < (_maxLimitWall + uncertainty));

                _env.onRightWall |= (_env.onWall && normal.x < 0);
                _env.onLeftWall  |= (_env.onWall && normal.x > 0);

            }

            _env.wallSide = _env.onRightWall ? -1 : 1;
        }

        private void RetrieveFriction(Collision2D collision)
        {
            PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;

            _env.friction = 0;

            if(material != null)
            {
                _env.friction = material.friction;
            }
        }
    }

    
    public struct EnvironmentCollision
    {
        public bool  onGround    ;
        public float friction    ;

        public bool  onWall      ;
        public bool  onRightWall ;
        public bool  onLeftWall  ;
        public int   wallSide    ;

        public void Clear()
        {
            onGround    = false;
            friction = 0;

            onWall      = false;
            onRightWall = false;
            onLeftWall  = false;
        }
    }
}