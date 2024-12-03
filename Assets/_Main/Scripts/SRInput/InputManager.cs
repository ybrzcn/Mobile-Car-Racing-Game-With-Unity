using System;
using _Main.Scripts.InGameUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Main.Scripts.SRInput
{
    public class InputManager : MonoBehaviour
    {
        protected float gasInput = 1f;
        protected float steeringInput;
        private bool isBraking;


        public float GasInput => gasInput;

        public float SteeringInput => steeringInput;

        public bool IsBraking => isBraking;

        private void Start()
        {
            gasInput = 1f;
        }

        protected virtual void OnEnable()
        {
            AddTriggerEvent(InGameUIManager.Instance.LSidewayInputBtn, EventTriggerType.PointerDown ,() =>
            {
                steeringInput = -1f;
            });
            AddTriggerEvent(InGameUIManager.Instance.LSidewayInputBtn, EventTriggerType.PointerUp ,() =>
            {
                steeringInput = 0f;
            });
            
            AddTriggerEvent(InGameUIManager.Instance.RSidewayInputBtn, EventTriggerType.PointerDown ,() =>
            {
                steeringInput = 1f;
            });

            AddTriggerEvent(InGameUIManager.Instance.RSidewayInputBtn, EventTriggerType.PointerUp ,() =>
            {
                steeringInput = 0f;
            });
            
            AddTriggerEvent(InGameUIManager.Instance.BrakeBtn, EventTriggerType.PointerDown ,() =>
            {
                isBraking = true;
            });
            
            AddTriggerEvent(InGameUIManager.Instance.BrakeBtn, EventTriggerType.PointerUp ,() =>
            {
                isBraking = false;
            });
        }

        protected virtual void Update()
        {
            GetInputs();
        }
        
        protected virtual void GetInputs()
        {
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