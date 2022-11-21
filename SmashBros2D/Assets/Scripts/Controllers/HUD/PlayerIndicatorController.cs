using UnityEngine;
using UnityEngine.UI;

namespace SmashBros2D
{
    public class PlayerIndicatorController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerManager    _playerManger = null ;
        [SerializeField] private Transform     _playerTransform = null ;

        [Header("HUD")]
        [SerializeField] private Text _playerIndicatorText   = null ;
        

        void Awake()
        {
            _playerIndicatorText.text = "P" + _playerManger.playerID ;
        }

        void LateUpdate()
        {
            transform.localScale = new Vector3(_playerTransform.localScale.x, transform.localScale.y, transform.localScale.z);
        }


    }
}
