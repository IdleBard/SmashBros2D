using UnityEngine;

namespace SmashBros2D
{
    public class Walk : Movement
    {

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
        
        // Update is called once per frame
        private void Update()
        {
            direction.x     = manager.input.RetrieveMoveInput();
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - manager.ground.GetFriction(), 0f);

        }

        // FixedUpdate is called every fixed frame-rate frame
        private void FixedUpdate()
        {
            velocity = manager.body.velocity;

            acceleration   = manager.onGround ? maxAcceleration : maxAirAcceleration;
            maxSpeedChange = acceleration * Time.fixedDeltaTime;
            velocity.x     = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

            if (manager.onWall)
            {
                if (timeWallStick < maxTimeWallStick)
                {
                    timeWallStick += Time.fixedDeltaTime;
                    velocity.x = manager.body.velocity.x;
                }
            }
            else
            {
                timeWallStick = 0;
            }

            manager.body.velocity = velocity ;
                        
        }
    }
}