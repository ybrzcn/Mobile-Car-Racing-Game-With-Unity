using System;
using _Main.Scripts.SRCarController;
using UnityEngine;

namespace _Main.Scripts.SRCarSound
{
    public class CarEngineSoundManager : MonoBehaviour
    {
        [SerializeField] private CarController carController;
        [SerializeField] private AudioSource engineAudioSource;
        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;
        
        private void Update()
        {
            HandleEnginePitch();
        }

        private void HandleEnginePitch()
        {
            engineAudioSource.pitch = Mathf.Lerp(minPitch, maxPitch, carController.Rpm / carController.MaxRpm);
        }
    }
}