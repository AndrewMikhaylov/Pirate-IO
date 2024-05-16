using System;
using Abilities;
using Abilities.LevelUp;
using EnemyLogic;
using GameLogic.ObserverPattern;
using PlayerController;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic
{
    public class GameManager: MonoBehaviour, IPlayerObserver
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject[] _enemiesForThisLevel;
        [SerializeField] private GameSpawnerData _gameSpawnerData;
        [SerializeField] private LevelUpStorer _levelUpStorer;
        [SerializeField] private LevelUpMenu _levelUpMenu;

        public static bool IsPaused;
        
        private EnemySpawner _enemySpawner;
        private GameTimeState _gameState;
        private LevelUpManager _levelUpManager;
        
        private void Awake()
        {
            _levelUpManager = new LevelUpManager(_levelUpStorer, _levelUpMenu); 
            _levelUpManager.OnChoiceDetermined += PlayerTraitLevelUp;
        }

        private void Start()
        {
            IsPaused = false;
            Player.Instance.AddObserver(this);
            Player.Instance.AddObserver(_levelUpManager);
            _enemySpawner = new EnemySpawner(_gameSpawnerData, _enemiesForThisLevel, Camera.main);
            _gameState = GameTimeState.EarlyGame;
        }

        private void Update()
        {
            if (!IsPaused)
            {
                SpawnEnemies();
            }
        }

        public void OnPlayerNotify(Player player, bool isMiniBossKill)
        {
            SetGameToPause();
        }
        
        private void PlayerTraitLevelUp(BasicLevelUp basicLevel)
        {
            Player player = _player.GetComponent<Player>();
            if (basicLevel is PlayerLevelUp playerLevelUp)
            {
                player.ImplementPlayerTraitLevelUp(playerLevelUp);   
            }
            if (basicLevel is AbilityToAdd abilityToAdd)
            {
                player.ImplementAbilityAdd(abilityToAdd);
            }
            if (basicLevel is AbilityEvolve abilityEvolve)
            {
                player.ImplementAbilityEvolve(abilityEvolve);
            }
            if (basicLevel is AbilityLevelUp abilityLevelUp)
            {
                player.ImplementAbilityLevelUp(abilityLevelUp);
            }
            UnPauseGame();
        }

        private void SetGameToPause()
        {
            IsPaused = true;
            Time.timeScale = 0f;
        }

        private void UnPauseGame()
        {
            IsPaused = false;
            Time.timeScale = 1f;
        }
        private void SetGameState()
        {
            if (Time.time<=_gameSpawnerData.MiddleGameTimeEnd && _gameSpawnerData.EarlyGameTimeEnd<Time.time)
            {
                _gameState = GameTimeState.MiddleGame;
            }

            else if (Time.time>_gameSpawnerData.MiddleGameTimeEnd)
            {
                _gameState = GameTimeState.LateGame;
            }
        }
        private void SpawnEnemies()
        {
            if (_enemySpawner.SpawnerIsReady(_gameState, Time.time))
            {
                SetGameState();
                _enemySpawner.SpawnEnemy(_player, Time.time);
            }
        }
    }
}