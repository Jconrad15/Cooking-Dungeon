using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookStation : MonoBehaviour
{
    [SerializeField]
    private GameObject cookParticles;
    
    private void Start()
    {
        CookingDisplayer cookingDisplayer =
            FindAnyObjectByType<CookingDisplayer>();
        cookingDisplayer.RegisterOnCooked(OnCook);
    }

    private void OnCook()
    {
        GameObject particles = Instantiate(cookParticles);
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        particles.transform.position = pc.transform.position;
        particles.transform.rotation =
            Quaternion.LookRotation(transform.up);
    }


}
