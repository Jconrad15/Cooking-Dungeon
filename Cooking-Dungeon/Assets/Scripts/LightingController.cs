using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{
    [SerializeField]
    private GameObject surfaceLighting;
    [SerializeField]
    private GameObject dungeonLighting;

    private PlayerController pc;

    private void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        pc.RegisterOnEndFlip(OnEndFlip);
        SwitchLightingTo(pc.IsOnSurface);
    }

    private void OnEndFlip()
    {
        SwitchLightingTo(pc.IsOnSurface);
    }

    private void SwitchLightingTo(bool isOnSurface)
    {
        if (isOnSurface)
        {
            surfaceLighting.SetActive(true);
            dungeonLighting.SetActive(false);
        }
        else
        {
            surfaceLighting.SetActive(false);
            dungeonLighting.SetActive(true);
        }
    }

}
