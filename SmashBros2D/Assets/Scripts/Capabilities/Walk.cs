using UnityEngine;

namespace SmashBros2D
{
    public class Walk : Movement
    {

        [Header("Walk Settings")]
        [SerializeField, Range(0f, 100f)] private float _maxSpeed           = 4f  ;
        [SerializeField, Range(0f, 100f)] private float _maxAcceleration    = 35f ;
        [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f ;

        [Header("Wall Stick")]
        [SerializeField, Range(0f, 1f)] private float _maxTimeWallStick = .1f ;

        private float _timeWallStick;

        private Vector2 _direction        ;
        private Vector2 _desiredVelocity  ;
        
        // Update is called once per frame
        private void Update()
        {
            _direction.x     = _manager.input.RetrieveMoveInput();
            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _manager.env.friction, 0f);
        }

        // FixedUpdate is called every fixed frame-rate frame
        private void FixedUpdate()
        {
            _velocity = _manager.body.velocity;
            if (_manager.env.onPlatform)
            {
                _velocity.x -= _manager.env.platformVelocity.x ;
            }

            float acceleration   = _manager.env.onGround ? _maxAcceleration : _maxAirAcceleration;
            float maxSpeedChange = acceleration * Time.fixedDeltaTime;
            _velocity.x     = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);

            if (_manager.env.onWall)
            {
                if (_timeWallStick < _maxTimeWallStick)
                {
                    _timeWallStick += Time.fixedDeltaTime;
                    _velocity.x = _manager.body.velocity.x;
                }
            }
            else
            {
                _timeWallStick = 0;
            }

            if (_manager.env.onPlatform)
            {
                _velocity.x      += _manager.env.platformVelocity.x ;
            }
            _manager.body.velocity = _velocity    ;
                        
        }
    }
}