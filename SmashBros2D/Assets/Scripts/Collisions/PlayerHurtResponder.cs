using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class PlayerHurtResponder : MonoBehaviour, IHurtResponder
    {
        private Rigidbody2D         _body;
        private List<PlayerHurtbox> _hurtboxes = new List<PlayerHurtbox>();

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();

            _hurtboxes = new List<PlayerHurtbox>(GetComponentsInChildren<PlayerHurtbox>());
            foreach (PlayerHurtbox _hurtbox in _hurtboxes)
            {
                _hurtbox.hurtResponder = this;
            }
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
