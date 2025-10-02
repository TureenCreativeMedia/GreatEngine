using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioLowPassFilter audio_lpf;
    [SerializeField] private AudioSource music_AudioSource;
    [SerializeField] private AudioClip[] music_Songs;

    private float lastCutoff = -1f;
    private bool wasPlayingLastFrame = false;

    public AudioClip[] MusicSongs => music_Songs;

    void Start()
    {
        PlayRandomEnabledSong();
    }

    void Update()
    {
        float targetCutoff = CONSTANTS.GAME_PAUSED ? 950f : 24000f;

        if (!Mathf.Approximately(lastCutoff, targetCutoff))
        {
            audio_lpf.cutoffFrequency = targetCutoff;
            lastCutoff = targetCutoff;
        }

        if (wasPlayingLastFrame && !music_AudioSource.isPlaying)
        {
            PlayRandomEnabledSong();
        }

        wasPlayingLastFrame = music_AudioSource.isPlaying;
    }

    public void PlayRandomEnabledSong()
    {
        List<int> enabledSongIndices = new List<int>();

        for (int i = 0; i < music_Songs.Length; i++)
        {
            bool enabled = PlayerPrefs.GetInt($"{i}_enabled", 1) == 1;
            if (enabled)
            {
                enabledSongIndices.Add(i);
            }
        }

        if (enabledSongIndices.Count == 0)
        {
            Debug.LogWarning("No songs are enabled! Playing random song anyway.");
            int randomIndex = Random.Range(0, music_Songs.Length);
            PlaySongByID(randomIndex);
            return;
        }

        int randomEnabledIndex = enabledSongIndices[Random.Range(0, enabledSongIndices.Count)];
        PlaySongByID(randomEnabledIndex);
    }

    public IEnumerator SlowMusic()
    {
        while (music_AudioSource.pitch > 0f)
        {
            music_AudioSource.pitch = Mathf.Max(0f, music_AudioSource.pitch - 0.5f * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator FastMusic()
    {
        while (music_AudioSource.pitch < 1f)
        {
            music_AudioSource.pitch = Mathf.MoveTowards(music_AudioSource.pitch, 1f, 0.5f * Time.deltaTime);
            yield return null;
        }
    }

    public void PlaySongByID(int id)
    {
        if (id >= 0 && id < music_Songs.Length)
        {
            PlayerPrefs.SetInt("Music_SongIndex", id);
            PlayerPrefs.Save();

            music_AudioSource.clip = music_Songs[id];
            music_AudioSource.pitch = 1f;
            music_AudioSource.Play();

            StopAllCoroutines();
            StartCoroutine(FastMusic());
        }
        else
        {
            Debug.LogWarning($"PlaySongByID: invalid id {id}");
        }
    }
}
