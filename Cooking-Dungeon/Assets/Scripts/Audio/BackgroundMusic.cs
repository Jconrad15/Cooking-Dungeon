using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] clips;

    int currentClip = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentClip = 0;
        audioSource.Play();
    }

    private void Update()
    {
        if (audioSource.isPlaying == false)
        {
            currentClip++;
            if (currentClip == clips.Length)
            {
                currentClip = 0;
            }

            audioSource.clip = clips[currentClip];
            audioSource.Play();
        }
    }


}
