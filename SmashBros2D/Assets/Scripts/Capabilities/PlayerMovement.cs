using UnityEngine;

namespace smash_bros
{
    [RequireComponent(typeof(Controller))]
    public class PlayerMovement : MonoBehaviour
    {
        protected Controller      controller ;
        protected Rigidbody2D     body       ;
        protected GroundCollision ground     ;

        protected Vector2         velocity   ;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            body       = GetComponent<Rigidbody2D>();
            ground     = GetComponent<GroundCollision>();
            controller = GetComponent<Controller>();
        }

    }
}