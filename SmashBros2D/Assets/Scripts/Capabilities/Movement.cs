using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(PlayerManager))]
    public class Movement : MonoBehaviour
    {
        protected PlayerManager manager  ;
        protected Vector2       velocity ;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            manager    = GetComponent<PlayerManager>();
        }

    }
}