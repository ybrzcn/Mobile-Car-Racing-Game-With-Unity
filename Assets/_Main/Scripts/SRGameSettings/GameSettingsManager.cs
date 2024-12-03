using System;
using UnityEngine;
using UnityEngine.Audio;

namespace _Main.Scripts.SRGameSettings
{
    public class GameSettingsManager : MonoSingleton<GameSettingsManager>
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private AudioMixer audioMixer;

        public GameSettings GameSettings => gameSettings;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume", 0f));
        }
    }
}