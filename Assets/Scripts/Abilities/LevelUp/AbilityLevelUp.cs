using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities.LevelUp
{
    [CreateAssetMenu]
    public class AbilityLevelUp: BasicLevelUp
    {
        public float MaxChangeAmount;
        public AbilityType AbilityType;
        public AbilityKeyWord AbilityKeyWord;


        public float GetThisAbilityChangeAmount()
        {
            float result = Random.Range(ChangeAmount, MaxChangeAmount);
            return result;
        }
    }
}