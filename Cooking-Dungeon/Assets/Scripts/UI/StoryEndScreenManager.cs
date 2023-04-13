using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEndScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] endStoryImages;

    private int currentIndex = 0;

    public void Init()
    {
        currentIndex = 0;
        Show(currentIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(InputKeyCodes.Instance.DialogueNextKey))
        {
            if (currentIndex == 0)
            {
                currentIndex++;
                Show(currentIndex);
            }
            else if(currentIndex == 1)
            {
                currentIndex++;
                Show(currentIndex);
            }
/*            else if (currentIndex == 2)
            {
                currentIndex++;
                Show(currentIndex);
            }*/

        }
    }


    public void OptionA()
    {
        Show(3);
    }

    public void OptionB()
    {
        Show(4);
    }

    public void ShowCredits()
    {
        Show(5);
    }

    private void Show(int index)
    {
        HideAll();
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
