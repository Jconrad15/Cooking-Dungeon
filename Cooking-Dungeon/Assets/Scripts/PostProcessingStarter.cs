using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] volumes;

    private void Start()
    {
        for (int i = 0; i < volumes.Length; i++)
        {
            volumes[i].SetActive(true);
        }
    }

}
