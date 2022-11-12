using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlatformEffector2D), typeof(PlatformCollisionDetection))]
    public class PlatformController : MonoBehaviour
    {
        private const float _ROTOFFUP   =   0f ;
        private const float _ROTOFFDOWN = 180f ;

        [Header("Movement Settings")]
        [SerializeField] private bool      _isMoving  = false ;
        [SerializeField] private Transform _endPoint  = null  ;
        [SerializeField, Range(0f, 5f)] private float _endTime  = 5f;
        
        [Header("One Way Settings")]
        [SerializeField] private bool _useOneWay      = false ;
        [SerializeField] private bool _useFallThrough = false ;

        private PlatformCollision _state = new PlatformCollision() ;
        public  PlatformCollision state { get => _state ; }

        private Rigidbody2D                _body     ;
        private PlatformEffector2D         _effector ;
        private PlatformCollisionDetection _detector ;

        private Vector2 _startPoint ;
        private Vector2 _lastPoint  ;
        private Vector2 _nextPoint  ;

        private float _time                  = 0f    ;

        void Awake()
        {
            _body     = GetComponent<Rigidbody2D>();
            _effector = GetComponent<PlatformEffector2D>();
            _detector = GetComponent<PlatformCollisionDetection>();
        }


        // Start is called before the first frame update
        void Start()
        {
            // Moving Platform Init
            _startPoint = this.transform.position ;
            _lastPoint  = _startPoint ;
            _nextPoint  = _endPoint.position   ;
            
            // One Way Platform Init
            _effector.useOneWay    = _useOneWay ;

            // Link Events
            FallThrough.FallTroughPlatform += PlayerFallThrough;
        }

        // Update is called once per frame
        void Update()
        {
            if (_detector != null)
            {
                _state = _detector.state ;

                if (_state.playerCollisionID.Count==0)
                {
                    _effector.rotationalOffset = _ROTOFFUP ;
                }
            }
        }

        // FixedUpdate is called every fixed frame-rate frame
        void FixedUpdate()
        {
            if (_isMoving)
            {
                _time += Time.fixedDeltaTime;

                if (_time <= _endTime)
                {
                    this._body.MovePosition( Vector2.Lerp(_lastPoint, _nextPoint, _time/_endTime ) );
                }
                else
                {
                    Vector2 tmp = _lastPoint ;
                    _lastPoint = _nextPoint;
                    _nextPoint = tmp;

                    _time = 0f;
                }
            }
        }

        private void PlayerFallThrough(int playerID)
        {
            if (_detector != null && _useFallThrough)
            {                
                if (_state.playerCollisionID.Contains(playerID))
                {
                    _effector.rotationalOffset = _ROTOFFDOWN;
                }
            }
        }
    }
}
