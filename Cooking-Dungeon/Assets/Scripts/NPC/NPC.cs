using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    PlayerController playerController;
    DialogueDisplayer dialogueDisplayer;

    [SerializeField]
    public string[] dialogue;

    [SerializeField]
    public MealData wantedMealData;

    [SerializeField]
    public string[] receiveMealDialogue;

    [SerializeField]
    private GameObject poofParticlesPrefab;

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

        dialogueDisplayer.NewConversation(this);
    }

    public void ReceivedMeal()
    {
        //TODO: particle effect poof?
        GameObject particles = Instantiate(poofParticlesPrefab);
        particles.transform.position = transform.position;
        Destroy(gameObject);
    }


}
