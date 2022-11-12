using UnityEngine;

namespace SmashBros2D
{   
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerManager : CharacterManager
    {
        [SerializeField] private int _playerID = 0;
        public int playerID { get => _playerID ; }

        internal EnvironmentCollisionDetection detector ;

        private EnvironmentCollision _env;
        public  EnvironmentCollision env { get => _env ; }

        // A delegate event that will notify other scripts that subscribe to this event to run a method whenever this delegate is called.
        public delegate void UpdateDamageText(float amount, int playerID);
        public static event UpdateDamageText UpdateDamage;

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


        public override void AddDamage(float damage)
        {
            base.AddDamage(damage);
            UpdateDamage(damageRatio, _playerID);
        }


        public override void Die(Transform spawnPoint)
        {
            base.Die(spawnPoint);
            
            Debug.Log("Player respawn : " + new Vector2(transform.position.x, transform.position.y) );
            transform.position = spawnPoint.position;
            _receivedDamage = 0;
            UpdateDamage(damageRatio, _playerID);
        }

    }
}