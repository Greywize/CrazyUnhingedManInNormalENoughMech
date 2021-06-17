using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Fields

    public static SoundManager Instance = null;

    #endregion

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayFX(AudioSource source, AudioClip clip, bool randomizePitch = false, float lowRange = 0.95f, float highRange = 1.05f)
    {
        if (randomizePitch)
        {
            float random = Random.Range(lowRange, highRange);
            source.pitch = random;
        }

        source.clip = clip;
        source.Play();
    }

    public void PlayMusic(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}