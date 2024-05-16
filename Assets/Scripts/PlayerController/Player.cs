using System;
using Abilities;
using Abilities.LevelUp;
using EnemyLogic;
using GameLogic;
using GameLogic.ObserverPattern;
using PlayerController.ExperienceLogic;
using UnityEngine;

namespace PlayerController
{
    public class Player : Subject
    {
        public static Player Instance;
        [SerializeField] private PlayerData _playerData;
        private MovementController _movementController;
        private AbilityManager _abilityManager;
        private PlayerHealthManager _playerHealthManager;
        private ExperienceManager _experienceManager;
        
        private void Start()
        {
            _abilityManager = gameObject.GetComponent<AbilityManager>();
            _movementController = new MovementController(_playerData.MoveSpeed);
            _playerHealthManager = new PlayerHealthManager(_playerData.MaxHealth);
            _experienceManager = new ExperienceManager(_playerData.PickUpRadius, _playerData.FirstLevelMaxExperience, _playerData.NewLevelExpIncreaseAmount);
            _experienceManager.OnLevelUp += LevelUp;
            EnemyBehaviour.OnMiniBossDeath += MiniBossKill;
        }

        private void Update()
        {

            if (!GameManager.IsPaused)
            {
                _abilityManager.UseAbilities();
                _movementController.MovePlayer(transform);   
                _experienceManager.ScanRadiusForExperience(transform.position);
            }
        }

        public AbilityManager GetAbilityManager()
        {
            return _abilityManager;
        }
        public void TakeExperience(float expAmount)
        {
            _experienceManager.AddExperience(expAmount);
            Debug.Log("current exp = "+ _experienceManager.GetCurrentExp());
        }

        public void TakeDamage(float damageAmount)
        {
            _playerHealthManager.TakeDamage(damageAmount);
        }

        public void ImplementPlayerTraitLevelUp(PlayerLevelUp playerLevelUp)
        {
            switch (playerLevelUp.CharacterPassiveTraitType)
            {
                case CharacterPassiveTraitType.Health:
                    _playerHealthManager.UpMaxHealth(playerLevelUp.ChangeAmount);
                    break;
                case CharacterPassiveTraitType.Speed:
                    _movementController.UpdateSpeed(playerLevelUp.ChangeAmount);
                    break; 
                case CharacterPassiveTraitType.PickUpRadius:
                    _experienceManager.UpdatePickUpRadius(playerLevelUp.ChangeAmount);
                    break;
            }
        }

        public bool IsWeaponInventoryFull()
        {
            return _abilityManager.GetCurrentPlayerAbilities().Count == 2;
        }
        
        public void ImplementAbilityLevelUp(AbilityLevelUp abilityLevelUp)
        {
            _abilityManager.LevelUpAbility(abilityLevelUp);
        }

        public void ImplementAbilityEvolve(AbilityEvolve abilityEvolve)
        {
            _abilityManager.EvolveAbility(abilityEvolve);
        }

        public void ImplementAbilityAdd(AbilityToAdd abilityToAdd)
        {
            _abilityManager.AddAbility(abilityToAdd);
        }

        private void MiniBossKill()
        {
            NotifyAll(this, true);
        }
        private void LevelUp()
        {
            NotifyAll(this, false);
        }
    }
}
