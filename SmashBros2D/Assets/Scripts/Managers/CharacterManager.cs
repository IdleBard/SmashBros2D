using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{  
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] internal InputController    input = null ;
        [SerializeField] internal PlayerStatistics   stats = null ;

        internal Rigidbody2D     body   ;
        internal GroundCollision ground ;

        protected float _receivedDamage;

        internal float damageRatio { get => (_receivedDamage / stats.maxDamage) ;}

        // Start is called before the first frame update
        private void Awake()
        {
            body       = GetComponent<Rigidbody2D>();
            ground     = GetComponent<GroundCollision>();
        }

        void Start()
        {
            _receivedDamage = 0;
        }

        public void addDamage(float damage)
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
