using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private KeyCode forward = KeyCode.W;
    private KeyCode backward = KeyCode.S;

    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;

    private KeyCode turnLeft = KeyCode.Q;
    private KeyCode turnRight = KeyCode.E;

    private Queue<KeyCode> actions;

    private bool isMoving;

    private float lerpDuration = 0.2f;

    private void Start()
    {
        actions = new Queue<KeyCode>();
    }

    private void Update()
    {
        CheckForInput();

        if (isMoving == false)
        {
            PerformNextAction();
        }
    }

    /// <summary>
    /// Check if the player hits a set key
    /// </summary>
    private void CheckForInput()
    {
        if (Input.GetKeyDown(forward))
        {
            actions.Enqueue(forward);
        }
        if (Input.GetKeyDown(backward))
        {
            actions.Enqueue(backward);
        }

        if (Input.GetKeyDown(left))
        {
            actions.Enqueue(left);
        }
        if (Input.GetKeyDown(right))
        {
            actions.Enqueue(right);
        }

        if (Input.GetKeyDown(turnLeft))
        {
            actions.Enqueue(turnLeft);
        }
        if (Input.GetKeyDown(turnRight))
        {
            actions.Enqueue(turnRight);
        }
    }

    /// <summary>
    /// Perform the next action in the queue
    /// </summary>
    public void PerformNextAction()
    {
        if (actions.Count == 0)
        {
            //Debug.Log("No actions queued");
            isMoving = false;
            return;
        }

        isMoving = true;
        KeyCode nextAction = actions.Dequeue();

        if (nextAction == forward)
        {
            MoveForward();
        }
        else if (nextAction == backward)
        {
            MoveBackward();
        }
        else if (nextAction == left)
        {
            MoveLeft();
        }
        else if (nextAction == right)
        {
            MoveRight();
        }
        else if (nextAction == turnLeft)
        {
            TurnLeft();
        }
        else if (nextAction == turnRight)
        {
            TurnRight();
        }
        else
        {
            Debug.LogError("Why is there no action??");
        }
    }

    private void MoveForward()
    {
        Vector3 endLocation = transform.position + transform.forward;
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveBackward()
    {
        Vector3 endLocation = transform.position - transform.forward;
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveLeft()
    {
        Vector3 endLocation = transform.position - transform.right;
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveRight()
    {
        Vector3 endLocation = transform.position + transform.right;
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void TurnLeft()
    {
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
        StartCoroutine(LerpToRotation(transform.rotation, endRotation));
    }

    private void TurnRight()
    {
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        StartCoroutine(LerpToRotation(transform.rotation, endRotation));
    }

    private IEnumerator LerpToPosition(
        Vector3 startLocation, Vector3 endLocation)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            Vector3 newPos = Vector3.Lerp(
                startLocation,
                endLocation,
                timeElapsed / lerpDuration);

            timeElapsed += Time.deltaTime;

            transform.position = newPos;
            yield return null;
        }

        // Hard set end location incase things end weirdly
        transform.position = endLocation;
        isMoving = false;
    }

    private IEnumerator LerpToRotation(
        Quaternion startRotation, Quaternion endRotation)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            Quaternion newRotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                timeElapsed / lerpDuration);

            timeElapsed += Time.deltaTime;

            transform.rotation = newRotation;
            yield return null;
        }

        // Hard set end location incase things end weirdly
        transform.rotation = endRotation;
        isMoving = false;
    }

}
