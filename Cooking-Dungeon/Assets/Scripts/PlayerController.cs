using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject surfaceWorld;
    [SerializeField]
    private GameObject dungeonWorld;

    public bool IsOnSurface { get; private set; } = true;
    private readonly float surfaceOffset = 0.5f;
    private readonly float dungeonOffset = -0.5f;
    private float currentOffset;

    private Queue<KeyCode> actions;

    private bool isMoving;
    private bool movementDisabled;

    private readonly float moveDuration = 0.2f;
    private readonly float flipDuration = 1f;

    private Action cbOnMove;
    private Action cbOnRotate;
    private Action cbOnStartFlip;
    private Action cbOnEndFlip;
    private Action cbOnMidFlip;

    private Action<NPC> cbOnStartTalkToNPC;
    private Action<Combatant> cbOnStartCombat;
    private Action<Ingredient> cbOnRunIntoItem;
    private Action<CookStation> cbOnStartCook;
    private Action<Healer> cbOnRunIntoHealer;

    private LayerMask movementMask;

    private void Start()
    {
        // This is starting the game on the surface
        IsOnSurface = true;
        currentOffset = surfaceOffset;

        movementDisabled = true;
        isMoving = false;

        actions = new Queue<KeyCode>();
        InitializeMovementMask();
    }

    private void InitializeMovementMask()
    {
        string[] layerNames = new string[7]
        { "Wall", "NPC", "Enemy", "Ingredient",
            "CookStation", "Door", "Healer" };
        movementMask = LayerMask.GetMask(layerNames);
    }

    private void Update()
    {
        if (movementDisabled) { return; }

        CheckForInput();

        if (isMoving == false)
        {
            PerformNextAction();
        }
    }

    /// <summary>
    /// Check if the player hits a key
    /// </summary>
    private void CheckForInput()
    {
        if (Input.GetKeyDown(InputKeyCodes.Instance.FlipKey))
        {
            actions.Enqueue(InputKeyCodes.Instance.FlipKey);
        }

        if (Input.GetKeyDown(InputKeyCodes.Instance.ForwardKey))
        {
            actions.Enqueue(InputKeyCodes.Instance.ForwardKey);
        }

        if (Input.GetKeyDown(InputKeyCodes.Instance.BackwardKey))
        {
            actions.Enqueue(InputKeyCodes.Instance.BackwardKey);
        }

        if (Input.GetKeyDown(InputKeyCodes.Instance.LeftKey))
        {
            actions.Enqueue(InputKeyCodes.Instance.LeftKey);
        }

        if (Input.GetKeyDown(InputKeyCodes.Instance.RightKey))
        {
            actions.Enqueue(InputKeyCodes.Instance.RightKey);
        }

        if (Input.GetKeyDown(InputKeyCodes.Instance.TurnLeftKey))
        {
            actions.Enqueue(InputKeyCodes.Instance.TurnLeftKey);
        }

        if (Input.GetKeyDown(InputKeyCodes.Instance.TurnRightKey))
        {
            actions.Enqueue(InputKeyCodes.Instance.TurnRightKey);
        }
    }

    /// <summary>
    /// Perform the next action in the queue
    /// </summary>
    public void PerformNextAction()
    {
        if (actions.Count == 0)
        {
            isMoving = false;
            return;
        }

        KeyCode nextAction = actions.Dequeue();

        if (nextAction == InputKeyCodes.Instance.ForwardKey)
        {
            MoveForward();
        }
        else if (nextAction == InputKeyCodes.Instance.BackwardKey)
        {
            MoveBackward();
        }
        else if (nextAction == InputKeyCodes.Instance.LeftKey)
        {
            MoveLeft();
        }
        else if (nextAction == InputKeyCodes.Instance.RightKey)
        {
            MoveRight();
        }
        else if (nextAction == InputKeyCodes.Instance.TurnLeftKey)
        {
            TurnLeft();
        }
        else if (nextAction == InputKeyCodes.Instance.TurnRightKey)
        {
            TurnRight();
        }
        else if (nextAction == InputKeyCodes.Instance.FlipKey)
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
        if (CanMoveDirection(Direction.Forward) == false) { return; }
        Vector3 endLocation = transform.position + transform.forward;
        cbOnMove?.Invoke();
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveBackward()
    {
        if (CanMoveDirection(Direction.Backward) == false) { return; }
        Vector3 endLocation = transform.position - transform.forward;
        cbOnMove?.Invoke(); 
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveLeft()
    {
        if (CanMoveDirection(Direction.Left) == false) { return; }
        Vector3 endLocation = transform.position - transform.right;
        cbOnMove?.Invoke(); 
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void MoveRight()
    {
        if (CanMoveDirection(Direction.Right) == false) { return; }
        Vector3 endLocation = transform.position + transform.right;
        cbOnMove?.Invoke(); 
        StartCoroutine(LerpToPosition(transform.position, endLocation));
    }

    private void TurnLeft()
    {
        Quaternion endRotation =
            transform.rotation * Quaternion.Euler(0, -90, 0);
        cbOnRotate?.Invoke();
        StartCoroutine(LerpToRotation(transform.rotation, endRotation));
    }

    private void TurnRight()
    {
        Quaternion endRotation =
            transform.rotation * Quaternion.Euler(0, 90, 0);
        cbOnRotate?.Invoke();
        StartCoroutine(LerpToRotation(transform.rotation, endRotation));
    }

    private void SwitchWorlds()
    {
        if (CanSwitchWorldsHere() == false)
        {
            Debug.Log("Cannot switch here");
            return;
        }

        cbOnStartFlip?.Invoke();
        StartCoroutine(LerpToFlip());
    }

    private bool CanSwitchWorldsHere()
    {
        Vector3 currentLocation = transform.position;
        currentLocation.y += currentOffset;
        float radius = 0.2f;

        int maxColliders = 4;
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

    public void EnableMovement()
    {
        _ = StartCoroutine(EnableMovementDelayed());
    }

    private IEnumerator EnableMovementDelayed()
    {
        yield return new WaitForEndOfFrame();
        movementDisabled = false;
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

        // Hard set end location in case things end weirdly
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

        // Hard set end location in case things end weirdly
        transform.rotation = endRotation;
        isMoving = false;
    }

    private IEnumerator LerpToFlip()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation =
            transform.rotation * Quaternion.Euler(0, 0, 180f);

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

            // Check if mid flip
            if(Mathf.Abs(transform.rotation.z) == 90 ||
               Mathf.Abs(transform.rotation.z) == 270)
            {
                cbOnMidFlip?.Invoke();
            }

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

        // Switch boolean
        IsOnSurface = !IsOnSurface;
        // Switch surface data
        currentOffset = IsOnSurface ? surfaceOffset : dungeonOffset;
        cbOnEndFlip?.Invoke();
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
                target += transform.forward;
                //Debug.DrawRay(current, transform.forward, Color.green, 3);
                break;

            case Direction.Backward:
                target -= transform.forward;
                //Debug.DrawRay(current, -transform.forward, Color.green, 3);
                break;

            case Direction.Left:
                target -= transform.right;
                //Debug.DrawRay(current, -transform.right, Color.green, 3);
                break;

            case Direction.Right:
                target += transform.right;
                //Debug.DrawRay(current, transform.right, Color.green, 3);
                break;
        }

        // Check for colliders in the target location
        if (Physics.Linecast(
            current, target, out RaycastHit hitInfo, movementMask))
        {
            GameObject other = hitInfo.collider.gameObject;
            // Try to get components in other gameobject
            if (other.TryGetComponent(out Wall wall))
            {
                Debug.Log("Blocked by wall");
                return false;
            }

            if (other.TryGetComponent(out NPC npc))
            {
                // Only talk forward
                if (d != Direction.Forward)
                {
                    return false;
                }

                cbOnStartTalkToNPC?.Invoke(npc);
                Debug.Log("TalkToNPC");
                return false;
            }

            if (other.TryGetComponent(out Combatant combatant))
            {
                // Only combat forward
                if (d != Direction.Forward)
                {
                    return false;
                }

                cbOnStartCombat?.Invoke(combatant);
                Debug.Log("StartCombat");
                return false;
            }

            if (other.TryGetComponent(out Ingredient ingredient))
            {
                // Only collect item forward
                if (d != Direction.Forward)
                {
                    return false;
                }

                cbOnRunIntoItem?.Invoke(ingredient);
                Debug.Log("RunIntoItem");
                return false;
            }

            if (other.TryGetComponent(out CookStation CookStation))
            {
                // Only cook forward
                if (d != Direction.Forward)
                {
                    return false;
                }

                cbOnStartCook?.Invoke(CookStation);
                Debug.Log("StartCooking");
                return false;
            }

            if (other.TryGetComponent(out Door door))
            {
                Debug.Log("Blocked by door");
                return false;
            }

            if (other.TryGetComponent(out Healer healer))
            {
                // Only heal forward
                if (d != Direction.Forward)
                {
                    return false;
                }

                Debug.Log("Blocked by Healer");
                cbOnRunIntoHealer?.Invoke(healer);
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

    public void RegisterOnStartFlip(Action callbackfunc)
    {
        cbOnStartFlip += callbackfunc;
    }

    public void UnregisterOnStartFlip(Action callbackfunc)
    {
        cbOnStartFlip -= callbackfunc;
    }

    public void RegisterOnMidFlip(Action callbackfunc)
    {
        cbOnMidFlip += callbackfunc;
    }

    public void UnregisterOnMidFlip(Action callbackfunc)
    {
        cbOnMidFlip -= callbackfunc;
    }

    public void RegisterOnEndFlip(Action callbackfunc)
    {
        cbOnEndFlip += callbackfunc;
    }

    public void UnregisterOnEndFlip(Action callbackfunc)
    {
        cbOnEndFlip -= callbackfunc;
    }

    public void RegisterOnStartTalkToNPC(Action<NPC> callbackfunc)
    {
        cbOnStartTalkToNPC += callbackfunc;
    }

    public void UnregisterOnStartTalkToNPC(Action<NPC> callbackfunc)
    {
        cbOnStartTalkToNPC -= callbackfunc;
    }

    public void RegisterOnStartCombat(Action<Combatant> callbackfunc)
    {
        cbOnStartCombat += callbackfunc;
    }

    public void UnregisterOnStartCombat(Action<Combatant> callbackfunc)
    {
        cbOnStartCombat -= callbackfunc;
    }

    public void RegisterOnRunIntoItem(Action<Ingredient> callbackfunc)
    {
        cbOnRunIntoItem += callbackfunc;
    }

    public void UnregisterOnRunIntoItem(Action<Ingredient> callbackfunc)
    {
        cbOnRunIntoItem -= callbackfunc;
    }

    public void RegisterOnStartCook(Action<CookStation> callbackfunc)
    {
        cbOnStartCook += callbackfunc;
    }

    public void UnregisterOnStartCook(Action<CookStation> callbackfunc)
    {
        cbOnStartCook -= callbackfunc;
    }

    public void RegisterOnRunIntoHealer(Action<Healer> callbackfunc)
    {
        cbOnRunIntoHealer += callbackfunc;
    }

    public void UnregisterOnRunIntoHealer(Action<Healer> callbackfunc)
    {
        cbOnRunIntoHealer -= callbackfunc;
    }
}
