using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private GameObject giveMealUI;

    private PlayerController playerController;

    private NPC currentNPC;

    // Start with dialogueBox off
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        dialogueBox.SetActive(false);
        giveMealUI.SetActive(false);
    }

    public void NewConversation(NPC npc)
    {
        currentNPC = npc;
        playerController.DisableMovement();
        ShowDialogueBox();

        // If this NPC wants a meal
        if (npc.wantedMealData != null)
        {
            ShowGiveMealUI();
        }

        StartCoroutine(DisplayConversation(npc));
    }

    private IEnumerator DisplayConversation(NPC npc)
    {
        string[] dialogue = npc.dialogue;

        int index = 0;
        if (dialogue.Length <= 0)
        {
            Debug.LogError("This character has no dialogue");
            yield return null;
        }
        else
        {
            ShowText(dialogue[index]);

            bool done = false;
            while (done == false)
            {
                if (Input.GetKeyDown(
                    InputKeyCodes.Instance.DialogueNextKey))
                {
                    index++;
                    if (index >= dialogue.Length)
                    {
                        done = true;
                        break;
                    }
                    ShowText(dialogue[index]);
                }
                yield return null;
            }
        }

        Debug.Log("Conversation Done");
        HideDialogueBox();
        playerController.EnableMovement();
    }

    private void ShowText(string lineOfDialogue)
    {
        text.SetText(lineOfDialogue);
    }

    private void ShowDialogueBox()
    {
        dialogueBox.SetActive(true);
    }

    private void HideDialogueBox()
    {
        ShowText("...");
        HideGiveMealUI();
        dialogueBox.SetActive(false);
    }

    private void ShowGiveMealUI()
    {
        giveMealUI.SetActive(true);
    }

    private void HideGiveMealUI()
    {
        giveMealUI.SetActive(false);
    }

    public void TryGiveMealButton()
    {
        Inventory inv = FindFirstObjectByType<Inventory>();
        bool gaveMeal = inv.TryGiveMeal(currentNPC.wantedMealData);
        if (gaveMeal)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayGiveMealOutcome(currentNPC));
        }
    }

    private IEnumerator DisplayGiveMealOutcome(NPC npc)
    {
        HideGiveMealUI();
        string[] dialogue = npc.receiveMealDialogue;

        int index = 0;
        if (dialogue.Length <= 0)
        {
            Debug.LogError("This character has no dialogue");
            yield return null;
        }
        else
        {
            ShowText(dialogue[index]);

            bool done = false;
            while (done == false)
            {
                if (Input.GetKeyDown(
                    InputKeyCodes.Instance.DialogueNextKey))
                {
                    index++;
                    if (index >= dialogue.Length)
                    {
                        done = true;
                        break;
                    }
                    ShowText(dialogue[index]);
                }
                yield return null;
            }
        }

        Debug.Log("Conversation Done");
        HideDialogueBox();
        npc.ReceivedMeal();
        playerController.EnableMovement();
    }

}
