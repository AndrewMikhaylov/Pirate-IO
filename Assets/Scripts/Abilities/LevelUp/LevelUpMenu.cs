using System;
using TMPro;
using UnityEngine;

namespace Abilities.LevelUp
{
    public class LevelUpMenu: MonoBehaviour
    {
        public Action<int> OnChoiceMade;

        [SerializeField] private TMP_Text _leftButtonText;
        [SerializeField] private TMP_Text _middleButtonText;
        [SerializeField] private TMP_Text _rightButtonText;

        private BasicLevelUp _leftLevelUp;
        private BasicLevelUp _middleLevelUp;
        private BasicLevelUp _rightLevelUp;
        public void InitializeOptions(BasicLevelUp[] randomPlayerLevelUps, BasicLevelUp randomAbilityLevelUp)
        {
            gameObject.SetActive(true);   
            _leftLevelUp = randomPlayerLevelUps[0];
            _middleLevelUp = randomAbilityLevelUp;
            _rightLevelUp = randomPlayerLevelUps[1];
            SetTextForOptions();
        }

        private void SetTextForOptions()
        {
            _leftButtonText.text = _leftLevelUp.Description;
            _middleButtonText.text = _middleLevelUp.Description;
            _rightButtonText.text = _rightLevelUp.Description;
        }

        public void LeftChoice()
        {
            OnChoiceMade?.Invoke(0);
            gameObject.SetActive(false);
        }

        public void MiddleChoice()
        {
            OnChoiceMade?.Invoke(1);
            gameObject.SetActive(false);
        }

        public void RightChoice()
        {
            OnChoiceMade?.Invoke(2);
            gameObject.SetActive(false);
        }
    }
}