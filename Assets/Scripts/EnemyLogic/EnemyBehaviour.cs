using System;
using PlayerController;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public static Action OnMiniBossDeath;
        
        [SerializeField] private EnemyData enemyData;
        private Player _player;
        private EnemyHealthManager _enemyHealthManager;
        private Vector3 _pushBackDirection;
        private float _lastTimeDealtDamage;
        private bool _isPushed;

        private float _speed;
        private float _health;
        private float _damage;
        private float _pushBackSpeed;

        private bool _isMiniBoss;

        private void Start()
        {
            _lastTimeDealtDamage = 0;
            _enemyHealthManager = new EnemyHealthManager(_health);
            _enemyHealthManager.hitPointsAtZero += OnDeathAction;
            _isPushed = false;
        }

        private void Update()
        {
            if (!_isPushed)
            {
                MoveEnemy(_player.transform.position);   
            }
            else
            {
                MoveEnemy(_pushBackDirection);
            }
        }

        public void SetEnemy(Player player, bool isMiniBoss)
        {
            _player = player;
            if (isMiniBoss)
            {
                _speed = enemyData.Speed + enemyData.Speed*enemyData.MiniBossStatMultiplier;
                _health = enemyData.Health + enemyData.Health*enemyData.MiniBossStatMultiplier;
                _damage = enemyData.Damage + enemyData.Damage*enemyData.MiniBossStatMultiplier;
                _pushBackSpeed = enemyData.PushBackSpeed - enemyData.PushBackSpeed * enemyData.MiniBossStatMultiplier;
                _isMiniBoss = true;

            }
            else
            {
                _speed = enemyData.Speed;
                _health = enemyData.Health;
                _damage = enemyData.Damage;
                _pushBackSpeed = enemyData.PushBackSpeed;
                _isMiniBoss = false;
            }
        }

        public float GetSpawnTimePeriodStart()
        {
            return enemyData.SpawnTimePeriodStart;
        }
        public float GetSpawnTimePeriodEnd()
        {
            return enemyData.SpawnTimePeriodEnd;
        }
        public void TakeDamage(float damage, Vector2 pushbackDirection)
        {
            _enemyHealthManager.TakeDamage(damage);
             _isPushed = true;
            _pushBackDirection = pushbackDirection;
        }

        private void OnDeathAction()
        {
            if (_isMiniBoss)
            {
                Instantiate(enemyData.MiniBossExperiencePoint, transform.position, transform.rotation);
                OnMiniBossDeath.Invoke();
            }
            else
            {
                Instantiate(enemyData.ExperiencePoint, transform.position, transform.rotation);   
            }
            Destroy(gameObject);
        }
        private void MoveEnemy(Vector3 targetPosition)
        {
            if (_player!=null && !_isPushed)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position,
                    _speed * Time.deltaTime);
            }
            if (_isPushed)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, _pushBackSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
                {
                    _isPushed = false;
                }    
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (enemyData.DamageFrequency <= Time.time - _lastTimeDealtDamage)
            {
                if (other.gameObject.GetComponent<Player>())
                {
                    _player.TakeDamage(_damage);
                    _lastTimeDealtDamage = Time.time;
                }   
            }
        }
    }
}
