using UnityEngine;

namespace SmashBros2D
{   
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerManager : CharacterManager
    {
        

        internal EnvironmentCollisionDetection detector ;

        private EnvironmentCollision _env;
        public  EnvironmentCollision env { get => _env ; }

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            detector   = GetComponent<EnvironmentCollisionDetection>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (detector != null)
            {
                _env = detector.env;
            }
        }

    }
}