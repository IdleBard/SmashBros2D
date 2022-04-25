using UnityEngine;
using System.Collections;

namespace SmashBros2D
{
    public abstract class CameraController : ScriptableObject
    {
        
        public abstract void    SetTarget(GameObject target);
        public abstract Vector2 Follow(Vector3 cameraPosition);
        public abstract void    OnDrawGizmos();

    }
}