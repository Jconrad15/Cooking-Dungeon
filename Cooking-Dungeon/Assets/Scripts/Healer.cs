using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    private PlayerController pc;

    private void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        pc.RegisterOnRunIntoHealer(OnRunIntoHealer);
    }

    private void OnRunIntoHealer(Healer h)
    {
        if (h != this) { return; }

        pc.GetComponent<Health>().HealAll();

    }
}