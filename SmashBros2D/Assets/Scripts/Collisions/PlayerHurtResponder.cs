using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    [RequireComponent(typeof(PlayerHurtbox))]
    public class PlayerHurtResponder : MonoBehaviour, IHurtResponder
    {
        [SerializeField] private Rigidbody2D   _body    ;
        [SerializeField] private PlayerHurtbox _hurtbox ;
        // private List<PlayerHurtbox> _hurtboxes = new List<PlayerHurtbox>();

        private void Awake()
        {
            // _hurtboxes = new List<PlayerHurtbox>(GetComponents<PlayerHurtbox>());
            // foreach (PlayerHurtbox _hurtbox in _hurtboxes)
            // {
            //     _hurtbox.hurtResponder = this;
            // }
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
