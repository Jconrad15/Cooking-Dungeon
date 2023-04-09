using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject escapeMenu;

    private bool isOpen;
    private PlayerController playerController;

    private void Start()
    {
        escapeMenu.SetActive(false);
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI();
        }
    }

    public void ToggleUI()
    {
        if (isOpen)
        {
            isOpen = false;
            playerController.EnableMovement();
        }
        else
        {
            isOpen = true;
            playerController.DisableMovement();
        }

        escapeMenu.SetActive(isOpen);
    }


}
