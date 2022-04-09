using UnityEngine;

namespace smash_bros
{
    public class Move : PlayerMovement
    {
        [SerializeField, Range(0f, 100f)] private float maxSpeed           = 4f  ;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration    = 35f ;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f ;

        private Vector2 direction       ;
        private Vector2 desiredVelocity ;
        
        private float maxSpeedChange ;
        private float acceleration   ;
        private bool  onGround       ;

        // private void Awake()
        // {
        //     body       = GetComponent<Rigidbody2D>();
        //     ground     = GetComponent<GroundCollision>();
        //     controller = GetComponent<Controller>();
        // }

        private void Update()
        {
            direction.x     = controller.input.RetrieveMoveInput();
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);
        }

        private void FixedUpdate()
        {
            onGround = ground.GetOnGround();
            velocity = body.velocity;

            acceleration   = onGround ? maxAcceleration : maxAirAcceleration;
            maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x     = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

            body.velocity = velocity;
        }
    }
}