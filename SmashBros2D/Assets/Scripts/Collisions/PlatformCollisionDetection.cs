// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    public class PlatformCollisionDetection : MonoBehaviour
    {
        [SerializeField] private LayerMask    _layerMask ;
        [SerializeField] private List<string> _tagMask   ;

        private PlatformCollision _state = new PlatformCollision() ;
        public  PlatformCollision state { get => _state ; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsPlayer(collision.gameObject))
            {
                PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
                _state.AddPlayerID(player.playerID);
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (_state.playerCollisionID.Count > 0)
            {
                if (IsPlayer(collider.gameObject))
                {
                    PlayerManager player = collider.gameObject.GetComponent<PlayerManager>();
                    // Debug.Log(player.playerID);
                    _state.RemovePlayerID(player.playerID);
                }
            }
        }


        private bool IsPlayer(GameObject gameObject)
        {
            return (_layerMask.value & (1 << gameObject.layer)) > 0 && _tagMask.Contains(gameObject.tag);
        }
    }

    public class PlatformCollision
    {
        private List<int> _playerCollisionID = new List<int>() ;
        public List<int>  playerCollisionID { get => _playerCollisionID; }

        public void AddPlayerID(int playerID)
        {
            if (!_playerCollisionID.Contains(playerID))
            {
                _playerCollisionID.Add(playerID);
            }
            // else
            // {
            //     Debug.LogWarning("Player ID already in list (" + playerID + ")");
            // }
        }

        public void RemovePlayerID(int playerID)
        {
            if (_playerCollisionID.Contains(playerID))
            {
                _playerCollisionID.Remove(playerID);
            }
            else
            {
                Debug.LogWarning("Player ID not in list (" + playerID + ")");
            }
        }

        public void Clear()
        {
            _playerCollisionID.Clear();
        }
    }
}
