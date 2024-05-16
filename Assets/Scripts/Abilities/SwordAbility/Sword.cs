using System;
using EnemyLogic;
using UnityEngine;

namespace Abilities.SwordAbility
{
    public class Sword: MonoBehaviour
    {
        private float _damage;
        private float _speed;
        private float _knockBackForce;
        private Transform _parent;
        private Quaternion _lastRotationValue;
        private float _totalRotation;

        public void SetUpSword(float damage, float speed, float knockBackForce)
        {
            _damage = damage;
            _speed = speed;
            _knockBackForce = knockBackForce;
            _lastRotationValue = transform.rotation;
            _totalRotation = 0f;
        }
        private void Update()
        {
            RotateSword();
            CheckForFullRotation();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                Vector2 directionAndForce = direction * _knockBackForce;
                enemy.TakeDamage(_damage, directionAndForce);
            }
        }

        private void RotateSword()
        {
            _parent = transform.parent;
            transform.RotateAround(_parent.position, new Vector3(0,0,1), _speed * Time.deltaTime);
            float angle = Quaternion.Angle(transform.rotation, _lastRotationValue);
            
            _totalRotation += angle;
            _lastRotationValue = transform.rotation;
        }

        private void CheckForFullRotation()
        {
            if (_totalRotation >= 360f)
            {
                _totalRotation = 0f;
                Destroy(gameObject);
            }
        }
    }
}