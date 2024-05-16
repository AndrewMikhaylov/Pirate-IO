using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu]
    public class GameSpawnerData : ScriptableObject
    {
        public float MiniBossSpawnPeriod;
        
        public float EarlyGameSpawnPeriod;
        public float EarlyGameTimeEnd;
        
        public float MiddleGameSpawnPeriod;
        public float MiddleGameTimeEnd;
        
        public float LateGameSpawnPeriod;
    }
}