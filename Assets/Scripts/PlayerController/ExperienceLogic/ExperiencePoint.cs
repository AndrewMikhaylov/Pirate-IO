using System;
using UnityEngine;

namespace PlayerController.ExperienceLogic
{
    public class ExperiencePoint: MonoBehaviour
    {
        [SerializeField] private float _experienceAmount;
        [SerializeField] private float _moveSpeed;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.GetComponent<Player>())
            {
                collider.gameObject.GetComponent<Player>().TakeExperience(_experienceAmount);
                Destroy(gameObject);
            }
        }

        public void MoveToTarget(Vector2 targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
        }
    }
}