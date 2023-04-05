using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEndScreenManager : MonoBehaviour
{
    private KeyCode nextButton = KeyCode.Space;

    [SerializeField]
    private GameObject[] endStoryImages;
    private int currentIndex = 0;

    public void Init()
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
        if (currentIndex >= endStoryImages.Length)
        {
            index = endStoryImages.Length - 1;
        }

        endStoryImages[index].SetActive(true);
    }

    private void HideAll()
    {
        for (int i = 0; i < endStoryImages.Length; i++)
        {
            endStoryImages[i].SetActive(false);
        }
    }


}
