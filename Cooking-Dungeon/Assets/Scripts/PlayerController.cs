using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int floorSize = 1;

    private KeyCode forward = KeyCode.W;
    private KeyCode backward = KeyCode.S;

    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;

    private KeyCode turnLeft = KeyCode.Q;
    private KeyCode turnRight = KeyCode.E;

    private Queue<KeyCode> actions;

    private bool isMoving;

    private float lerpDuration = 0.2f;

    private WorldSwitcher worldSwitcher;

    private void Start()
    {
        actions = new Queue<KeyCode>();
        worldSwitcher = GetComponent<WorldSwitcher>();
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
        bool canMove = CanMoveDirection(Direction.Forward);
        if (canMove == false)
        { return; }
        Vector3 endLocation = transform.position + (floorSize * transform.forward);
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveBackward()
    {
        bool canMove = CanMoveDirection(Direction.Backward);
        if (canMove == false) { return; }
        Vector3 endLocation = transform.position - (floorSize * transform.forward);
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveLeft()
    {
        bool canMove = CanMoveDirection(Direction.Left);
        if (canMove == false) { return; }
        Vector3 endLocation = transform.position - (floorSize * transform.right);
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveRight()
    {
        bool canMove = CanMoveDirection(Direction.Right);
        if (canMove == false) { return; }
        Vector3 endLocation = transform.position + (floorSize * transform.right);
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
        isMoving = true;

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
        isMoving = true;

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

    /// <summary>
    /// Check if a wall is in the provided direction.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private bool CanMoveDirection(Direction d)
    {
        Vector3 current = transform.position;
        current.y += worldSwitcher.currentOffset;
        Debug.Log(worldSwitcher.currentOffset);
        Vector3 target = current;

        switch (d)
        {
            case Direction.Forward:
                target += (floorSize * transform.forward);
                Debug.DrawRay(current, transform.forward, Color.green, 3);
                break;

            case Direction.Backward:
                target -= (floorSize * transform.forward);
                Debug.DrawRay(current, -transform.forward, Color.green, 3);
                break;

            case Direction.Left:
                target -= (floorSize * transform.right);
                Debug.DrawRay(current, -transform.right, Color.green, 3);
                break;

            case Direction.Right:
                target += (floorSize * transform.right);
                Debug.DrawRay(current, transform.right, Color.green, 3);
                break;
        }

        if (Physics.Linecast(current, target, out RaycastHit hitInfo))
        {
            GameObject other = hitInfo.collider.gameObject;
            other.TryGetComponent(out Wall wall);
            if (wall != null)
            {
                //Debug.Log("Blocked by wall");
                return false;
            }
        }

        return true;
    }

}
