using UnityEngine;

namespace smash_bros
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
        
        private int  jumpPhase     ;
        private int  jumpBuffer    ;
        private int  coyoteTime    ;
        private int  wallJumpPhase ;

        // Collision bool
        // private bool onGround    ;
        // private bool onWall      ;
        // private bool onRightWall ;
        // private bool onLeftWall  ;
        // private int  wallSide    ;
        private int  oldWallSide ;


        // Input Bool
        private bool holdJumpButton ;
        private bool desiredJump    ;

        // Update is called once per frame
        void Update()
        {
            desiredJump   |= manager.input.RetrieveJumpInput();
            holdJumpButton = manager.input.RetrieveHoldJumpInput();
        }

        // FixedUpdate is called every fixed frame-rate frame
        private void FixedUpdate()
        {

            if (manager.onWall && manager.wallSide != oldWallSide)
            {
                wallJumpPhase = 0;
            }

            oldWallSide = manager.wallSide;          

            velocity = manager.body.velocity;

            if (manager.onGround)
            {
                jumpPhase     = 0;
                jumpBuffer    = 0;
                coyoteTime    = 0;
                wallJumpPhase = 0;
            }
            else
            {
                coyoteTime += 1;
            }

            if (desiredJump)
            {   
                
                if (manager.onGround || jumpPhase < maxAirJumps || coyoteTime < maxCoyoteTime)
                {
                    desiredJump = false;
                    JumpAction(Vector2.up);
                }
                else if(enableWallJump && manager.onWall)
                {   
                    desiredJump = false;
                    WallJumpAction();
                }
                else if (jumpBuffer < maxJumpBuffer)
                {
                    jumpBuffer += 1;
                    desiredJump = true;
                }
                else
                {
                    desiredJump = false;
                }
                
            }

            // Improve Jump
            if (manager.body.velocity.y < 0) // Faster Falling
            {
                manager.body.gravityScale = defaultGravityScale * fallGravityMultiplier;
            }
            else if (manager.body.velocity.y > 0 && !holdJumpButton) // Low Jump
            {
                manager.body.gravityScale = defaultGravityScale * upGravityMultiplier;
            }
            else
            {
                manager.body.gravityScale = defaultGravityScale;
            }

            manager.body.velocity = velocity;
        }

        private void JumpAction(Vector2 direction)
        {
            jumpPhase += 1;
            
            float   jumpSpeed    = Mathf.Sqrt(2f * defaultGravityScale * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
            Vector2 jumpVelocity = direction * jumpSpeed;
            
            if (velocity.y > 0f)
            {
                jumpVelocity.y = Mathf.Max(jumpVelocity.y - velocity.y, 0f);
            }
            else if (velocity.y < 0f)
            {
                jumpVelocity.y += Mathf.Abs(manager.body.velocity.y);
            }
            
            velocity += jumpVelocity;
        }

        private void WallJumpAction()
        {
            if (wallJumpPhase < maxWallJumps)
            {
                wallJumpPhase += 1;
                
                float wallJumpAngleRadiant = wallJumpAngle * Mathf.PI / 180f;
                Vector2 direction = new Vector2 (manager.wallSide * Mathf.Cos(wallJumpAngleRadiant),Mathf.Sin(wallJumpAngleRadiant));

                JumpAction(direction);
            }
        }
    }
}