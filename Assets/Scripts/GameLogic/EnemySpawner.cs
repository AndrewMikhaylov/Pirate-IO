using EnemyLogic;
using PlayerController;
using UnityEngine;

namespace GameLogic
{
    public class EnemySpawner
    {
        private GameObject[] _enemies;
        private Camera _camera;
        private float _lastTimeEnemySpawned;
        private float _lastTimeMiniBossSpawned;
        private GameSpawnerData _gameSpawnerData;
        public EnemySpawner(GameSpawnerData gameSpawnerData, GameObject[] enemies, Camera camera)
        {
            _enemies = enemies;
            _camera = camera;
            _lastTimeEnemySpawned = 0;
            _lastTimeMiniBossSpawned = 0;
            _gameSpawnerData = gameSpawnerData;
        }
        
        public void SpawnEnemy(GameObject player, float timePeriod)
        {
            _lastTimeEnemySpawned = timePeriod;
            _lastTimeMiniBossSpawned = timePeriod - _lastTimeMiniBossSpawned;
            foreach (var enemy in _enemies)
            {
                var currentEnemy = enemy.GetComponent<EnemyBehaviour>();
                if (currentEnemy.GetSpawnTimePeriodStart() <= timePeriod &&
                    timePeriod <= currentEnemy.GetSpawnTimePeriodEnd())
                {
                    var spawnPosition = SetSpawnPosition(player.transform.position);
                    var enemySpawned = GameObject.Instantiate(enemy, spawnPosition, player.transform.rotation);
                    if (_lastTimeEnemySpawned>=_gameSpawnerData.MiniBossSpawnPeriod)
                    {
                        enemySpawned.GetComponent<EnemyBehaviour>().SetEnemy(player.GetComponent<Player>(), true);
                        _lastTimeMiniBossSpawned = 0;
                    }
                    else
                    {
                        enemySpawned.GetComponent<EnemyBehaviour>().SetEnemy(player.GetComponent<Player>(), false);
                    }
                }
            }
        }
        public bool SpawnerIsReady(GameTimeState currentGameState, float currentTime)
        {
            bool isReady = true;
            switch (currentGameState)
            {
                case GameTimeState.EarlyGame:
                    if (currentTime - _lastTimeEnemySpawned<_gameSpawnerData.EarlyGameSpawnPeriod)
                    {
                        isReady = false;
                    }
                    break;
                case GameTimeState.MiddleGame:
                    if (currentTime - _lastTimeEnemySpawned<_gameSpawnerData.MiddleGameSpawnPeriod)
                    {
                        isReady = false;
                    }
                    break;
                case GameTimeState.LateGame:
                    if (currentTime - _lastTimeEnemySpawned<_gameSpawnerData.LateGameSpawnPeriod)
                    {
                        isReady = false;
                    }
                    break;
            }
            return isReady;
        }

        private Vector2 SetSpawnPosition(Vector2 position)
        {
            Vector3 topRightCorner = _camera.ViewportToWorldPoint(new Vector3(1,1, _camera.nearClipPlane));
            float spawnDistance = Vector3.Distance(position, topRightCorner);
            var randomSpawnPoint = Random.insideUnitCircle.normalized * spawnDistance + position;
            return randomSpawnPoint;
        }
    }
}