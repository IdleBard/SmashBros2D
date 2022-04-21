using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Hitbox : MonoBehaviour, IHitDetector
{
    [SerializeField] private BoxCollider m_collider;
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private HurtboxMask m_hurtboxMask = HurtboxMask.Enemy;

    public float m_thickness = 0.025f;
    private IHitResponder m_hitResponder;

    public IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

    public void CheckHit(HitData hitData)
    {
        Vector3 _scaledSize = new Vector3(
            m_collider.size.x * transform.lossyScale.x,
            m_collider.size.y * transform.lossyScale.y,
            m_collider.size.z * transform.lossyScale.z
        );

        float _distance = _scaledSize.y - m_thickness;
        Vector3 _direction = transform.up;
        Vector3 _center = transform.TransformPoint(m_collider.center);
        Vector3 _start = _center - _direction * (_distance / 2);
        Vector3 _halfExtents = new Vector3( _scaledSize.x, m_thickness, _scaledSize.z / 2);
        Quaternion _orientation = transform.rotation;

        HitData _hitdata = null;
        IHurtbox _hurtbox = null;
        RaycastHit[] _hits = Physics.BoxCastAll(_start, _halfExtents, _direction, _orientation, _distance, m_LayerMask);
        foreach (RaycastHit _hit in _hits)
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
                        hitPoint = _hit.point == Vector3.zero ? _center : _hit.point,
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
