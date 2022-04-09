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
        [SerializeField, Range(0f, 10f)] private float jumpHeight                 = 3f   ;
        [SerializeField, Range(0, 5)]    private int   maxAirJumps                = 0    ;

        [Header("Gravity Multiplier Settings")]
        [SerializeField, Range(0f, 5f)]  private float defaultGravityScale   = 1f ;
        [SerializeField, Range(0f, 5f)]  private float fallGravityMultiplier = 3f ;
        [SerializeField, Range(0f, 5f)]  private float upGravityMultiplier   = 2f ;

        private int  jumpPhase   ;

        private bool desiredJump    ;
        private bool holdJumpButton ;
        private bool onGround       ;


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
            onGround = ground.GetOnGround();
            velocity = body.velocity;

            if (onGround)
            {
                jumpPhase = 0;
            }

            if (desiredJump)
            {
                desiredJump = false;
                JumpAction();
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

        private void JumpAction()
        {
            if (onGround || jumpPhase < maxAirJumps)
            {
                jumpPhase += 1;
                float jumpSpeed = Mathf.Sqrt(2f * defaultGravityScale * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
                
                if (velocity.y > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
                }
                else if (velocity.y < 0f)
                {
                    jumpSpeed += Mathf.Abs(body.velocity.y);
                }
                
                velocity.y += jumpSpeed;
            }
        }
    }
}