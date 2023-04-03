using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    PlayerController playerController;
    DialogueDisplayer dialogueDisplayer;

    [SerializeField]
    private string[] dialogue; 

    private void Start()
    {
        dialogueDisplayer = FindAnyObjectByType<DialogueDisplayer>();
        playerController = FindAnyObjectByType<PlayerController>();
        playerController.RegisterOnStartTalkToNPC(OnStartTalkToNPC);
    }

    private void OnStartTalkToNPC(NPC talkedToNPC)
    {
        // Is this the NPC the player is talking to
        if (talkedToNPC != this)
        {
            return;
        }




    }



}
