using UnityEngine;
// using System.Collections;

namespace SmashBros2D
{
    public class CameraManager : MonoBehaviour
    {
        

        [SerializeField] private CameraController controller = null     ;
        [SerializeField] private string           targetTag  = "Player" ;
        
        [Header("Debug Settings")]
        [SerializeField] private bool debugMode = true;

        void Awake()
        {
             controller.SetTarget(targetTag);
        }

        void FixedUpdate()
        {
            Vector2 _nextPosition = controller.Follow(transform.position);
            transform.position = new Vector3(_nextPosition.x, _nextPosition.y, transform.position.z);
        }

        void OnDrawGizmos()
        {
            if (debugMode)
            {
                controller?.OnDrawGizmos();
            }
        }

    }
}