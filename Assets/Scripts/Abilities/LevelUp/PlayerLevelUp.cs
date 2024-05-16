using PlayerController.ExperienceLogic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities.LevelUp
{
    [CreateAssetMenu]
    public class PlayerLevelUp: BasicLevelUp
    {
        public CharacterPassiveTraitType CharacterPassiveTraitType;
    }
}