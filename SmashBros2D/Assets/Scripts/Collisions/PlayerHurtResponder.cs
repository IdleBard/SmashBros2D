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
            
            float _magnitude = data.attackData.shift * (1 + _manager.damageRatio);
            // Vector2 _impulse = -1 * data.hitNormal * _magnitude;
            Vector2 _impulse =  data.attackData.direction * _magnitude;
            _impulse.x *= Mathf.Sign(-1 * data.hitNormal.x);

            // Debug.Log("Hurt Response " + _impulse + " : Shift " + _magnitude + " : Damage Percentage : " + _manager.damageRatio*100f + "%");
            _body.AddForce(_impulse, ForceMode2D.Impulse);
            
            _manager.AddDamage(data.attackData.damage);
        }

    }
}
