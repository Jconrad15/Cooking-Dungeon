using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueDisplayer : MonoBehaviour
{
    private KeyCode nextButton = KeyCode.Space;

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject dialogueBox;

    private PlayerController playerController;

    // Start with dialogueBox off
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        dialogueBox.SetActive(false);
    }

    public void NewConversation(string[] dialogue)
    {
        playerController.DisableMovement();
        ShowDialogueBox();
        StartCoroutine(DisplayConversation(dialogue));
    }

    private IEnumerator DisplayConversation(string[] dialogue)
    {
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
                if (Input.GetKeyDown(nextButton))
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
        dialogueBox.SetActive(false);
    }

}
