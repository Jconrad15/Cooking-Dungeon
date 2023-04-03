using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    private void Update()
    {
        Vector3 worldPos =
            transform.position
            + Camera.main.transform.rotation * Vector3.forward;
        Vector3 worldUp = Vector3.up;
        transform.LookAt(worldPos, worldUp);

    }
}
