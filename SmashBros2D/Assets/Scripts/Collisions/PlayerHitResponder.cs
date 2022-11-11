// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(PlayerHitbox))]
    public class PlayerHitResponder : MonoBehaviour, IHitResponder
    {
        [SerializeField] private AttackData _attackData  = null  ;

        internal bool isAttacking ;
        
        private PlayerHitbox  _hitbox  ;
        private PlayerManager _manager ;

        public AttackData attackData {get => _attackData; }

        private void Awake()
        {
            _manager = transform.parent.GetComponent<PlayerManager>();
            _hitbox  = GetComponent<PlayerHitbox>();
        }

        private void Start()
        {
            _hitbox.hitResponder = this;
            isAttacking = false;
        }

        private void Update()
        {
            if (isAttacking)
            {
                _hitbox.CheckHit(null);
            }
        }

        public bool CheckHit(HitData data)
        {
            // Debug.Log("PlayerHitResponder.CheckHit");
            return true;
        }

        public void Response(HitData data)
        {
            // Debug.Log("PlayerHitResponder.Response");
            isAttacking = false;
        }
    }
}
