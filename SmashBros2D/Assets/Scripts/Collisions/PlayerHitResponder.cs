// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(PlayerHitbox))]
    public class PlayerHitResponder : MonoBehaviour, IHitResponder
    {
        [SerializeField] private bool       _isAttacking = false ;
        [SerializeField] private AttackData _attackData  = null  ;
        
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
        }

        public void Attack()
        {
            _hitbox.CheckHit(null);
        }

        // private void Update()
        // {
        //     isAttacking = _manager.isAttacking;
        //     // isAttacking = _manager.input.RetrieveAttackInput();

        //     // if (isAttacking)
        //     // {
        //     //     _hitbox.CheckHit(null);
        //     // }
        // }

        bool IHitResponder.CheckHit(HitData data)
        {
            return true;
        }

        void IHitResponder.Response(HitData data)
        {

        }
    }
}
