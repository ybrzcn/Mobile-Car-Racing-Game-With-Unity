using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.End;
using _Main.Scripts.SRCamera;
using _Main.Scripts.SRCarController;
using _Main.Scripts.SRGameSettings;
using _Main.Scripts.Track;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main.Scripts.Lap
{
    public class CarLapManager : MonoBehaviour
    {
        [SerializeField] private PlayerCameraManager playerCameraManager;
        [SerializeField] private Standing standing;
        private int currentLap;
        private int standingScore;

        private Dictionary<int, bool> passedCheckpointsDictionary;

        public Dictionary<int, bool> PassedCheckpointsDictionary => passedCheckpointsDictionary;

        public int CurrentLap => currentLap;

        public Standing Standing => standing;

        private void Start()
        {
            currentLap = 0;
            ResetPassedCheckpointsDictionary();
            standing.InitalizeValues();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoint"))
            {
                PassCheckpoint(GameManager.Instance.GlobalLapManager.Checkpoints.IndexOf(other.gameObject));
            }

            if (other.CompareTag("Finish") && CheckLapFinished())
            {
                currentLap++;
                ResetPassedCheckpointsDictionary();
                if (currentLap >= GameSettingsManager.Instance.GameSettings.TotalLapCount && standing.IsPlayerControlled)
                {
                    FinishGame();
                }
            }
        }

        private void FinishGame()
        {
            EndUIManager.Instance.EnableEndUI();
            playerCameraManager.ChangeCamToEndCam();
        }

        private void ResetPassedCheckpointsDictionary()
        {
            passedCheckpointsDictionary = new Dictionary<int, bool>();
            for (int i = 0; i < GameManager.Instance.GlobalLapManager.Checkpoints.Count; i++)
            {
                passedCheckpointsDictionary.Add(i, false);
            }
        }


        private bool CheckLapFinished()
        {
            return !(passedCheckpointsDictionary.Count(x => x.Value == false) > 0);
        }

        private void PassCheckpoint(int checkpointCount)
        {
            passedCheckpointsDictionary[checkpointCount] = true;
        }

        
    }
}