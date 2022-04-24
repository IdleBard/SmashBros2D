using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{  
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] internal InputController    input = null ;
        [SerializeField] internal PlayerStatistics   stats = null ;

        protected float _receivedDamage;

        internal float damageRatio { get => (_receivedDamage / stats.maxDamage) ;}

        void Start()
        {
            _receivedDamage = 0;
        }

        public void addDamage(float damage)
        {
            _receivedDamage += damage;
        }
    }
}
