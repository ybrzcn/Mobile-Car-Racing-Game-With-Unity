using System;
using System.Linq;
using _Main.Scripts.InGameUI;
using _Main.Scripts.SRCarController;
using _Main.Scripts.SRGameSettings;
using _Main.Scripts.SRStanding;
using UnityEngine;

namespace _Main.Scripts.Lap
{
    public class LapAndStandingUIUpdater : MonoBehaviour
    {
        [SerializeField] private CarLapManager carLapManager;

        private void Update()
        {
            UpdateLapsTxt();
            UpdateStandingTxt();
        }

        private void UpdateLapsTxt()
        {
            InGameUIManager.Instance.LapsTxt.text = (carLapManager.CurrentLap + 1) + " / " + GameSettingsManager.Instance.GameSettings.TotalLapCount  +" Laps";
        }

        private void UpdateStandingTxt()
        {
            if(StandingManager.Instance.StandingList.Count == 0) return;
            var standing = StandingManager.Instance.StandingList.IndexOf(carLapManager.Standing) + 1;
            var standingString = "";

            switch (standing)
            {
                case 1:
                    standingString = "1st";
                    break;
                case 2:
                    standingString = "2nd";
                    break;
                case 3:
                    standingString = "3rd";
                    break;
                default:
                    standingString = standing + "th";
                    break;
            }

            InGameUIManager.Instance.StandingTxt.text = standingString;

        }
    }
}