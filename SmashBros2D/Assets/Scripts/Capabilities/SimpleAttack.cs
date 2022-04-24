using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    public class SimpleAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackPoint ;

        private  PlayerHitResponder _hitResponder ;

        void Awake()
        {
            _hitResponder = attackPoint.GetComponent<PlayerHitResponder>();
        }

        void StartAttacking()
        {
            _hitResponder.isAttacking = true;
        }

        void EndAttacking()
        {
            _hitResponder.isAttacking = false;
        }
    }
}
