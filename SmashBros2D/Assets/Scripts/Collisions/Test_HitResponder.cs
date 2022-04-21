// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class Test_HitResponder : MonoBehaviour, IHitResponder
    {
        [SerializeField] private GameObject character = null;
        private Manager manager ;

        [SerializeField] private bool m_attack;
        [SerializeField] private int m_damage = 10;
        [SerializeField] private Comp_Hitbox _hitbox;

        int IHitResponder.Damage {get => m_damage; }

        private void Start() {
            _hitbox.HitResponder = this;
            if (character != null)
            {
                manager = character.GetComponent<Manager>();
            }
        }

        private void Update() {
            m_attack = manager.input.RetrieveAttackInput();

            if (m_attack)
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
