using UnityEngine;

namespace SmashBros2D
{
    public class Jump : Movement
    {

        [Header("Jump Settings")]
        [SerializeField, Range(0f, 10f)] private float jumpHeight            = 3f   ;
        [SerializeField, Range(0,  5)]   private int   maxAirJumps           = 0    ;
        [SerializeField, Range(0, 10)]   private int   maxJumpBuffer         = 5    ;
        [SerializeField, Range(0, 10)]   private int   maxCoyoteTime         = 5    ;
        [SerializeField ]                private bool  enableWallJump        = true ;
        [SerializeField, Range(0f, 90f)] private float wallJumpAngle         = 45f  ;
        [SerializeField, Range(1,  5)]   private int   maxWallJumps          = 1    ;

        [Header("Gravity Settings")]
        [SerializeField, Range(0f, 5f)]  private float defaultGravityScale   = 1f ;
        [SerializeField, Range(0f, 5f)]  private float fallGravityMultiplier = 3f ;
        [SerializeField, Range(0f, 5f)]  private float upGravityMultiplier   = 2f ;
        
        // Counters
        private int  _jumpPhase     ;
        private int  _jumpBuffer    ;
        private int  _coyoteTime    ;
        private int  _wallJumpPhase ;

        // Input Bool
        private bool _holdJumpButton ;
        private bool _desiredJump    ;

        // Buffers
        private int _oldWallSide;

        // Update is called once per frame
        void Update()
        {
            _desiredJump   |= _manager.input.RetrieveJumpInput();
            _holdJumpButton = _manager.input.RetrieveHoldJumpInput();
        }

        // FixedUpdate is called every fixed frame-rate frame
        private void FixedUpdate()
        {

            if (_manager.env.onWall && _manager.env.wallSide != _oldWallSide)
            {
                _wallJumpPhase = 0;
            }

            _oldWallSide = _manager.env.wallSide;          

            _velocity = _manager.body.velocity;
            if (_manager.env.onPlatform)
            {
                _velocity.y -= _manager.env.platformVelocity.y ;
            }

            if (_manager.env.onGround)
            {
                _jumpPhase     = 0;
                _jumpBuffer    = 0;
                _coyoteTime    = 0;
                _wallJumpPhase = 0;
            }
            else
            {
                _coyoteTime += 1;
            }

            if (_desiredJump)
            {   
                
                if (_manager.env.onGround || _jumpPhase < maxAirJumps || _coyoteTime < maxCoyoteTime)
                {
                    _desiredJump = false;
                    JumpAction(Vector2.up);
                }
                else if(enableWallJump && _manager.env.onWall)
                {   
                    _desiredJump = false;
                    WallJumpAction();
                }
                else if (_jumpBuffer < maxJumpBuffer)
                {
                    _jumpBuffer += 1;
                    _desiredJump = true;
                }
                else
                {
                    _desiredJump = false;
                }
                
            }

            // Improve Jump
            if (_manager.body.velocity.y < 0) // Faster Falling
            {
                _manager.body.gravityScale = defaultGravityScale * fallGravityMultiplier;
            }
            else if (_manager.body.velocity.y > 0 && !_holdJumpButton) // Low Jump
            {
                _manager.body.gravityScale = defaultGravityScale * upGravityMultiplier;
            }
            else
            {
                _manager.body.gravityScale = defaultGravityScale;
            }

            if (_manager.env.onPlatform)
            {
                _velocity.y += _manager.env.platformVelocity.y ;
            }
            _manager.body.velocity = _velocity;
        }

        private void JumpAction(Vector2 direction)
        {
            _jumpPhase += 1;
            
            float   _jumpSpeed    = Mathf.Sqrt(2f * defaultGravityScale * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
            Vector2 _jumpVelocity = direction * _jumpSpeed;
            
            if (_velocity.y > 0f)
            {
                _jumpVelocity.y = Mathf.Max(_jumpVelocity.y - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpVelocity.y += Mathf.Abs(_manager.body.velocity.y);
            }
            
            _velocity += _jumpVelocity;
        }

        private void WallJumpAction()
        {
            if (_wallJumpPhase < maxWallJumps)
            {
                _wallJumpPhase += 1;
                
                float   _wallJumpAngleRadiant = wallJumpAngle * Mathf.PI / 180f;
                Vector2 _direction = new Vector2 (_manager.env.wallSide * Mathf.Cos(_wallJumpAngleRadiant), Mathf.Sin(_wallJumpAngleRadiant));

                JumpAction(_direction);
            }
        }
    }
}