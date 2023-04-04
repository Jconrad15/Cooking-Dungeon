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
    [SerializeField]
    private AudioClip runIntoItem;
    [SerializeField]
    private AudioClip startCombat;
    [SerializeField]
    private AudioClip startTalkToNPC;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Register to callbacks
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        pc.RegisterOnMove(OnMove);
        pc.RegisterOnFlip(OnFlip);
        pc.RegisterOnRotate(OnRotate);
        pc.RegisterOnRunIntoItem(OnRunIntoItem);
        pc.RegisterOnStartCombat(OnStartCombat);
        pc.RegisterOnStartTalkToNPC(OnStartTalkToNPC);
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

    private void OnRunIntoItem(Ingredient ingredient)
    {
        audioSource.PlayOneShot(runIntoItem);
    }

    private void OnStartCombat(Combatant combatant)
    {
        audioSource.PlayOneShot(startCombat);
    }

    private void OnStartTalkToNPC(NPC npc)
    {
        audioSource.PlayOneShot(startTalkToNPC);
    }

}
