using UnityEngine;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "CharacterStatistics", menuName = "Data/CharacterStatistics")]
    public class CharacterStatistics : ScriptableObject
    {
        [SerializeField] private string _characterName = "Warrior";

        [SerializeField, Range(0f  , 500f)] private float _maxDamage          = 100f;
        [SerializeField, Range(0.1f,  10f)] private float _maxAttackFrequency =   2f;

        public string characterName     { get => _characterName;             }
        public float  maxDamage         { get => _maxDamage;                 }
        public float  minNextAttackTime { get => (1f / _maxAttackFrequency); }
    }
}
