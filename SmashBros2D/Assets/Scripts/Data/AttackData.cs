using UnityEngine;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "Data/AttackData")]
    public class AttackData : ScriptableObject
    {
        [SerializeField, Range(  0f, 100f)] private float _damage = 20f;
        [SerializeField, Range(  0f, 100f)] private float _shift  = 10f;
        [SerializeField, Range(-90f,  90f)] private float _angle  = 45f;

        public float   damage    { get => _damage; }
        public float   shift     { get => _shift;  }
        public Vector2 direction { get => new Vector2( Mathf.Cos(_angle * Mathf.PI / 180f), Mathf.Sin(_angle * Mathf.PI / 180f) ); }
    }
}
