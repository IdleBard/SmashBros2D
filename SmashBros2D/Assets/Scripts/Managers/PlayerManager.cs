using UnityEngine;

namespace SmashBros2D
{   
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerManager : CharacterManager
    {
        [SerializeField] internal PlayerHitResponder hitResponder ;

        internal EnvironmentCollisionDetection detector ;

        private EnvironmentCollision _env;
        public  EnvironmentCollision env { get => _env ; }

        internal bool isAttacking = false;

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

        public void Attack()
        {
            hitResponder.Attack();
        }



    }
}