using System;
using System.Collections.Generic;
using Abilities.LevelUp;
using UnityEngine;

namespace Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] List<Ability> _currentPlayerAbilities;
        private Dictionary<Ability, float> _abilityToLastUsage;
        
        private void Start()
        {
            _abilityToLastUsage = new Dictionary<Ability, float>();
        }

        public void UseAbilities()
        {
            foreach (var ability in _currentPlayerAbilities)
            {
                if (_abilityToLastUsage.ContainsKey(ability))
                {
                    if (IsCoolDownReady(ability))
                    {
                        ability.ActivateAbility(gameObject.transform);
                        _abilityToLastUsage.Remove(ability);
                        _abilityToLastUsage.Add(ability, Time.time);
                    }   
                }
                else
                {
                    ability.SetUpAbility();
                    _abilityToLastUsage.Add(ability, Time.time);
                }
            }
        }

        public List<Ability> GetCurrentPlayerAbilities()
        {
            return _currentPlayerAbilities;
        }
        
        public void AddAbility(AbilityToAdd newAbility)
        {
            _currentPlayerAbilities.Add(newAbility.AbilityAction);
            newAbility.AbilityAction.SetUpAbility();   
        }

        public void LevelUpAbility(AbilityLevelUp abilityLevelUp)
        {
            var abilityToUpgrade = _currentPlayerAbilities.Find(ability => ability.AbilityType.Equals(abilityLevelUp.AbilityType));
            abilityToUpgrade.LevelUp(abilityLevelUp.GetThisAbilityChangeAmount(), abilityLevelUp.AbilityKeyWord);  
        }

        public void EvolveAbility(AbilityEvolve abilityEvolve)
        {
            _currentPlayerAbilities.Remove(abilityEvolve.OldAbilityAction);
            _currentPlayerAbilities.Add(abilityEvolve.NewAbilityAction);
        }

        private bool IsCoolDownReady(Ability ability)
        {
            _abilityToLastUsage.TryGetValue(ability, out var lastTimeUsed);
            if (ability.GetAbilityCooldown() > Time.time - lastTimeUsed)
            {
                return false;
            }
            return true;
        }
    }
}