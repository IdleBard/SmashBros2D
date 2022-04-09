using UnityEngine;

namespace smash_bros
{
    public class Walk : PlayerMovement
    {
        // protected Controller      controller ;
        // protected Rigidbody2D     body       ;
        // protected GroundCollision ground     ;

        // protected Vector2         velocity   ;

        [Header("Walk Settings")]
        [SerializeField, Range(0f, 100f)] private float maxSpeed           = 4f  ;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration    = 35f ;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f ;

        [Header("Wall Stick")]
        [SerializeField, Range(0f, 1f)] private float maxTimeWallStick = .1f ;

        private float timeWallStick;

        private Vector2 direction       ;
        private Vector2 desiredVelocity ;
        
        private float maxSpeedChange ;
        private float acceleration   ;
        private bool  onGround       ;
        private bool  onWall         ;

        // Start is called before the first frame update
        // protected virtual void Awake()
        // {
        //     body       = GetComponent<Rigidbody2D>();
        //     ground     = GetComponent<GroundCollision>();
        //     controller = GetComponent<Controller>();
        // }

        private void Update()
        {
            animator.SetFloat("Speed", Mathf.Abs(velocity.x));
            direction.x     = controller.input.RetrieveMoveInput();
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        }

        private void FixedUpdate()
        {
            onGround = ground.GetOnGround();
            onWall   = ground.GetOnWall();

            velocity = body.velocity;

            acceleration   = onGround ? maxAcceleration : maxAirAcceleration;
            maxSpeedChange = acceleration * Time.fixedDeltaTime;
            velocity.x     = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

            if (onWall)
            {
                if (timeWallStick < maxTimeWallStick)
                {
                    timeWallStick += Time.fixedDeltaTime;
                    velocity.x = body.velocity.x;
                }
            }
            else
            {
                timeWallStick = 0;
            }

            body.velocity = velocity ;

            if (velocity.x > 0f)
            {
                renderer.flipX = true;
            }
            else if (velocity.x < 0f)
            {
                renderer.flipX = false;
            }
            
        }
    }
}