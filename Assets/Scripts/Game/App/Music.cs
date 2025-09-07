using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class MusicStruct
{
    public static string[] SonglistNames = new string[]
    {
        // Song names in music_Songs should match that of MusicStruct.SonglistNames
        "Random",
        "Theme Song"
    };
}

public class Music : MonoBehaviour
{
    [SerializeField] AudioLowPassFilter audio_lpf;
    [SerializeField] AudioSource music_AudioSource;
    [SerializeField] AudioClip[] music_Songs;

    int mIndex;
    private float lastCutoff = -1f;

    void Start()
    {
        // Music Index Array
        mIndex = PlayerPrefs.GetInt("Music_SongIndex");
        mIndex = Mathf.Clamp(mIndex, 0, music_Songs.Length);
    }

    void Update()
    {
        float targetCutoff = CONSTANTS.GAME_PAUSED ? 950f : 24000f;

        if (!Mathf.Approximately(lastCutoff, targetCutoff))
        {
            audio_lpf.cutoffFrequency = targetCutoff;
            lastCutoff = targetCutoff;
        }
    }


    public void StartNewMusic()
    {
        if (mIndex <= 0)
        {
            music_AudioSource.clip = music_Songs[Random.Range(0, music_Songs.Length)];
        }
        else
        {
            music_AudioSource.clip = music_Songs[mIndex - 1];
        }

        music_AudioSource.Play();
        StartCoroutine(FastMusic());
    }

    public IEnumerator SlowMusic()
    {
        while (music_AudioSource.pitch > 0f)
        {
            music_AudioSource.pitch = Mathf.Max(0f, music_AudioSource.pitch - 0.5f * Time.deltaTime);
            yield return null;
        }

        music_AudioSource.Stop();
    }

    public IEnumerator FastMusic()
    {
        while (music_AudioSource.pitch < 1f)
        {
            music_AudioSource.pitch = Mathf.MoveTowards(music_AudioSource.pitch, 1f, 0.5f * Time.deltaTime);
            yield return null;
        }
    }
}