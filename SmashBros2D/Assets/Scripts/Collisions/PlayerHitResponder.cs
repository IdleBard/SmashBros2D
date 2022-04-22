// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    [RequireComponent(typeof(PlayerHitbox))]
    public class PlayerHitResponder : MonoBehaviour, IHitResponder
    {
        [SerializeField] private bool         isAttacking = false ;
        [SerializeField] private int          damage      = 10 ;
        
        private PlayerHitbox _hitbox  ;
        private Manager      _manager ;

        int IHitResponder.Damage {get => damage; }

        private void Awake()
        {
            _manager = transform.parent.GetComponent<Manager>();
            _hitbox  = GetComponent<PlayerHitbox>();
        }

        private void Start()
        {
            _hitbox.hitResponder = this;
        }

        private void Update()
        {
            isAttacking = _manager.input.RetrieveAttackInput();

            if (isAttacking)
            {
                _hitbox.CheckHit(null);
            }
        }

        bool IHitResponder.CheckHit(HitData data)
        {
            return true;
        }

        void IHitResponder.Response(HitData data)
        {

        }
    }
}
