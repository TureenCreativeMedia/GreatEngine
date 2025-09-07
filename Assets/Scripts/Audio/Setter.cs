using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Setter : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] private List<float> startVol = new List<float>();

    private float savedVolume;
    private float lastVolume = -1f;

    public List<GameObject> hasMusicObjects;

    void Awake()
    {
        hasMusicObjects = GameObject.FindGameObjectsWithTag("HasMusic").ToList();

        foreach (var obj in hasMusicObjects)
        {
            var source = obj.GetComponent<AudioSource>();
            if (source != null)
            {
                startVol.Add(source.volume);
                audioSources.Add(source);
            }
        }
    }

    void Update()
    {
        float currentVolume = PlayerPrefs.GetFloat("Volume", 0.7f);
        if (!Mathf.Approximately(currentVolume, lastVolume))
        {
            ApplyVolume(currentVolume);
            lastVolume = currentVolume;
        }
    }

    private void ApplyVolume(float volume)
    {
        savedVolume = volume;

        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i] != null)
                audioSources[i].volume = savedVolume * startVol[i];
        }
    }
}
