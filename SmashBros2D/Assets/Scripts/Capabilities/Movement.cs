using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(PlayerManager))]
    public class Movement : MonoBehaviour
    {
        protected PlayerManager _manager  ;
        protected Vector2       _velocity ;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            _manager    = GetComponent<PlayerManager>();
        }

    }
}