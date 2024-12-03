using System;
using _Main.Scripts.InGameUI;
using _Main.Scripts.SRCarController;
using UnityEngine;

namespace _Main.Scripts.Dashboard
{
    public class DashboardUIUpdater : MonoBehaviour
    {
        [SerializeField] private CarController carController;

        private void Update()
        {
            UpdateDashBoard();
        }

        private void UpdateDashBoard()
        {
            InGameUIManager.Instance.SpeedTxt.text = Mathf.RoundToInt(carController.Speed) + " km/h";
            InGameUIManager.Instance.GearTxt.text = GetGearString();
        }

        private string GetGearString()
        {
            return carController.Speed == 0 ? "N" : (carController.CurrentGear + 1).ToString();
        }
    }
}