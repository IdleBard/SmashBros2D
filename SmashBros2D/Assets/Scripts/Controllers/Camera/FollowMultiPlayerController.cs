using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "FollowMultiPlayerController", menuName = "Controller/Camera/FollowMultiPlayer")]
    public class FollowMultiPlayerController : CameraController
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

        private MultiFocusArea   _focusArea ;
        // private GameObject       _target    ;
        private GameObject[]     _targets   ;
        private List<Collider2D> _colliders ;

        private float _smoothVelocityX ;
        private float _smoothVelocityY ;
        private float _smoothZoom      ;


        public override void SetTarget(string targetTag)
        {
            _targets    = GameObject.FindGameObjectsWithTag(targetTag);
            _colliders  = new List<Collider2D>();

            foreach (GameObject _target in _targets)
            {
                Collider2D _collider  = _target?.GetComponent<Collider2D>();

                if (_collider != null)
                {
                    _colliders.Add(_collider);
                }
            }

            _focusArea = new MultiFocusArea (_relativeFocusSize, _screen, 5f, 20f);

            _smoothVelocityX = 0f;
            _smoothVelocityY = 0f;
            _smoothZoom      = 0f;

        }


        public override Vector2 Follow(Vector3 cameraPosition)
        {
            if (_targets == null || _targets.Length == 0)
            {
                return Vector3.zero;
            }

            _focusArea.Update (_colliders);

            Vector2 _focusPosition = _focusArea.cameraPosition;

            _focusPosition.x = Mathf.SmoothDamp (cameraPosition.x, _focusArea.cameraPosition.x, ref _smoothVelocityX , xDamping);
            _focusPosition.y = Mathf.SmoothDamp (cameraPosition.y, _focusArea.cameraPosition.y, ref _smoothVelocityY , yDamping);

            Camera.main.orthographicSize = Mathf.SmoothDamp (Camera.main.orthographicSize, _focusArea.cameraOrthographicSize, ref _smoothZoom , xDamping);

            return _focusPosition;
        }


        public override void OnDrawGizmos()
        {
            Gizmos.color = new Color (1, 0, 0, .5f);
            Gizmos.DrawCube (_focusArea.center, _focusArea.size);
        }
    }

    public struct MultiFocusArea
    {
        public Vector2 center         { get => new Vector2( _screenBounds.center.x , _screenBounds.center.y ) ; }
        public Vector2 size           { get => new Vector2( _screenBounds.size.x   , _screenBounds.size.y   ) ; }
        public Vector2 cameraPosition { get => center - _screenOffset ; }
        public Vector2 velocity ;
        public float   cameraOrthographicSize { get => _cameraOrthographicSize ; }
        public float   zoom ;

        private Bounds  _screenBounds           ;
        private Vector2 _relativeSize           ;
        private Vector2 _relativeScreenOffset   ;
        private float   _cameraOrthographicSize ;
        private float   _minOrthographicSize    ;
        private float   _maxOrthographicSize    ;

        private Vector2 _screenOffset { get => new Vector2((_relativeScreenOffset.x - 0.5f) * 2f * Camera.main.orthographicSize * Camera.main.aspect, (_relativeScreenOffset.y - 0.5f) * 2f * Camera.main.orthographicSize) ; }


        public MultiFocusArea(Vector2 relativeSize , Vector2 relativeScreenOffset, float minOrthographicSize, float maxOrthographicSize)
        {

            _screenBounds = new Bounds(Vector3.zero, new Vector3(relativeSize.x * 2f * Camera.main.orthographicSize * Camera.main.aspect, relativeSize.y * 2f * Camera.main.orthographicSize, 0f));

            _relativeSize           = relativeSize ;
            _relativeScreenOffset   = relativeScreenOffset ;
            _cameraOrthographicSize = Camera.main.orthographicSize ;
            _minOrthographicSize    = minOrthographicSize ;
            _maxOrthographicSize    = maxOrthographicSize ;

            velocity      = Vector2.zero ;
            zoom          = 0f           ;

        }

        public void Update(List<Collider2D> colliders)
        {
            Bounds mergedBounds = getMergedBounds(colliders);

            velocity = (Vector2) ( mergedBounds.center - _screenBounds.center );

            if ( (_relativeSize.y * mergedBounds.size.y) > (_relativeSize.x *  mergedBounds.size.x  * Camera.main.aspect) )
            {
                _cameraOrthographicSize = mergedBounds.size.y / (_relativeSize.y * 2f);
                zoom     = _cameraOrthographicSize - Camera.main.orthographicSize ;
            }
            else
            {
                _cameraOrthographicSize = mergedBounds.size.x / (_relativeSize.x * Camera.main.aspect * 2f);
                zoom     = _cameraOrthographicSize - Camera.main.orthographicSize ;
            }

            if (_cameraOrthographicSize < _minOrthographicSize)
            {
                _cameraOrthographicSize = _minOrthographicSize ;
                
            }
            else if (_cameraOrthographicSize > _maxOrthographicSize)
            {
                _cameraOrthographicSize = _maxOrthographicSize ;
            }

            _screenBounds = new Bounds(mergedBounds.center, new Vector3(_relativeSize.x * 2f * _cameraOrthographicSize * Camera.main.aspect, _relativeSize.y * 2f * _cameraOrthographicSize, 0f));
            
            zoom     = _cameraOrthographicSize - Camera.main.orthographicSize ;
        }

        private Bounds getMergedBounds(List<Collider2D> colliders)
        {

            List<Bounds> _targetsBounds = new List<Bounds>() ;
            Bounds       _mergedBounds  = new Bounds()       ;

            foreach (Collider2D collider in colliders)
            {
                _targetsBounds.Add(collider.bounds);
            }

            Vector3 _min = _targetsBounds[0].min ;
            Vector3 _max = _targetsBounds[0].max ;

            foreach(Bounds b in _targetsBounds)
            {
                if (b.min.x < _min.x)
                {
                    _min.x = b.min.x ;
                }

                if (b.max.x > _max.x)
                {
                    _max.x = b.max.x ;
                }

                if (b.min.y < _min.y)
                {
                    _min.y = b.min.y ;
                }

                if (b.max.y > _max.y)
                {
                    _max.y = b.max.y ;
                }
            }

            _mergedBounds.SetMinMax(_min, _max);

            return _mergedBounds;
        }
    }
}