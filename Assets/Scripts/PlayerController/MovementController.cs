using UnityEngine;

namespace PlayerController
{
    public class MovementController
    {
        private float _playerSpeed;
        public MovementController(float speed)
        {
            _playerSpeed = speed;
        }

        public void UpdateSpeed(float speedAmount)
        {
            _playerSpeed += speedAmount;
        }
        public void MovePlayer(Transform transform)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position  = Vector2.MoveTowards(transform.position, mousePos, _playerSpeed * Time.deltaTime);
        }
    }
}
