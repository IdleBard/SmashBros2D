using UnityEngine;
using System.Collections;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "FollowPlayerController", menuName = "CameraController/FollowPlayerController")]
    public class FollowPlayerController : CameraController
    {


        [Header("Camera Controller Settings")]
        [SerializeField, Range(0f, 10f)] private float   verticalOffset     =  1f ;
        [SerializeField                ] private Vector2 focusAreaSize      = new Vector2(5f, 5f);
        [SerializeField, Range(0f, 10f)] private float   lookAheadDstX      =  3f ;
        [SerializeField, Range(0f,  1f)] private float   lookSmoothTimeX    = .5f ;
        [SerializeField, Range(0f,  1f)] private float   verticalSmoothTime = .2f ;

        [Header("Debug Settings")]
        [SerializeField] private bool debugMode = true;

        private GameObject  _target   ;
        private Collider2D  _collider ;

        private float _currentLookAheadX   ;
        private float _targetLookAheadX    ;
        private float _lookAheadDirX       ;
        private float _smoothLookVelocityX ;
        private float _smoothVelocityY     ;

        private bool _lookAheadStopped     ;


        public override void SetTarget(GameObject target)
        {
            _target    = target;
            _collider  = _target.GetComponent<BoxCollider2D>();
            _focusArea = new FocusArea (_collider.bounds, focusAreaSize);
        }


        public override Vector3 Follow(Vector3 cameraPosition)
        {
            if (_target == null)
            {
                return Vector3.zero;
            }

            _focusArea.Update (_collider.bounds);

            Vector2 _focusPosition = _focusArea.center + Vector2.up * verticalOffset;

            if (_focusArea.velocity.x != 0)
            {
                _lookAheadDirX = Mathf.Sign (_focusArea.velocity.x);

                if (Mathf.Sign(_target.transform.position.x) == Mathf.Sign(_focusArea.velocity.x) && _target.transform.position.x != 0)
                {
                    _lookAheadStopped = false;
                    _targetLookAheadX = _lookAheadDirX * lookAheadDstX;
                }
                else
                {
                    if (!_lookAheadStopped)
                    {
                        _lookAheadStopped = true;
                        _targetLookAheadX = _currentLookAheadX + (_lookAheadDirX * lookAheadDstX - _currentLookAheadX)/4f;
                    }
                }
            }


            _currentLookAheadX = Mathf.SmoothDamp (_currentLookAheadX, _targetLookAheadX, ref _smoothLookVelocityX, lookSmoothTimeX);

            _focusPosition.y = Mathf.SmoothDamp (cameraPosition.y, _focusPosition.y, ref _smoothVelocityY, verticalSmoothTime);
            _focusPosition  += Vector2.right * _currentLookAheadX;

            return (Vector3)_focusPosition + Vector3.forward * -10;
        }


        void OnDrawGizmos()
        {
            if (debugMode)
            {
                Gizmos.color = new Color (1, 0, 0, .5f);
                Gizmos.DrawCube (_focusArea.center, focusAreaSize);
            }
        }

    }
}