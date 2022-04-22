// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class PlayerHurtbox : MonoBehaviour, IHurtbox
    {
        [SerializeField] private bool        _active      = true;
        [SerializeField] private GameObject  _owner       = null;
        [SerializeField] private HurtboxType _hurtboxType = HurtboxType.Enemy;

        private IHurtResponder m_hurtResponder;
        
        public bool           active        { get => _active;      }
        public GameObject     owner         { get => _owner;       }
        // public Transform      transform     { get => transform;     }
        public HurtboxType    type          { get => _hurtboxType; }
        public IHurtResponder hurtResponder { get => m_hurtResponder; set => m_hurtResponder = value; }
        
        bool IHurtbox.CheckHit(HitData hitData)
        {
            if (m_hurtResponder == null)
            {
                Debug.Log("No Responder");
            }
            return true;
        }
    }
}
