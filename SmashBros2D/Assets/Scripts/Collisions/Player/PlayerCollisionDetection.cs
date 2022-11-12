using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    public class PlayerCollisionDetection : MonoBehaviour
    {
        // [Header("Layers")]
        // public LayerMask groundLayer;

        [Header("Collision Settings")]
        [SerializeField, Range(0f, 90f)] private float maxGroudAngle  = 60f  ;
        [SerializeField, Range(0f, 90f)] private float minWallAngle   = 75f  ;
        [SerializeField, Range(0f, 90f)] private float maxWallAngle   = 90f  ;
        [SerializeField, Range(0f, 1f)]  private float uncertainty    =  .1f ;

        [Header("Platform Settings")]
        [SerializeField] private LayerMask    _platformLayerMask ;
        [SerializeField] private List<string> _platformTagMask   ;

        private PlayerCollision _env ;
        public  PlayerCollision env { get => _env ; }

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
            _env.Clear();
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

            if (_env.onGround && IsPlatform(collision.gameObject))
            {
                _env.platformVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity ;
                Debug.Log(_env.platformVelocity);
            }
            else
            {
                _env.platformVelocity = Vector2.zero ;
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

        private bool IsPlatform(GameObject gameObject)
        {
            return (_platformLayerMask.value & (1 << gameObject.layer)) > 0 && _platformTagMask.Contains(gameObject.tag);
        }
    }

    
    public struct PlayerCollision
    {
        public bool  onGround    ;
        public float friction    ;

        public Vector2 platformVelocity ;

        public bool  onWall      ;
        public bool  onRightWall ;
        public bool  onLeftWall  ;
        public int   wallSide    ;

        public void Clear()
        {
            platformVelocity = Vector2.zero ;
            onGround    = false;
            friction = 0;

            onWall      = false;
            onRightWall = false;
            onLeftWall  = false;
        }
    }
}