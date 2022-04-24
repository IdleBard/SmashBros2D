// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    public class LevelBoundaries : MonoBehaviour
    {

        [SerializeField] private Transform spawnPoint;

        // void OnTriggerEnter2D(Collider2D collider)
        // {
        //     Debug.Log("Enter " + collider.tag);
        // }

        // void OnTriggerStay2D(Collider2D collider)
        // {
        //     Debug.Log("Enter " + collider.tag);
        // }

        void OnTriggerExit2D(Collider2D collider)
        {
            Debug.Log("Exit " + collider.tag);

            if (collider.tag == "Player")
            {
                Debug.Log("Player " + collider.gameObject);
                CharacterManager _manager = collider.gameObject.GetComponent<CharacterManager>();
                _manager.Die(spawnPoint);
            }
        }
    }
}
