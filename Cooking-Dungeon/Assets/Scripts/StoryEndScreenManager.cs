using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEndScreenManager : MonoBehaviour
{
    private KeyCode nextButton = KeyCode.Space;

    [SerializeField]
    private GameObject[] endStoryImages;

    public void Init()
    {
        HideAll();
        Show(0);
    }

    public void OptionA()
    {
        Show(1);
    }

    public void OptionB()
    {
        Show(2);
    }

    public void ShowCredits()
    {
        Show(3);
    }

    private void Show(int index)
    {
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
