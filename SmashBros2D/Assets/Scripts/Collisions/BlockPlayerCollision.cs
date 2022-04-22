// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerCollision : MonoBehaviour
{
    public Collider2D characterCollider;
    public Collider2D characterBlockerCollider;

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }
}
