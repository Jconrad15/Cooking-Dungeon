using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CookingSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip cook;
    [SerializeField]
    private AudioClip failToCook;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        CookingDisplayer cookingDisplayer = 
            FindAnyObjectByType<CookingDisplayer>();
        cookingDisplayer.RegisterOnCooked(OnCook);
        cookingDisplayer.RegisterOnFailedToCook(OnFailedToCook);
    }

    private void OnCook()
    {
        audioSource.PlayOneShot(cook);
        Debug.Log("PlayCookSound");
    }

    private void OnFailedToCook()
    {
        audioSource.PlayOneShot(failToCook);
    }


}
