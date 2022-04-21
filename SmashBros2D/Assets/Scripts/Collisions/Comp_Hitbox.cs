// using System.Diagnostics;
// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class Comp_Hitbox : MonoBehaviour, IHitDetector
    {
        [SerializeField] private BoxCollider2D m_collider    = null              ;
        [SerializeField] private LayerMask     m_LayerMask  ;
        [SerializeField] private HurtboxMask   m_hurtboxMask = HurtboxMask.Enemy ;

        public float m_thickness = 0.025f;
        private IHitResponder m_hitResponder;

        public IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

        public void CheckHit(HitData hitData)
        {
            Vector2 _scaledSize = new Vector2(
                m_collider.size.x * transform.lossyScale.x,
                m_collider.size.y * transform.lossyScale.y
            );

            float _distance = _scaledSize.y - m_thickness;
            Vector2 _direction = transform.up;
            Vector2 _center = transform.TransformPoint(m_collider.offset);
            Vector2 _start = _center - _direction * (_distance / 2);
            Vector2 _halfExtents = new Vector2( _scaledSize.x, m_thickness);
            float _angle = transform.rotation.z;

            HitData _hitdata = null;
            IHurtbox _hurtbox = null;
            RaycastHit2D[] _hits = Physics2D.BoxCastAll(_start, _halfExtents, _angle, _direction, _distance, m_LayerMask);
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
                            hitPoint = _hit.point == Vector2.zero ? _center : _hit.point,
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
    }
}