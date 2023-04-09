using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField]
    private bool isOnSurface;

    private void Update()
    {
        Vector3 worldPos =
            transform.position
            + Camera.main.transform.rotation * Vector3.forward;

        Vector3 worldUp = Vector3.up;
        if (isOnSurface == false)
        {
            worldUp = Vector3.down;
        }
        
        transform.LookAt(worldPos, worldUp);
    }

    public bool CheckIsOnSurface()
    {
        return isOnSurface;
    }

    public void SetIsOnSurface(bool isOnSurface)
    {
        this.isOnSurface = isOnSurface;
    }
}
