// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class BlockPlayerCollision : MonoBehaviour
    {
        private Collider2D[] characterColliders;
        private Collider2D   characterBlockerCollider;

        void Awake()
        {
            characterBlockerCollider = GetComponent<Collider2D>();
            characterColliders       = transform.parent.GetComponents<Collider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Collider2D characterCollider in characterColliders)
                Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
        }
    }
}
