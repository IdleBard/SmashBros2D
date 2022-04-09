using UnityEngine;

namespace smash_bros
{
    public class Jump : PlayerMovement
    {
        // protected Controller      controller ;
        // protected Rigidbody2D     body       ;
        // protected GroundCollision ground     ;

        // protected Vector2         velocity   ;

        [Header("Jump Settings")]
        [SerializeField, Range(0f, 10f)] private float jumpHeight            = 3f   ;
        [SerializeField, Range(0,  5)]   private int   maxAirJumps           = 0    ;
        [SerializeField, Range(0, 10)]   private int   maxJumpBuffer         = 5    ;
        [SerializeField, Range(0, 10)]   private int   maxCoyoteTime         = 5    ;
        [SerializeField ]                private bool  enableWallJump        = true ;
        [SerializeField, Range(0f, 90f)] private float wallJumpAngle         = 45f  ;

        [Header("Gravity Settings")]
        [SerializeField, Range(0f, 5f)]  private float defaultGravityScale   = 1f ;
        [SerializeField, Range(0f, 5f)]  private float fallGravityMultiplier = 3f ;
        [SerializeField, Range(0f, 5f)]  private float upGravityMultiplier   = 2f ;
        
        private int  jumpPhase   ;
        private int  jumpBuffer  ;
        private int  coyoteTime  ;

        // Collision bool
        private bool onGround    ;
        private bool onWall      ;
        private bool onRightWall ;
        private bool onLeftWall  ;
        private int  wallSide    ;


        // Input Bool
        private bool holdJumpButton ;
        private bool desiredJump    ;


        // Start is called before the first frame update
        // protected virtual void Awake()
        // {
        //     body       = GetComponent<Rigidbody2D>();
        //     ground     = GetComponent<GroundCollision>();
        //     controller = GetComponent<Controller>();
        // }

        // Update is called once per frame
        void Update()
        {
            desiredJump   |= controller.input.RetrieveJumpInput();
            holdJumpButton = controller.input.RetrieveHoldJumpInput();
        }

        // FixedUpdate is called every fixed frame-rate frame
        private void FixedUpdate()
        {
            onGround    = ground.GetOnGround();
            onWall      = ground.GetOnWall();
            onRightWall = ground.GetOnRightWall();
            onLeftWall  = ground.GetOnLeftWall();
            wallSide    = ground.GetWallSide();

            velocity = body.velocity;

            // if (onWall)
            // {
            //     Debug.Log("onRightWall : " + onRightWall + ", onLeftWall : " + onLeftWall + ", wallSide :" + wallSide);
            // }

            if (onGround)
            {
                jumpPhase  = 0;
                jumpBuffer = 0;
                coyoteTime = 0;
            }
            else
            {
                coyoteTime += 1;
            }

            if (desiredJump)
            {   
                
                if (onGround || jumpPhase < maxAirJumps || coyoteTime < maxCoyoteTime)
                {
                    desiredJump = false;
                    JumpAction(Vector2.up);
                }
                else if(enableWallJump && onWall)
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
            if (body.velocity.y < 0) // Faster Falling
            {
                body.gravityScale = defaultGravityScale * fallGravityMultiplier;
            }
            else if (body.velocity.y > 0 && !holdJumpButton) // Low Jump
            {
                body.gravityScale = defaultGravityScale * upGravityMultiplier;
            }
            else
            {
                body.gravityScale = defaultGravityScale;
            }

            body.velocity = velocity;
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
                jumpVelocity.y += Mathf.Abs(body.velocity.y);
            }
            
            velocity += jumpVelocity;
        }

        private void WallJumpAction()
        {
            float wallJumpAngleRadiant = wallJumpAngle * Mathf.PI / 180f;
            Vector2 direction = new Vector2 (wallSide * Mathf.Cos(wallJumpAngleRadiant),Mathf.Sin(wallJumpAngleRadiant));

            JumpAction(direction);
        }
    }
}