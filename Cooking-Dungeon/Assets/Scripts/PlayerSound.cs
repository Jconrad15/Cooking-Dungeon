using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip move;
    [SerializeField]
    private AudioClip flip;
    [SerializeField]
    private AudioClip rotate;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Register to callbacks
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        pc.RegisterOnMove(OnMove);
        pc.RegisterOnFlip(OnFlip);
        pc.RegisterOnRotate(OnRotate);
    }

    private void OnMove()
    {
        audioSource.PlayOneShot(move);
    }

    private void OnFlip()
    {
        audioSource.PlayOneShot(flip);
    }

    private void OnRotate()
    {
        audioSource.PlayOneShot(rotate);
    }
}
