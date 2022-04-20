using UnityEngine;
using System.Collections;

namespace smash_bros
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private GameObject target;

        [SerializeField, Range(0f, 10f)] private float verticalOffset     =  1f;
        [SerializeField, Range(0f, 10f)] private float lookAheadDstX      =  3f;
        [SerializeField, Range(0f,  1f)] private float lookSmoothTimeX    = .5f;
        [SerializeField, Range(0f,  1f)] private float verticalSmoothTime = .2f;
        [SerializeField] private Vector2 focusAreaSize = new Vector2(5f, 5f);

        [SerializeField] private bool debugMode = false;

        FocusArea focusArea;
        private Collider2D  collider;
        private Rigidbody2D body;

        float currentLookAheadX;
        float targetLookAheadX;
        float lookAheadDirX;
        float smoothLookVelocityX;
        float smoothVelocityY;

        bool lookAheadStopped;
        
        void Awake()
        {
            collider = target.GetComponent<BoxCollider2D>();
            body     = target.GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            focusArea = new FocusArea (collider.bounds, focusAreaSize);
        }

        void LateUpdate()
        {
            focusArea.Update (collider.bounds);

            Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

            if (focusArea.velocity.x != 0)
            {
                lookAheadDirX = Mathf.Sign (focusArea.velocity.x);

                if (Mathf.Sign(body.position.x) == Mathf.Sign(focusArea.velocity.x) && body.position.x != 0)
                {
                    lookAheadStopped = false;
                    targetLookAheadX = lookAheadDirX * lookAheadDstX;
                }
                else
                {
                    if (!lookAheadStopped)
                    {
                        lookAheadStopped = true;
                        targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX)/4f;
                    }
                }
            }


            currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

            focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
            focusPosition  += Vector2.right * currentLookAheadX;

            transform.position = (Vector3)focusPosition + Vector3.forward * -10;
        }

        void OnDrawGizmos()
        {
            if (debugMode)
            {
                Gizmos.color = new Color (1, 0, 0, .5f);
                Gizmos.DrawCube (focusArea.center, focusAreaSize);
            }
        }


        struct FocusArea
        {

            public Vector2 center;
            public Vector2 velocity;

            float left,right;
            float top,bottom;

            public FocusArea(Bounds targetBounds, Vector2 size)
            {
                left   = targetBounds.center.x - size.x/2;
                right  = targetBounds.center.x + size.x/2;
                bottom = targetBounds.min.y;
                top    = targetBounds.min.y + size.y;

                velocity = Vector2.zero;
                center = new Vector2((left+right)/2,(top +bottom)/2);
            }

            public void Update(Bounds targetBounds)
            {
                
                float shiftX = 0;
                float shiftY = 0;

                if (targetBounds.min.x < left)
                {
                    shiftX = targetBounds.min.x - left;
                }
                else if (targetBounds.max.x > right)
                {
                    shiftX = targetBounds.max.x - right;
                }

                left  += shiftX;
                right += shiftX;

                if (targetBounds.min.y < bottom)
                {
                    shiftY = targetBounds.min.y - bottom;
                }
                else if (targetBounds.max.y > top)
                {
                    shiftY = targetBounds.max.y - top;
                }

                top    += shiftY;
                bottom += shiftY;

                center   = new Vector2((left+right)/2,(top +bottom)/2);
                velocity = new Vector2(shiftX, shiftY);
            }
        }

    }
}