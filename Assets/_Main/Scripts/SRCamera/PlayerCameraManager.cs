using System;
using _Main.Scripts.InGameUI;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Main.Scripts.SRCamera
{
    public class PlayerCameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera raceCam;
        [SerializeField] private CinemachineVirtualCamera endCam;
        [SerializeField] private CinemachineVirtualCamera backCam;

        private void Start()
        {
            ChangeCamToRaceCam();
            
            AddTriggerEvent(InGameUIManager.Instance.BackBtn, EventTriggerType.PointerDown ,ChangeCamToBackCam);
            AddTriggerEvent(InGameUIManager.Instance.BackBtn, EventTriggerType.PointerUp ,ChangeCamToRaceCam);
        }

        private void ChangeCamToRaceCam()
        {
            raceCam.Priority = 10;
            endCam.Priority = 0;
            backCam.Priority = 0;
        }
        
        private void ChangeCamToBackCam()
        {
            backCam.Priority = 10;
            raceCam.Priority = 0;
            endCam.Priority = 0;
        }
        
        public void ChangeCamToEndCam()
        {
            backCam.Priority = 0;
            raceCam.Priority = 0;
            endCam.Priority = 10;
        }
        
        private void AddTriggerEvent(EventTrigger eventTrigger, EventTriggerType triggerType, Action action)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = triggerType;
            entry.callback.AddListener((_) => action?.Invoke());
            eventTrigger.triggers.Add(entry);
        }
    }
}