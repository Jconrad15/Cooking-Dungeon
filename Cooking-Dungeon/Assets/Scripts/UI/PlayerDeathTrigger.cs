using UnityEngine;

public class PlayerDeathTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject deathScreenPrefab;
    [SerializeField]
    private GameObject canvas;

    public void PlayerDied(PlayerController pc)
    {
        pc.DisableMovement();
        _ = Instantiate(deathScreenPrefab, canvas.transform);
    }


}
