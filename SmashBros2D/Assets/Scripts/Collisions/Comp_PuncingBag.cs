using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class Comp_PuncingBag : MonoBehaviour, IHurtResponder
    {
        [SerializeField] private bool m_targetable = true;
        [SerializeField] private Transform m_targetTransform;
        [SerializeField] private Rigidbody2D m_rbBag;

        private List<Comp_Hurtbox> m_hurtboxes = new List<Comp_Hurtbox>();

        private void Start()
        {
            m_hurtboxes = new List<Comp_Hurtbox>(GetComponentsInChildren<Comp_Hurtbox>());
            foreach (Comp_Hurtbox _hurtbox in m_hurtboxes)
            {
                _hurtbox.HurtResponder = this;
            }
        }

        bool IHurtResponder.CheckHit(HitData data)
        {
            return true;
        }

        void IHurtResponder.Response(HitData data)
        {
            Debug.Log("Hurt Response");
        }

    }
}
