// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace smash_bros
{
    public class BlockPlayerCollision : MonoBehaviour
    {
        private Collider2D[] playerColliders;
        private Collider2D   playerBlockerCollider;

        void Awake()
        {
            playerBlockerCollider = GetComponent<Collider2D>();
            playerColliders       = transform.parent.GetComponents<Collider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Collider2D playerCollider in playerColliders)
                Physics2D.IgnoreCollision(playerCollider, playerBlockerCollider, true);
        }
    }
}
