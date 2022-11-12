using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    public class StickyPlatformTriggerDetection : MonoBehaviour
    {

        [SerializeField] private LayerMask    _layerMask ;
        [SerializeField] private List<string> _tagMask   ;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (IsPlayer(collider.gameObject))
            {
                collider.gameObject.transform.SetParent(transform);
            }
        }


        private void OnTriggerExit2D(Collider2D collider)
        {
            if (IsPlayer(collider.gameObject))
            {
                collider.gameObject.transform.SetParent(null);
            }
        }


        private bool IsPlayer(GameObject gameObject)
        {
            return (_layerMask.value & (1 << gameObject.layer)) > 0 && _tagMask.Contains(gameObject.tag);
        }
    }
}