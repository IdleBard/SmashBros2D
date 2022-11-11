using UnityEngine;

namespace SmashBros2D
{  
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] internal InputController     input = null ;
        [SerializeField] internal CharacterStatistics stats = null ;

        internal Rigidbody2D                   body     ;

        protected float _receivedDamage;

        internal float damageRatio { get => (_receivedDamage / stats.maxDamage) ;}

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            body       = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            _receivedDamage = 0;
        }

        public void AddDamage(float damage)
        {
            _receivedDamage += damage;
        }

        public void Die(Transform spawnPoint)
        {
            Debug.Log("Player " + this.name + " is died.");
            body.velocity = Vector2.zero;
            transform.position = spawnPoint.position;
            _receivedDamage = 0;
            Debug.Log("Player respawn : " + new Vector2(transform.position.x, transform.position.y) );
        }
    }

}
