using System;
using System.Collections.Generic;
using EnemyLogic;
using PlayerController.ExperienceLogic;
using UnityEngine;

namespace PlayerController
{
    public class ExperienceManager
    {
        public Action OnLevelUp;

        private float _pickUpRadius;
        private float _currentExperience;
        private float _currentLevelMaxExperience;
        private float _newLevelCapIncrease;

        public ExperienceManager(float pickUpRadius, float firstLevelExpCap, float newLevelCapIncrease)
        {
            _pickUpRadius = pickUpRadius;
            _currentLevelMaxExperience = firstLevelExpCap;
            _currentExperience = 0;
            _newLevelCapIncrease = newLevelCapIncrease;
        }
        
        public float GetCurrentExp()
        {
            return _currentExperience;
        }

        public void UpdatePickUpRadius(float updateAmount)
        {
            _pickUpRadius += updateAmount;
        }

        public void ScanRadiusForExperience(Vector2 position)
        {
            List<Collider2D> expColliders = new List<Collider2D>();
            Collider2D[] results = Physics2D.OverlapCircleAll(position, _pickUpRadius);
            if (results.Length == 0)
            {
                return;
            }
            foreach (var collider in results)
            {
                if (collider.TryGetComponent<ExperiencePoint>(out ExperiencePoint expPoint))
                {
                    expColliders.Add(collider);
                }
            }

            foreach (var expCollider in expColliders)
            {
                expCollider.GetComponent<ExperiencePoint>().MoveToTarget(position);
            }
            
        }

        public void AddExperience(float expAmount)
        {
            _currentExperience += expAmount;
            if (_currentExperience>=_currentLevelMaxExperience)
            {
                _currentExperience -= _currentLevelMaxExperience;
                _currentLevelMaxExperience += _newLevelCapIncrease;
                OnLevelUp.Invoke();
            }
        }
    }
}