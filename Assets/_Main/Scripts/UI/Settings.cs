using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class _Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume", 0f));
    }

    public void SetVolume (float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        audioMixer.SetFloat("volume", volume);
    }
}
