using UnityEngine;
// using System.Collections;

namespace SmashBros2D
{
    public class CameraManager : MonoBehaviour
    {
        

        [SerializeField] private CameraController controller = null;
        [SerializeField] private GameObject       target     = null;

        void Awake()
        {
             controller.SetTarget(target);
        }

        void LateUpdate()
        {
            transform.position = controller.Follow(transform.position);
        }

    }
}