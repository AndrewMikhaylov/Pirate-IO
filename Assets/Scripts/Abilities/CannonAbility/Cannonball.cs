using System;
using System.Collections;
using System.Collections.Generic;
using EnemyLogic;
using UnityEngine;
using Random = System.Random;

namespace Abilities.CannonAbility
{
    public class Cannonball : MonoBehaviour
    {
        [SerializeField] protected float explosionRadius;

        protected float _damage;
        protected float _speed;
        protected float _knockBackForce;
        protected Vector2 _enemyPosition;
        protected Vector2 _startPosition;
        protected Vector2 _controlPosition;
        protected float _countDistanceToTarget;
        protected float _radius;

        public virtual void SetUpCannonball(float damage, float speed, float radius, float knockBackForce)
        {
            _radius = radius;
            _damage = damage;
            _speed = speed;
            _knockBackForce = knockBackForce;
            _startPosition = transform.position;
            _controlPosition = _startPosition + (_controlPosition - _startPosition) / 2 + Vector2.up * 5.0f;
            _countDistanceToTarget = 0.0f;
            if (FindAllEnemies(_startPosition).Count==0)
            {
                _enemyPosition = UnityEngine.Random.insideUnitCircle.normalized * _radius;

            }
            else
            {
                var chosenEnemyTarget = ChooseEnemy(FindAllEnemies(_startPosition));
                _enemyPosition = chosenEnemyTarget.transform.position;    
            }
            
        }

        private void Update()
        {
            if (_countDistanceToTarget < 1.0f)
            {
                
                _countDistanceToTarget += _speed * Time.deltaTime;
                Vector2 m1 = Vector2.Lerp(_startPosition, _controlPosition, _countDistanceToTarget);
                Vector2 m2 = Vector2.Lerp(_controlPosition, _enemyPosition, _countDistanceToTarget);
                transform.position = Vector2.Lerp(m1, m2, _countDistanceToTarget);
            }
            else
            {
                Detonate();
            }
        }

        protected virtual void Detonate()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            if (hitColliders.Length!=0)
            {
                foreach (var collider in hitColliders)
                {
                    if (collider.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
                    {
                        Vector2 direction = (collider.transform.position - transform.position).normalized;
                        Vector2 directionAndForce = direction * _knockBackForce;
                        enemy.TakeDamage(_damage, directionAndForce);
                    }
                }   
            }
            Destroy(gameObject);
        }

        protected Collider2D ChooseEnemy(List<Collider2D> enemies)
        {
            Random random = new Random();
            int enemyIndex = random.Next(enemies.Count);
            return enemies[enemyIndex];
        }
        
        protected List<Collider2D> FindAllEnemies(Vector2 position)
        {
            List<Collider2D> enemyColliders = new List<Collider2D>();
            Collider2D[] results = Physics2D.OverlapCircleAll(position, _radius);
            if (results.Length == 0)
            {
                return enemyColliders;
            }
            foreach (var collider in results)
            {
                if (collider.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
                {
                    enemyColliders.Add(collider);
                }
            }

            return enemyColliders;
        }
    }
}
