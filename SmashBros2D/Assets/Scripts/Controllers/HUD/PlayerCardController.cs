using UnityEngine;
using UnityEngine.UI;

namespace SmashBros2D
{
    public class PlayerCardController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerManager _playerManger     = null ;

        [Header("HUD")]
        [SerializeField] private Text _playerNameText   = null ;
        [SerializeField] private Text _playerDamageText = null ;

        [Header("Color Settings")]
        [SerializeField] private Color _lowDamageColor     = new Color(0.2196079f,.2196079f, .2196079f, 1f);
        [SerializeField] private Color _mediumDamageColor  = new Color(255/255f,  211f/255f,   1f/255f, 1f);
        [SerializeField, Range(0f, 1f)] private float _mediumDamageLimit = .6f ;
        [SerializeField] private Color _highDamageColor    = new Color(214f/255f,  31f/255f,  31f/255f, 1f);
        [SerializeField, Range(0f, 1f)] private float _highDamageLimit   = .85f  ;
        
        private int   _playerID ;

        void Awake()
        {
            _playerNameText.text = _playerManger.stats.characterName ;
            _playerID            = _playerManger.playerID   ;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            PlayerManager.UpdateDamage += UpdateDamageText;
            UpdateDamageText(0f, _playerID);
        }
        
        private void UpdateDamageText(float amount, int playerID)
        {
            if (_playerID == playerID)
            {
                _playerDamageText.text = amount.ToString("0.%");

                if (amount >= _highDamageLimit)
                {
                    _playerDamageText.color = _highDamageColor;
                }
                else if (amount >= _mediumDamageLimit)
                {
                    _playerDamageText.color = _mediumDamageColor;
                }
                else
                {
                    _playerDamageText.color = _lowDamageColor;
                }
                
            }
        }
    }
}
