using System;
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

    private KeyCode flip = KeyCode.Space;

    [SerializeField]
    private GameObject surfaceWorld;
    [SerializeField]
    private GameObject dungeonWorld;

    private bool isOnSurface;
    private float surfaceOffset = 0.5f;
    private float dungeonOffset = -0.5f;
    private float currentOffset;

    private Queue<KeyCode> actions;

    private bool isMoving;
    private bool movementDisabled;

    private float moveDuration = 0.2f;
    private float flipDuration = 1f;

    private Action cbOnMove;
    private Action cbOnRotate;
    private Action cbOnFlip;

    private Action<NPC> cbOnStartTalkToNPC;

    private void Start()
    {
        // This is currently starting the game in the safe world
        isOnSurface = true;
        currentOffset = surfaceOffset;

        movementDisabled = false;
        isMoving = false;

        actions = new Queue<KeyCode>();
    }

    private void Update()
    {
        if (movementDisabled)
        {
            return;
        }

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
        if (Input.GetKeyDown(flip))
        {
            actions.Enqueue(flip);
        }

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
        else if (nextAction == flip)
        {
            SwitchWorlds();
        }
        else
        {
            Debug.LogError("Why is there no action??");
        }
    }

    private void MoveForward()
    {
        bool canMove = CanMoveDirection(Direction.Forward);
        if (canMove == false) { return; }
        Vector3 endLocation = transform.position + (floorSize * transform.forward);
        cbOnMove?.Invoke();
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveBackward()
    {
        bool canMove = CanMoveDirection(Direction.Backward);
        if (canMove == false) { return; }
        Vector3 endLocation = transform.position - (floorSize * transform.forward);
        cbOnMove?.Invoke(); 
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveLeft()
    {
        bool canMove = CanMoveDirection(Direction.Left);
        if (canMove == false) { return; }
        Vector3 endLocation = transform.position - (floorSize * transform.right);
        cbOnMove?.Invoke(); 
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveRight()
    {
        bool canMove = CanMoveDirection(Direction.Right);
        if (canMove == false) { return; }
        Vector3 endLocation = transform.position + (floorSize * transform.right);
        cbOnMove?.Invoke(); 
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void TurnLeft()
    {
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
        cbOnRotate?.Invoke();
        StartCoroutine(LerpToRotation(transform.rotation, endRotation));
    }

    private void TurnRight()
    {
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        cbOnRotate?.Invoke();
        StartCoroutine(LerpToRotation(transform.rotation, endRotation));
    }

    private void SwitchWorlds()
    {
        bool canSwitch = CanSwitchHere();
        if (canSwitch == false)
        {
            Debug.Log("Cannot switch here");
            return;
        }

        cbOnFlip?.Invoke();
        StartCoroutine(LerpToFlip());
    }

    private bool CanSwitchHere()
    {
        Vector3 currentLocation = transform.position;
        currentLocation.y += currentOffset;
        float radius = 0.2f;

        int maxColliders = 2;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(
            currentLocation, radius, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            // Check if overlaping collider 
            hitColliders[i].TryGetComponent(out NoSwitchZone noSwitchZone);
            if (noSwitchZone != null)
            {
                // A no switch zone is present
                return false;
            }
        }

        return true;
    }

    public void DisableMovement()
    {
        movementDisabled = true;
    }

    private IEnumerator LerpToPosition(
        Vector3 startLocation, Vector3 endLocation)
    {
        isMoving = true;

        float timeElapsed = 0;
        while (timeElapsed < moveDuration)
        {
            Vector3 newPos = Vector3.Lerp(
                startLocation,
                endLocation,
                timeElapsed / moveDuration);

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
        while (timeElapsed < moveDuration)
        {
            Quaternion newRotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                timeElapsed / moveDuration);

            timeElapsed += Time.deltaTime;

            transform.rotation = newRotation;
            yield return null;
        }

        // Hard set end location incase things end weirdly
        transform.rotation = endRotation;
        isMoving = false;
    }

    private IEnumerator LerpToFlip()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 0, 180f);

        isMoving = true;

        float timeElapsed = 0;
        while (timeElapsed < flipDuration)
        {
            Quaternion newRotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                timeElapsed / flipDuration);

            timeElapsed += Time.deltaTime;

            transform.rotation = newRotation;
            yield return null;
        }

        // Hard set end location incase things end weirdly
        transform.rotation = endRotation;
        Vector3 roundedPos = transform.position;
        roundedPos.x = Mathf.RoundToInt(roundedPos.x);
        roundedPos.y = Mathf.RoundToInt(roundedPos.y);
        roundedPos.z = Mathf.RoundToInt(roundedPos.z);
        transform.position = roundedPos;
        isMoving = false;

        // Switch surface data
        if (isOnSurface)
        {
            currentOffset = dungeonOffset;
        }
        else
        {
            currentOffset = surfaceOffset;
        }
        // Switch boolean
        isOnSurface = !isOnSurface;
    }

    /// <summary>
    /// Check if a wall is in the provided direction.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private bool CanMoveDirection(Direction d)
    {
        Vector3 current = transform.position;
        current.y += currentOffset;
        Vector3 target = current;

        switch (d)
        {
            case Direction.Forward:
                target += (floorSize * transform.forward);
                //Debug.DrawRay(current, transform.forward, Color.green, 3);
                break;

            case Direction.Backward:
                target -= (floorSize * transform.forward);
                //Debug.DrawRay(current, -transform.forward, Color.green, 3);
                break;

            case Direction.Left:
                target -= (floorSize * transform.right);
                //Debug.DrawRay(current, -transform.right, Color.green, 3);
                break;

            case Direction.Right:
                target += (floorSize * transform.right);
                //Debug.DrawRay(current, transform.right, Color.green, 3);
                break;
        }

        // Check for colliders in the target location
        if (Physics.Linecast(current, target, out RaycastHit hitInfo))
        {
            GameObject other = hitInfo.collider.gameObject;
            // Try to get components in other gameobject
            other.TryGetComponent(out Wall wall);
            other.TryGetComponent(out NPC npc);

            if (wall != null)
            {
                //Debug.Log("Blocked by wall");
                return false;
            }
            if (npc != null)
            {
                Debug.Log("TalkToNPC");
                return false;
            }
        }

        return true;
    }

    public void RegisterOnMove(Action callbackfunc)
    {
        cbOnMove += callbackfunc;
    }

    public void UnregisterOnMove(Action callbackfunc)
    {
        cbOnMove -= callbackfunc;
    }

    public void RegisterOnRotate(Action callbackfunc)
    {
        cbOnRotate += callbackfunc;
    }

    public void UnregisterOnRotate(Action callbackfunc)
    {
        cbOnRotate -= callbackfunc;
    }

    public void RegisterOnFlip(Action callbackfunc)
    {
        cbOnFlip += callbackfunc;
    }

    public void UnregisterOnFlip(Action callbackfunc)
    {
        cbOnFlip -= callbackfunc;
    }

    public void RegisterOnStartTalkToNPC(Action<NPC> callbackfunc)
    {
        cbOnStartTalkToNPC += callbackfunc;
    }

    public void UnregisterOnStartTalkToNPC(Action<NPC> callbackfunc)
    {
        cbOnStartTalkToNPC -= callbackfunc;
    }
}
