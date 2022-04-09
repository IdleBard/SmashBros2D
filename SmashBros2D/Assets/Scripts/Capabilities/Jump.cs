using UnityEngine;

namespace smash_bros
{
    public class Jump : PlayerMovement
    {
        // protected Controller      controller ;
        // protected Rigidbody2D     body       ;
        // protected GroundCollision ground     ;

        // protected Vector2         velocity   ;

        [SerializeField, Range(0f, 10f)] private float jumpHeight                 = 3f   ;
        [SerializeField, Range(0, 5)]    private int   maxAirJumps                = 0    ;
        [SerializeField, Range(0f, 5f)]  private float downwardMovementMultiplier = 3f   ;
        [SerializeField, Range(0f, 5f)]  private float upwardMovementMultiplier   = 1.7f ;

        private int   jumpPhase           ;
        private float defaultGravityScale ;

        private bool desiredJump ;
        private bool onGround    ;


        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();

            defaultGravityScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            desiredJump |= controller.input.RetrieveJumpInput();
        }

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

            if (body.velocity.y > 0)
            {
                body.gravityScale = upwardMovementMultiplier;
            }
            else if (body.velocity.y < 0)
            {
                body.gravityScale = downwardMovementMultiplier;
            }
            else if(body.velocity.y == 0)
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
                float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
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