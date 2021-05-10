using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundControl : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] Slider slider;

    AudioSource audioSource;

    public static float EndTime { get; private set; }

    public static AudioSource Audio { get; private set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;

        EndTime = audioClip.length;
        Audio = audioSource;

        slider.maxValue = EndTime;
    }

    private void Update()
    {
        if (Audio.time == 0)
        {
            slider.value = 0;
        }

        if (Audio.isPlaying)
        {
            slider.value = Audio.time;
        }
        else
        {
            Audio.time = slider.value;
        }


        if (isStop())
        {
            if (Audio.isPlaying)
            {
               Audio.Pause();
            }
            else
            {
               Audio.Play();
            }
        }
    }

    bool isStop()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
