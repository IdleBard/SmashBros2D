using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private Transform endPoint     ;
        [SerializeField, Range(0f, 5f)] private float     endTime  = 5f;

        internal Rigidbody2D     body   ;

        private Vector2 startPoint ;
        private Vector2 lastPoint  ;
        private Vector2 nextPoint  ;

        private float time;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }


        // Start is called before the first frame update
        void Start()
        {
            startPoint = this.transform.position;
            lastPoint  = startPoint ;
            nextPoint  = endPoint.position   ;
            time       = 0f;
        }

        // // Update is called once per frame
        // void Update()
        // {
            
        // }

        // FixedUpdate is called every fixed frame-rate frame
        void FixedUpdate()
        {
            time += Time.fixedDeltaTime;

            if (time <= endTime)
            {
                this.body.MovePosition( Vector2.Lerp(lastPoint, nextPoint, time/endTime ) );
            }
            else
            {
                Vector2 tmp = lastPoint ;
                lastPoint = nextPoint;
                nextPoint = tmp;

                time = 0f;
            }
        }
    }
}
