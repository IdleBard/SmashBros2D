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

        public virtual void AddDamage(float damage)
        {
            _receivedDamage += damage;
        }

        public virtual void Die(Transform spawnPoint)
        {
            Debug.Log("Player " + this.name + " is dead.");
            body.velocity = Vector2.zero;
            
            // Debug.Log("Player respawn : " + new Vector2(transform.position.x, transform.position.y) );
            // transform.position = spawnPoint.position;
            // _receivedDamage = 0;
        }
    }

}
