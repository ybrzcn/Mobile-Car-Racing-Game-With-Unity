using System;
using System.Linq;
using _Main.Scripts.Cars;
using _Main.Scripts.Skill;
using _Main.Scripts.SRCarController;
using _Main.Scripts.Track;
using UnityEngine;

namespace _Main.Scripts.Lap
{
    [Serializable]
    public class Standing
    {
        [SerializeField] private CarSkillManager carSkillManager;
        [SerializeField] private CarController myCarController;
        [SerializeField] private CarLapManager myCarLapManager;

        private CarProps carProps;
        private bool isPlayerControlled;
        public CarProps CarProps => carProps;

        public bool IsPlayerControlled => isPlayerControlled;

        public void InitalizeValues()
        {
            carProps = myCarController.CarProps;
            isPlayerControlled = carSkillManager.CasterType == CasterType.Human;
        }

        public float MyStandingScore()
        {
            return myCarController.CurrentWaypointIndex + myCarLapManager.CurrentLap *
                TrackWaypointsManager.Instance.Waypoints.Count * 50000000f + myCarLapManager.PassedCheckpointsDictionary.Count(x => x.Value) * 70f;
        }
    }
}