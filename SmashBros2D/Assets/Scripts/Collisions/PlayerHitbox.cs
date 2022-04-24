// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    public class PlayerHitbox : MonoBehaviour, IHitDetector
    {
        [Header("Hitbox Settings")]
        [SerializeField, Range(  0f,  1f)] private float radius   = .5f ;
        [SerializeField, Range(  0f,  5f)] private float distance = .5f ;
        [SerializeField, Range(-90f, 90f)] private float angle    =  0f ;
        
        [Header("Target Settings")]
        [SerializeField] private LayerMask     layerMask  ;
        [SerializeField] private HurtboxMask   hurtboxMask = HurtboxMask.Enemy ;

        [Header("Debug Settings")]
        [SerializeField] bool debugMode = true;

        private IHitResponder _hitResponder;
        public  IHitResponder hitResponder { get => _hitResponder; set => _hitResponder = value; }

        public void CheckHit(HitData hitData)
        {

            HitData  _hitdata = null;
            IHurtbox _hurtbox = null;

            Vector2 _direction = new Vector2(transform.lossyScale.x * Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f));

            RaycastHit2D[] _hits = Physics2D.CircleCastAll(transform.position, radius, _direction, (distance + radius), layerMask);
            foreach (RaycastHit2D _hit in _hits)
            {
                _hurtbox = _hit.collider.GetComponent<IHurtbox>();
                if (_hurtbox != null)
                {
                    if (hurtboxMask.HasFlag((HurtboxMask)_hurtbox.type))
                    {
                        // Generate Hitdata
                        _hitdata = new HitData
                        {
                            attackData  = _hitResponder == null ? null : _hitResponder.attackData,
                            hitPoint    = _hit.point == Vector2.zero ? new Vector2(transform.position.x, transform.position.y) : _hit.point,
                            hitNormal   = _hit.normal,
                            hurtbox     = _hurtbox,
                            hitDetector = this
                        };

                        if (_hitdata.Validate())
                        {
                            _hitdata.hitDetector.hitResponder.Response(_hitdata);
                            _hitdata.hurtbox.hurtResponder.Response(_hitdata);
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