using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public int damageDealt = 1;
    public string combatantName;

    public Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

}
