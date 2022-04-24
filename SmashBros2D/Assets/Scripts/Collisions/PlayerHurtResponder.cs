using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(PlayerHurtbox))]
    public class PlayerHurtResponder : MonoBehaviour, IHurtResponder
    {
        private Rigidbody2D      _body    ;
        private CharacterManager _manager ;
        private PlayerHurtbox    _hurtbox ;

        private void Awake()
        {
            _hurtbox = GetComponent<PlayerHurtbox>();
            _hurtbox.hurtResponder = this;
            _body      = _hurtbox.owner.GetComponent<Rigidbody2D>();
            _manager   = _hurtbox.owner.GetComponent<CharacterManager>();
        }

        bool IHurtResponder.CheckHit(HitData data)
        {
            return true;
        }

        void IHurtResponder.Response(HitData data)
        {
            _manager.addDamage(data.damage);
            float _magnitude = data.damage * (1 + _manager.damageRatio);
            Vector2 _impulse = -1 * data.hitNormal * _magnitude;
            Debug.Log("Hurt Response " + _impulse + " : Damage " + _magnitude);
            _body.AddForce(_impulse, ForceMode2D.Impulse);
            
        }

    }
}
