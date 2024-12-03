using System;
using UnityEngine;

namespace _Main.Scripts.SRCarController.Gear
{
    [Serializable]
    public class GearProperties
    {
        [SerializeField] private float gearSpeedLimit;
        [SerializeField] private float gearTorqueRatio;

        public float GearSpeedLimit => gearSpeedLimit;

        public float GearTorqueRatio => gearTorqueRatio;
    }
}