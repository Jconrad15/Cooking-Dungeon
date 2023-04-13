using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CookingSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip cook;
    [SerializeField]
    private AudioClip failToCook;

    [SerializeField]
    private AudioClip backgroundA;
    [SerializeField]
    private AudioClip backgroundB;

    [SerializeField]
    private AudioSource audioSourceSFX;
    [SerializeField]
    private AudioSource audioSourceBG;

    private void Start()
    {
        CookingDisplayer cookingDisplayer = 
            FindAnyObjectByType<CookingDisplayer>();
        cookingDisplayer.RegisterOnCooked(OnCook);
        cookingDisplayer.RegisterOnFailedToCook(OnFailedToCook);
        cookingDisplayer.RegisterOnLeaveCookingStation(OnLeaveCookingStation);

        FindAnyObjectByType<PlayerController>()
            .RegisterOnStartCook(OnStartCook);
    }

    private void OnStartCook(CookStation cookStation)
    {
        audioSourceBG.PlayOneShot(backgroundA);
        audioSourceBG.PlayOneShot(backgroundB);
    }

    private void OnLeaveCookingStation()
    {
        audioSourceBG.Stop();
    }

    private void OnCook()
    {
        audioSourceSFX.PlayOneShot(cook);
    }

    private void OnFailedToCook()
    {
        audioSourceSFX.PlayOneShot(failToCook);
    }


}
