using System;
using System.Collections.Generic;
using Abilities;
using Abilities.LevelUp;
using GameLogic.ObserverPattern;
using PlayerController;

namespace GameLogic
{
    public class LevelUpManager : IPlayerObserver
    {
        public Action<BasicLevelUp> OnChoiceDetermined;
        
        private LevelUpStorer _levelUpStorer;
        private AbilityManager _currentAbilityManager;
        private LevelUpMenu _levelUpMenu;

        private BasicLevelUp _firstLevelUp, _secondLevelUp, _thirdLevelUp;
        
        public LevelUpManager(LevelUpStorer levelUpStorer, LevelUpMenu levelUpMenu)
        {
            _levelUpStorer = levelUpStorer;
            _levelUpMenu = levelUpMenu;
            _levelUpMenu.OnChoiceMade += DetermineLevelUp;
        }

        public void GetNewWeapons()
        {
            _firstLevelUp = _levelUpStorer.GetNotListedAbilities(_currentAbilityManager.GetCurrentPlayerAbilities())[0];
            _secondLevelUp = _levelUpStorer.GetNotListedAbilities(_currentAbilityManager.GetCurrentPlayerAbilities())[1];
            _thirdLevelUp = _levelUpStorer.GetNotListedAbilities(_currentAbilityManager.GetCurrentPlayerAbilities())[2];
        }


        public void GetRandomPlayerLevelUps()
        {
            PlayerLevelUp[] playerLevelUps = new PlayerLevelUp[2];
            for (int i = 0; i < playerLevelUps.Length; i++)
            {
                playerLevelUps[i] = _levelUpStorer.GetNeededCharacterLevelUp();
            }
            _firstLevelUp = playerLevelUps[0];
            _thirdLevelUp = playerLevelUps[1];
        }

        public void GetRandomAbilityLevelUp()
        {
            var currentAbilities = _currentAbilityManager.GetCurrentPlayerAbilities();
            Random random = new Random();
            var abilityToLevelUpNumber = random.Next(currentAbilities.Count);
            var abilityToLevelUp = currentAbilities[abilityToLevelUpNumber];
            if (abilityToLevelUp.CanBeLeveledUp() || !abilityToLevelUp.HasEvolvedStage)
            {
                var levelUpToReturn = _levelUpStorer.GetNeededAbilityLevelUp(abilityToLevelUp.AbilityType);
                _secondLevelUp = levelUpToReturn;
            }
            else
            {
                _secondLevelUp = _levelUpStorer.GetEvolvedAbility(abilityToLevelUp);   
            }
        }

        public void CreateChoiceMenu()
        {
            var randomPlayerLevelUps = new[] { _firstLevelUp, _thirdLevelUp };
            var randomAbilityLevelUp = _secondLevelUp;
            _levelUpMenu.InitializeOptions(randomPlayerLevelUps, randomAbilityLevelUp);
        }
        
        private void DetermineLevelUp(int levelUpNumber)
        {
            switch (levelUpNumber)
            {
                case 0:
                    OnChoiceDetermined.Invoke(_firstLevelUp);
                    break;
                case 1:
                    OnChoiceDetermined.Invoke(_secondLevelUp);
                    break;
                case 2:
                    OnChoiceDetermined.Invoke(_thirdLevelUp);
                    break;
            }
        }

        public void OnPlayerNotify(Player player, bool isMiniBossKill)
        {
            _currentAbilityManager = player.GetAbilityManager();
            if (isMiniBossKill)
            {
                GetNewWeapons();
            }
            else
            {
                GetRandomPlayerLevelUps();
                GetRandomAbilityLevelUp();   
            }
            CreateChoiceMenu();
        }
    }
}