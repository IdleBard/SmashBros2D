using UnityEngine;

namespace smash_bros
{
    [RequireComponent(typeof(Manager))]
    public class Movement : MonoBehaviour
    {
        protected Manager         manager    ;

        protected Vector2         velocity   ;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            manager    = GetComponent<Manager>();
        }

    }
}