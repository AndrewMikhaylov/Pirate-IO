using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Abilities.LevelUp
{
    [CreateAssetMenu]
    public class LevelUpStorer: ScriptableObject
    {
        [SerializeField] private List<AbilityToAdd> _abilitiesToAdd;
        
        [SerializeField] private List<AbilityEvolve> _evolveAbilitiesToAdd;
        
        [SerializeField] private List<AbilityLevelUp> _swordLevelUps;
        [SerializeField] private List<AbilityLevelUp> _shootLevelUps;
        [SerializeField] private List<AbilityLevelUp> _cannonLevelUps;
        [SerializeField] private List<AbilityLevelUp> _shipLevelUps;
        [SerializeField] private List<PlayerLevelUp> _characterLevelUps;


        public AbilityLevelUp GetNeededAbilityLevelUp(AbilityType abilityType)
        {
            Random random = new Random();
            int neededAbilityLevelUpNumber = 0;
            AbilityLevelUp neededLevelUp = default;
            switch (abilityType)
            {
                case AbilityType.Sword:
                    neededAbilityLevelUpNumber = random.Next(_swordLevelUps.Count);
                    neededLevelUp = _swordLevelUps[neededAbilityLevelUpNumber];
                    break;
                case AbilityType.Cannon:
                    neededAbilityLevelUpNumber = random.Next(_cannonLevelUps.Count);
                    neededLevelUp = _cannonLevelUps[neededAbilityLevelUpNumber];
                    break;
                case AbilityType.Shoot:
                    neededAbilityLevelUpNumber = random.Next(_shootLevelUps.Count);
                    neededLevelUp = _shootLevelUps[neededAbilityLevelUpNumber];
                    break;
                case AbilityType.Ship:
                    neededAbilityLevelUpNumber = random.Next(_shipLevelUps.Count);
                    neededLevelUp = _shipLevelUps[neededAbilityLevelUpNumber];
                    break;
            }

            return neededLevelUp;
        }

        public AbilityEvolve GetEvolvedAbility(Ability oldAbility)
        {
            AbilityEvolve newAbility = null;
            foreach (var evolveAbility in _evolveAbilitiesToAdd)
            {
                if (evolveAbility.AbilityType == oldAbility.AbilityType)
                {
                    newAbility = evolveAbility;
                }
            }

            return newAbility;
        }

        public AbilityToAdd[] GetNotListedAbilities(List<Ability> currentAbilities)
        {
            AbilityToAdd[] notListedAbilities = new AbilityToAdd[3];
            Ability ability = currentAbilities[0];
            int counter = 0;
            foreach (var abilityToAdd in _abilitiesToAdd)
            {
                if (abilityToAdd.AbilityAction != ability)
                {
                    notListedAbilities[counter] = abilityToAdd;
                    counter++;
                }
            }
            return notListedAbilities;
        }
        public PlayerLevelUp GetNeededCharacterLevelUp()
        {
            Random random = new Random();
            int neededAbilityLevelUpNumber = 0;
            neededAbilityLevelUpNumber = random.Next(_characterLevelUps.Count);
            return _characterLevelUps[neededAbilityLevelUpNumber];
        }
    }
}