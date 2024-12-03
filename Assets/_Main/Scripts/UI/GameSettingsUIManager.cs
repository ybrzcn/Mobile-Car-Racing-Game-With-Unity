using System;
using System.Collections.Generic;
using _Main.Scripts.SRGameSettings;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI
{
    public class GameSettingsUIManager : MonoBehaviour
    {
        [SerializeField] private List<AIDifficultyButton> difficultyButtons;
        [SerializeField] private List<TotalLapCountButton> totalLapCountButtons;

        private AIDifficultyButton currentDifficultyButton;
        private TotalLapCountButton currentTotalLapCountButton;

        private void Start()
        {
            ChangeDifficulty(difficultyButtons[0]);
            ChangeTotalLap(totalLapCountButtons[0]);
        }

        public void ChangeDifficulty(AIDifficultyButton newDifficulty)
        {
            if (newDifficulty == currentDifficultyButton)
            {
                return;
            }

            foreach (var difficultyButton in difficultyButtons)
            {
                difficultyButton.BoxGameObject.SetActive(false);
            }

            currentDifficultyButton = newDifficulty;
            currentDifficultyButton.BoxGameObject.SetActive(true);
            GameSettingsManager.Instance.GameSettings.AiProperties = currentDifficultyButton.MyAIProperty;
        }
        
        public void ChangeTotalLap(TotalLapCountButton newDifficulty)
        {
            if (newDifficulty == currentTotalLapCountButton)
            {
                return;
            }

            foreach (var difficultyButton in totalLapCountButtons)
            {
                difficultyButton.BoxGameObject.SetActive(false);
            }

            currentTotalLapCountButton = newDifficulty;
            currentTotalLapCountButton.BoxGameObject.SetActive(true);
            
            GameSettingsManager.Instance.GameSettings.TotalLapCount = currentTotalLapCountButton.MyTotalLapCount;
        }
    }
}