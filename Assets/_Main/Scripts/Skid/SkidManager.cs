using System;
using _Main.Scripts.SRCarController;
using _Main.Scripts.SRInput;
using UnityEngine;

namespace _Main.Scripts.Skid
{
    public class SkidManager : MonoBehaviour
    {
        [SerializeField] private CarController carController;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private WheelCollider myWheel;
        [SerializeField] private TrailRenderer skidTrail;

        private void Update()
        {
            HandleSkidMarks();
        }

        private void HandleSkidMarks()
        {
            if (myWheel.GetGroundHit(out WheelHit wheelHit) && inputManager.IsBraking)
            {
                if (!skidTrail.emitting)
                {
                    skidTrail.emitting = true;
                }

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    audioSource.pitch = Mathf.Lerp(minPitch, maxPitch,
                        carController.Speed / carController.CarProps.MaxSpeed);
                }
            }

            else
            {
                if (skidTrail.emitting)
                {
                    skidTrail.emitting = false;
                }
                
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}