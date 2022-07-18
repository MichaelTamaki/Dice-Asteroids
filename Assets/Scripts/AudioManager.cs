using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioClip gameOverEffect;
    [SerializeField] private AudioClip boomEffect;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusicSource.Play();
    }

    public void TriggerGameOverSound()
    {
        backgroundMusicSource.clip = gameOverEffect;
        backgroundMusicSource.volume = 1.0f;
        backgroundMusicSource.loop = false;
        backgroundMusicSource.Play();
    }
}
