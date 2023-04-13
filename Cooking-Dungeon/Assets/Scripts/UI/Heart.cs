using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private GameObject[] heartPortions;

    public void SetPortions(int portions)
    {
        SetAllPortionsOff();
        
        if (portions > 4)
        {
            Debug.LogError("Too many portions");
            return;
        }
        else if (portions <= 0)
        {
            SetAllPortionsOff();
            return;
        }

        for (int i = 0; i < portions; i++)
        {
            heartPortions[i].SetActive(true);
        }
    }

    public void SetAllPortionsOff()
    {
        for (int i = 0; i < heartPortions.Length; i++)
        {
            heartPortions[i].SetActive(false);
        }
    }

}
