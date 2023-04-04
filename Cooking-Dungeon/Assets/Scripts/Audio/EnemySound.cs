using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySound : MonoBehaviour
{
    [SerializeField]
    private AudioClip attack;
    [SerializeField]
    private AudioClip hurt;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponentInParent<Health>()
            .RegisterOnHealthChanged(OnHealthChanged);
        GetComponentInParent<Combatant>()
            .RegisterOnAttack(OnAttack);
    }

    private void OnAttack(Combatant combatant)
    {
        audioSource.PlayOneShot(attack);
    }

    private void OnHealthChanged(int amount, bool increased)
    {
        if (increased == false)
        {
            audioSource.PlayOneShot(hurt);
        }

    }

}
