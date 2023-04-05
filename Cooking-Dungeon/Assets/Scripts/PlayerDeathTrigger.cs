using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject deathScreenPrefab;
    [SerializeField]
    private GameObject canvas;

    public void PlayerDied(PlayerController pc)
    {
        Debug.Log("PlayerDied");
        pc.DisableMovement();

        Instantiate(deathScreenPrefab, canvas.transform);
    }


}
