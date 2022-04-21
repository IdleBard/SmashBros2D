// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class Comp_Hurtbox : MonoBehaviour, IHurtbox
    {
        [SerializeField] private bool m_active = true;
        [SerializeField] private GameObject m_owner = null;
        [SerializeField] private HurtboxType m_hurtboxType = HurtboxType.Enemy;
        private IHurtResponder m_hurtResponder;
        
        public bool Active { get => m_active; }
        public GameObject Owner { get => m_owner; }
        public Transform Transform { get => transform;}
        public HurtboxType Type { get => m_hurtboxType;}
        public IHurtResponder HurtResponder { get => m_hurtResponder; set => m_hurtResponder = value; }
        
        public bool CheckHit(HitData hitData)
        {
            if (m_hurtResponder == null)
            {
                Debug.Log("No Responder");
            }
            return true;
        }
    }
}
