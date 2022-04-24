using UnityEngine;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "PlayerStatistics", menuName = "Data/PlayerStatistics")]
    public class PlayerStatistics : ScriptableObject
    {
        [SerializeField] private float _maxDamage = 100;

        public float maxDamage { get => _maxDamage; }
    }
}
