using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStartScreenManager : MonoBehaviour
{
    private KeyCode nextButton = KeyCode.Space;

    [SerializeField]
    private GameObject[] startStoryImages;
    private int currentIndex = 0;

    private void Start()
    {
        HideAll();
        Show(currentIndex);
    }

    private void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(nextButton))
        {
            GoToNextStoryImage();
        }
    }

    private void GoToNextStoryImage()
    {
        currentIndex++;
        HideAll();
        Show(currentIndex);
    }

    private void Show(int index)
    {
        // Check if done
        if (currentIndex >= startStoryImages.Length)
        {
            FindAnyObjectByType<PlayerController>().EnableMovement();
            Destroy(gameObject);
            return;
        }

        startStoryImages[index].SetActive(true);
    }

    private void HideAll()
    {
        for (int i = 0; i < startStoryImages.Length; i++)
        {
            startStoryImages[i].SetActive(false);
        }
    }


}
