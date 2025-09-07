using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public AudioSource BirbSoundSource;

    public AudioClip DeathSound;
    public AudioClip ScoreSound;
    public AudioClip[] FlarpSound;

    public void MakeSound(string soundByteName)
    {
        if (CONSTANTS.GAME_PAUSED) return;

        switch (soundByteName)
        {
            default:
                {
                    BirbSoundSource.PlayOneShot(FlarpSound[Random.Range(0, FlarpSound.Length)]);
                    break;
                }
            case "Death":
                {
                    BirbSoundSource.PlayOneShot(DeathSound);
                    break;
                }
            case "SlowMusic":
                {
                    var m_local = CONSTANTS.MUSIC_OBJ.GetComponent<Music>();
                    m_local.StartCoroutine(m_local.SlowMusic());
                    break;
                }
            case "Score":
                {
                    BirbSoundSource.PlayOneShot(ScoreSound);
                    break;
                }
            }
    }
}