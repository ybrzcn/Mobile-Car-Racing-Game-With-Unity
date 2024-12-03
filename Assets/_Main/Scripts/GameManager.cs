using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.Cars;
using _Main.Scripts.InGameUI;
using _Main.Scripts.Lap;
using _Main.Scripts.SRCarController;
using _Main.Scripts.SRGameSettings;
using _Main.Scripts.StartLight;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private GlobalLapManager globalLapManager;
        [SerializeField] private StartLightManager startLightManager;
        [SerializeField] private List<Transform> spawnPositions;
        [SerializeField] private List<CarProps> cars;
        [SerializeField] private float startCountdownTime;

        private List<CarController> activeCars;

        private bool isGameStarted = false;
        private float startTime;
        
        public GlobalLapManager GlobalLapManager => globalLapManager;

        private void Start()
        {
            activeCars = new List<CarController>();
            SpawnCars();
            StartCoroutine(CheckGameStarted());
        }

        private void SpawnCars()
        {
            var playerSpawnIndex = Random.Range(0, spawnPositions.Count);

            for (int i = 0; i < spawnPositions.Count; i++)
            {
                if (i == playerSpawnIndex)
                {
                    activeCars.Add(Instantiate(GameSettingsManager.Instance.GameSettings.PlayerCar.PlayerPrefab, spawnPositions[i].position,
                        spawnPositions[i].rotation).GetComponent<CarController>());
                }

                else
                {
                    var randomIndex = Random.Range(0, cars.Count);
                    activeCars.Add(Instantiate(cars[randomIndex].AiPrefab, spawnPositions[i].position,
                        spawnPositions[i].rotation).GetComponent<CarController>());
                }
            }
        }

        private IEnumerator CheckGameStarted()
        {
            startTime = Time.time;
            isGameStarted = false;
            InGameUIManager.Instance.CountdownTxt.gameObject.SetActive(true);

            foreach (var activeCar in activeCars)
            {
                activeCar.enabled = false;
            }

            while (!isGameStarted)
            {
                isGameStarted = Time.time >= startTime + startCountdownTime;
                startLightManager.ChangeColors(Time.time / (startTime + startCountdownTime));
                InGameUIManager.Instance.CountdownTxt.text = (startTime + startCountdownTime - Time.time).ToString("F0");
                yield return null;
            }
            
            foreach (var activeCar in activeCars)
            {
                activeCar.enabled = true;
            }
            InGameUIManager.Instance.CountdownTxt.gameObject.SetActive(false);
            startLightManager.MakeLightsGreen();

            isGameStarted = true;
        }
    }
}