using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(10f)); //cuz dB unit for audio (value 0 - 100)
    }
}
