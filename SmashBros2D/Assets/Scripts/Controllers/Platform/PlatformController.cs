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
        [SerializeField] private bool            _isMoving  = false ;
        [SerializeField] private List<Transform> _positions = null  ;
        [SerializeField, Range(0f, 5f)] private float _maxSpeed         = 5f;
        // [SerializeField, Range(0f, 5f)] private float _maxAcceleration  = 5f;
        
        [Header("One Way Settings")]
        [SerializeField] private bool _useOneWay      = false ;
        [SerializeField] private bool _useFallThrough = false ;

        private PlatformCollision _state = new PlatformCollision() ;
        public  PlatformCollision state { get => _state ; }

        private Rigidbody2D                _body     ;
        private PlatformEffector2D         _effector ;
        private PlatformCollisionDetection _detector ;

        // Movement properties
        private int     _nextPosIndex ;
        private float   _time      ;
        private Vector2 _lastPoint ;
        private Vector2 _nextPoint ;

        private float   _distance  { get => Vector2.Distance(_lastPoint,_nextPoint) ; }
        private Vector2 _direction { get => (_nextPoint - _lastPoint) / _distance ; }
        private int   _posNumber { get => _positions.Count ; }

        void Awake()
        {
            _body     = GetComponent<Rigidbody2D>();
            _effector = GetComponent<PlatformEffector2D>();
            _detector = GetComponent<PlatformCollisionDetection>();
        }


        // Start is called before the first frame update
        void Start()
        {
            if (_isMoving)
            {
                Debug.Assert( _posNumber > 1, "Error : moving platform has not enough position points.");

                // Moving Platform Init
                _lastPoint  = this.transform.position ;

                if (this.transform.position == _positions[0].position)
                {  
                    _nextPosIndex  = 1 ;
                    _nextPoint = _positions[1].position ;
                }
                else
                {
                    _nextPosIndex  = 0 ;
                    _nextPoint = _positions[0].position ;
                }

                _time = 0f;
            }

            
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
                MovePlatform();
            }
        }

        private void MovePlatform()
        {
            if  (Vector3.Distance(this.transform.position, _positions[_nextPosIndex].position) <= 1E03 * Vector3.kEpsilon)
            {
                _lastPoint = _nextPoint ;
                _nextPosIndex = (_nextPosIndex + 1) % _posNumber ;
                _nextPoint = _positions[_nextPosIndex].position ;

                _time = 0f;
            }
            
            _time += Time.fixedDeltaTime ;
            this._body.velocity = (Vector2.Lerp(_lastPoint, _nextPoint, _time * _maxSpeed / _distance ) - (Vector2)this.transform.position) / Time.fixedDeltaTime ;
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
