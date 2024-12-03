using System;
using _Main.Scripts.SRCarController;
using UnityEngine;

namespace _Main.Scripts.InGameUI
{
    public class RespawnSliderManager : MonoBehaviour
    {
        [SerializeField] private CarController carController;
        [SerializeField] private float sliderAppearDelay;
        private void Update()
        {
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            if (carController.RecoverTimeChecker >= sliderAppearDelay)
            {
                if (!InGameUIManager.Instance.RespawnSlider.gameObject.activeInHierarchy)
                {
                    InGameUIManager.Instance.RespawnSlider.gameObject.SetActive(true);
                }

                if (carController.RecoverTimeChecker >= carController.RecoverTime && InGameUIManager.Instance.RespawnSlider.gameObject.activeInHierarchy)
                {
                    InGameUIManager.Instance.RespawnSlider.gameObject.SetActive(false);
                }
                InGameUIManager.Instance.RespawnSlider.value =
                    (carController.RecoverTimeChecker - sliderAppearDelay) / (carController.RecoverTime - sliderAppearDelay);
            }

            else
            {
                if (InGameUIManager.Instance.RespawnSlider.gameObject.activeInHierarchy)
                {
                    InGameUIManager.Instance.RespawnSlider.gameObject.SetActive(false);
                }
            }
        }
    }
}