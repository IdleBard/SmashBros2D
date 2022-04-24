using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(PlayerHurtbox))]
    public class PlayerHurtResponder : MonoBehaviour, IHurtResponder
    {
        private Rigidbody2D   _body    ;
        private PlayerHurtbox _hurtbox ;

        private void Awake()
        {
            _hurtbox = GetComponent<PlayerHurtbox>();
            _hurtbox.hurtResponder = this;
            _body      = _hurtbox.owner.GetComponent<Rigidbody2D>();
        }

        bool IHurtResponder.CheckHit(HitData data)
        {
            return true;
        }

        void IHurtResponder.Response(HitData data)
        {
            Debug.Log("Hurt Response " + data.hitNormal * data.damage);
            _body.AddForce(-1 * data.hitNormal * data.damage, ForceMode2D.Impulse);
        }

    }
}
