using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject storyEndScreensPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerEnd();
        }
    }

    public void TriggerEnd()
    {
        FindAnyObjectByType<PlayerController>().DisableMovement();
        GameObject endScreens = Instantiate(
            storyEndScreensPrefab, canvas.transform);
        endScreens.GetComponent<StoryEndScreenManager>().Init();
    }

}
