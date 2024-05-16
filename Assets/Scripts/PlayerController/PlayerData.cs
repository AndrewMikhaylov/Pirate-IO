using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerController
{
    [CreateAssetMenu]
    public class PlayerData: ScriptableObject
    {
        public float MaxHealth;
        public float MoveSpeed;
        public float PickUpRadius;
        public float FirstLevelMaxExperience;
        public float NewLevelExpIncreaseAmount;
    }
}