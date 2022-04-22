// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class Comp_Hitbox : MonoBehaviour, IHitDetector
    {
        [Header("Hitbox Settings")]
        [SerializeField, Range(  0f,  1f)] private float radius   = .5f ;
        [SerializeField, Range(  0f,  5f)] private float distance = .5f ;
        [SerializeField, Range(-90f, 90f)] private float angle    =  0f ;
        
        [Header("Target Settings")]
        [SerializeField] private LayerMask     m_LayerMask  ;
        [SerializeField] private HurtboxMask   m_hurtboxMask = HurtboxMask.Enemy ;

        [Header("Debug Settings")]
        [SerializeField] bool debugMode = true;

        private IHitResponder m_hitResponder;
        public  IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

        public void CheckHit(HitData hitData)
        {

            HitData  _hitdata = null;
            IHurtbox _hurtbox = null;

            Vector2 _direction = new Vector2(transform.lossyScale.x * Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f));

            RaycastHit2D[] _hits = Physics2D.CircleCastAll(transform.position, radius, _direction, (distance + radius), m_LayerMask);
            foreach (RaycastHit2D _hit in _hits)
            {
                _hurtbox = _hit.collider.GetComponent<IHurtbox>();
                if (_hurtbox != null)
                {
                    if (m_hurtboxMask.HasFlag((HurtboxMask)_hurtbox.Type))
                    {
                        // Generate Hitdata
                        _hitdata = new HitData
                        {
                            damage = m_hitResponder == null ? 0 : m_hitResponder.Damage,
                            hitPoint = _hit.point == Vector2.zero ? new Vector2(transform.position.x, transform.position.y) : _hit.point,
                            hitNormal = _hit.normal,
                            hurtbox = _hurtbox,
                            hitDetector = this
                        };

                        if (_hitdata.Validate())
                        {
                            _hitdata.hitDetector.HitResponder.Response(_hitdata);
                            _hitdata.hurtbox.HurtResponder.Response(_hitdata);
                        }
                    }
                    
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (debugMode)
            {
                Vector2 _direction = new Vector2(transform.lossyScale.x * Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f));
                Gizmos.DrawWireSphere(transform.position, radius);
                Gizmos.DrawWireSphere(transform.position + (new Vector3(_direction.x, _direction.y, 0f) * distance), radius);
            }
        }
    }
}