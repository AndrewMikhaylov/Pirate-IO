using Abilities.CannonAbility;
using EnemyLogic;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.EvolvedAbilities.EvolvedCannon
{
    public class EvolvedCannonBall: Cannonball
    {
        [SerializeField] private float _bounceCount;
        private float _bounceCounter;
        
        public override void SetUpCannonball(float damage, float speed, float radius, float knockBackForce)
        {
            _radius = radius;
            _damage = damage;
            _speed = speed;
            _knockBackForce = knockBackForce;
            SetPosition();
            _bounceCounter = _bounceCount;
        }

        void Update()
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
                _bounceCounter -= 1;
                SetPosition();
            }
        }

        private void SetPosition()
        {
            _startPosition = transform.position;
            _controlPosition = _startPosition + (_controlPosition - _startPosition) / 2 + Vector2.up * 5.0f;
            _countDistanceToTarget = 0.0f;
            if (FindAllEnemies(_startPosition).Count==0)
            {
                _enemyPosition = Random.insideUnitCircle.normalized * _radius;

            }
            else
            {
                var chosenEnemyTarget = ChooseEnemy(FindAllEnemies(_startPosition));
                _enemyPosition = chosenEnemyTarget.transform.position;    
            }
        }
        protected override void Detonate()
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
            if (_bounceCounter == 0)
            {
                Destroy(gameObject);    
            }
        }
    }
}