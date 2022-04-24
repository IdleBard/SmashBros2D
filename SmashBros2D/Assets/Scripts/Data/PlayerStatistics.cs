using UnityEngine;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "PlayerStatistics", menuName = "Data/PlayerStatistics")]
    public class PlayerStatistics : ScriptableObject
    {
        [SerializeField, Range(0f  , 500f)] private float _maxDamage          = 100f;
        [SerializeField, Range(0.1f,  10f)] private float _maxAttackFrequency =   2f;

        public float maxDamage         { get => _maxDamage; }
        public float minNextAttackTime { get => (1f / _maxAttackFrequency); }
    }
}
