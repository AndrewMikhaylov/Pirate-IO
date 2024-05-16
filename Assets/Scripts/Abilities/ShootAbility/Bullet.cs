using System;
using EnemyLogic;
using UnityEngine;

namespace Abilities.ShootAbility
{
    public class Bullet:MonoBehaviour
    {
        private float _bulletSpeed;
        private Vector3 _direction;
        private float _damage;
        private float _knockBackForce;
        
        private void Update()
        {
            transform.Translate(_direction * (_bulletSpeed * Time.deltaTime));
        }

        public void SetBullet(float givenSpeed, Vector3 direction, float damage, float knockBackForce)
        {
            _bulletSpeed = givenSpeed;
            _direction = direction;
            _damage = damage;
            _knockBackForce = knockBackForce;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                Vector2 directionAndForce = direction * _knockBackForce;
                enemy.TakeDamage(_damage, directionAndForce);
                Destroy(gameObject);
            }
        }
    }
}