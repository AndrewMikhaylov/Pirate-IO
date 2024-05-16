using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyLogic
{
    [CreateAssetMenu]
    public class EnemyData: ScriptableObject
    {
        public float Health;
        public float Speed;
        public float Damage;
        public float DamageFrequency;
        public float SpawnTimePeriodStart;
        public float SpawnTimePeriodEnd;
        public float PushBackSpeed;
        public GameObject ExperiencePoint;

        public float MiniBossStatMultiplier;
        public GameObject MiniBossExperiencePoint;

    }
}