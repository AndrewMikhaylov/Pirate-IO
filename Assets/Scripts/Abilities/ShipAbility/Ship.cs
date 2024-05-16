using System;
using EnemyLogic;
using PlayerController;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.ShipAbility
{
    public class Ship : MonoBehaviour
    {
        private Vector2 _destination;
        private float _speed;
        private float _damage;
        private float _knockBackForce;
        private float _radius;
        public virtual void SetUpProjectile(Transform playerTransform, float radius, float speed, float damage, float knockBackForce)
        {
            _speed = speed;
            _damage = damage;
            _radius = radius;
            var xDestination = playerTransform.position.x - (transform.position.x - playerTransform.position.x);
            var yDestination = playerTransform.position.y - (transform.position.y - playerTransform.position.y);
            _destination = new Vector2(xDestination, yDestination);
            _knockBackForce = knockBackForce;
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _destination)<0.01f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
             if (other.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
             {
                 Vector2 direction = (other.transform.position - transform.position).normalized;
                 Vector2 directionAndForce = direction * _knockBackForce;
                 enemy.TakeDamage(_damage, directionAndForce);
             }
        }
    }
}