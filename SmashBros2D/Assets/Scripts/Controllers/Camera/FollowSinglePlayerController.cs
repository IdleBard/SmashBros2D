using UnityEngine;
using System.Collections;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "FollowSinglePlayerController", menuName = "Controller/Camera/FollowSinglePlayer")]
    public class FollowSinglePlayerController : CameraController
    {
        
        [Header("Position")]
        [SerializeField, Range(0f, 1f)] private float   screenX =  .5f ;
        [SerializeField, Range(0f, 1f)] private float   screenY =  .5f ;
        private Vector2 _screen { get => new Vector2(screenX, screenY);}

        [Header("Damping")]
        [SerializeField, Range(0f,  1f)] private float xDamping = .2f ;
        [SerializeField, Range(0f,  1f)] private float yDamping = .2f ;

        [Header("Dead Zone")]
        [SerializeField, Range(.2f,  1f)] private float deadZoneWidth  = .5f ;
        [SerializeField, Range(.2f,  1f)] private float deadZoneHeight = .5f ;
        private Vector2 _relativeFocusSize { get => new Vector2(deadZoneWidth, deadZoneHeight);}

        // [SerializeField, Range(0f, 10f)] private float   lookAheadDstX      =  3f ;

        private SingleFocusArea   _focusArea ;
        private GameObject        _target    ;
        private Collider2D        _collider  ;

        private float _smoothVelocityX ;
        private float _smoothVelocityY ;

        // private float _currentLookAheadX   ;
        // private float _targetLookAheadX    ;
        // private float _lookAheadDirX       ;
        // private float _smoothLookVelocityX ;
        // 

        // private bool _lookAheadStopped     ;

        public override void SetTarget(string targetTag)
        {
            _target        = GameObject.FindWithTag(targetTag);
            _collider      = _target.GetComponent<BoxCollider2D>();

            _focusArea = new SingleFocusArea  (_relativeFocusSize, _screen);

            _smoothVelocityX = 0f;
            _smoothVelocityY = 0f;

        }


        public override Vector2 Follow(Vector3 cameraPosition)
        {
            if (_target == null)
            {
                return Vector3.zero;
            }

            _focusArea.Update (_collider.bounds);

            Vector2 _focusPosition = _focusArea.cameraPosition;
            // Vector2 _focusPosition = _focusArea.center;

            // if (_focusArea.velocity.x != 0)
            // {
            //     _lookAheadDirX = Mathf.Sign (_focusArea.velocity.x);

            //     if (Mathf.Sign(_target.transform.position.x) == Mathf.Sign(_focusArea.velocity.x) && _target.transform.position.x != 0)
            //     {
            //         _lookAheadStopped = false;
            //         _targetLookAheadX = _lookAheadDirX * lookAheadDstX;
            //     }
            //     else
            //     {
            //         if (!_lookAheadStopped)
            //         {
            //             _lookAheadStopped = true;
            //             _targetLookAheadX = _currentLookAheadX + (_lookAheadDirX * lookAheadDstX - _currentLookAheadX)/4f;
            //         }
            //     }
            // }


            // _currentLookAheadX = Mathf.SmoothDamp (_currentLookAheadX, _targetLookAheadX, ref _smoothLookVelocityX, horizontalSmoothTime);

            // _focusPosition.y = Mathf.SmoothDamp (cameraPosition.y, _focusPosition.y, ref _smoothVelocityY, verticalSmoothTime);
            // _focusPosition  += Vector2.right * _currentLookAheadX;

            // return (Vector3)_focusPosition + Vector3.forward * -10;


            _focusPosition.x = Mathf.SmoothDamp (cameraPosition.x, _focusArea.cameraPosition.x, ref _smoothVelocityX , xDamping);
            _focusPosition.y = Mathf.SmoothDamp (cameraPosition.y, _focusArea.cameraPosition.y, ref _smoothVelocityY , yDamping);

            return _focusPosition;
        }


        public override void OnDrawGizmos()
        {
            Gizmos.color = new Color (1, 0, 0, .5f);
            Gizmos.DrawCube (_focusArea.center, _focusArea.size);
        }
    }

    public struct SingleFocusArea
    {
        public Vector2 center         { get => new Vector2( (_left + _right)/2f , (_top + _bottom)/2f ) ; }
        public Vector2 size           { get => new Vector2( _left - _right , _top - _bottom ) ; }
        public Vector2 cameraPosition { get => center - _screen ; }
        public Vector2 velocity ;

        private Vector2 _screen ;
        private float  _left, _right  ;
        private float  _top,  _bottom ;

        public SingleFocusArea(Vector2 relativeSize , Vector2 relativeScreen)
        {
            _screen.x = (relativeScreen.x - 0.5f) * 2f * Camera.main.orthographicSize  * Camera.main.aspect;
            _screen.y = (relativeScreen.y - 0.5f) * 2f * Camera.main.orthographicSize ;

            Vector2 _areaSize;
            _areaSize.x = relativeSize.x * 2f * Camera.main.orthographicSize * Camera.main.aspect;
            _areaSize.y = relativeSize.y * 2f * Camera.main.orthographicSize;

            _left   = Camera.main.pixelRect.center.x - _areaSize.x / 2f ;
            _right  = Camera.main.pixelRect.center.x + _areaSize.x / 2f ;

            _bottom = Camera.main.pixelRect.center.y - _areaSize.y / 2f;
            _top    = Camera.main.pixelRect.center.y + _areaSize.y / 2f;

            velocity = Vector2.zero;
        }

        public void Update(Bounds targetBounds)
        {
            
            float shiftX = 0;
            float shiftY = 0;

            if (targetBounds.min.x < _left)
            {
                shiftX = targetBounds.min.x - _left;
            }
            else if (targetBounds.max.x > _right)
            {
                shiftX = targetBounds.max.x - _right;
            }

            _left  += shiftX;
            _right += shiftX;

            if (targetBounds.min.y < _bottom)
            {
                shiftY = targetBounds.min.y - _bottom;
            }
            else if (targetBounds.max.y > _top)
            {
                shiftY = targetBounds.max.y - _top;
            }

            _top    += shiftY;
            _bottom += shiftY;

            // center   = new Vector2((_left+_right)/2,(_top +_bottom)/2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}