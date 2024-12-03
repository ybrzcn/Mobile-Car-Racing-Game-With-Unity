using System;
using _Main.Scripts.CarAI;
using _Main.Scripts.Cars;
using UnityEngine;

namespace _Main.Scripts.SRGameSettings
{
    
    [Serializable]
    public class GameSettings
    {
        [SerializeField] private CarProps playerCar;
        [SerializeField] private AIProperties aiProperties;
        [SerializeField] private int totalLapCount;

        public CarProps PlayerCar
        {
            get => playerCar;
            set => playerCar = value;
        }

        public AIProperties AiProperties
        {
            get => aiProperties;
            set => aiProperties = value;
        }

        public int TotalLapCount
        {
            get => totalLapCount;
            set => totalLapCount = value;
        }
    }
}