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

        private Vector2 direction       ;
        private Vector2 desiredVelocity ;
        
        // Update is called once per frame
        private void Update()
        {
            direction.x     = _manager.input.RetrieveMoveInput();
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(_maxSpeed - _manager.env.friction, 0f);
        }

        // FixedUpdate is called every fixed frame-rate frame
        private void FixedUpdate()
        {
            _velocity = _manager.body.velocity;

            float acceleration   = _manager.env.onGround ? _maxAcceleration : _maxAirAcceleration;
            float maxSpeedChange = acceleration * Time.fixedDeltaTime;
            _velocity.x     = Mathf.MoveTowards(_velocity.x, desiredVelocity.x + _manager.env.platformVelocity.x, maxSpeedChange);

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

            _manager.body.velocity = _velocity ;
                        
        }
    }
}