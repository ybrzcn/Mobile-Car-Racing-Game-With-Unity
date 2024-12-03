using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.SRGameSettings;
using _Main.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private List<SelectCarModel> carsList;
    [SerializeField] private TextMeshProUGUI motorPowerTxt;
    [SerializeField] private TextMeshProUGUI breakPowerTxt;
    [FormerlySerializedAs("maxSpeedPowerTxt")] [SerializeField] private TextMeshProUGUI maxSpeedTxt;
    [SerializeField] private TextMeshProUGUI carNameTxt;
    private int currentCar;
    
    private void Start()
    {
        currentCar = 0;
        SelectCar(currentCar);
        GameSettingsManager.Instance.GameSettings.PlayerCar = carsList[currentCar].CarProps;
    }

    private void SelectCar(int _index)
    {
        for (int i = 0; i < carsList.Count; i++)
        {
            if (i == _index)
            {
                motorPowerTxt.text = "MOTOR POWER : " + carsList[i].CarProps.MotorPower;
                breakPowerTxt.text = "BRAKE POWER : " + carsList[i].CarProps.BrakePower;
                maxSpeedTxt.text = "MAX SPEED : " + carsList[i].CarProps.MaxSpeed;
                carNameTxt.text = carsList[i].CarProps.CarName;
            }
            carsList[i].gameObject.SetActive(i == _index);
        }
        
        GameSettingsManager.Instance.GameSettings.PlayerCar = carsList[currentCar].CarProps;
    }

    public void ChangeCar(int _change)
    {
        currentCar += _change;
        if (currentCar < 0)
        {
            currentCar = carsList.Count - 1;
        }

        if (currentCar > carsList.Count - 1)
        {
            currentCar = 0;
        }
        
        SelectCar(currentCar);
    }

    public void StartRace()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
