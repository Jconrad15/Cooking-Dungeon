using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton that defines player inputs.
/// </summary>
public class InputKeyCodes : MonoBehaviour
{
    [SerializeField]
    private KeyCode forwardKey = KeyCode.W;
    public KeyCode ForwardKey => forwardKey;

    [SerializeField]
    private KeyCode backwardKey = KeyCode.S;
    public KeyCode BackwardKey => backwardKey;

    [SerializeField]
    private KeyCode leftKey = KeyCode.A;
    public KeyCode LeftKey => leftKey;

    [SerializeField] 
    private KeyCode rightKey = KeyCode.D;
    public KeyCode RightKey => rightKey;

    [SerializeField] 
    private KeyCode turnLeftKey = KeyCode.Q;
    public KeyCode TurnLeftKey => turnLeftKey;

    [SerializeField] 
    private KeyCode turnRightKey = KeyCode.E;
    public KeyCode TurnRightKey => turnRightKey;

    [SerializeField] 
    private KeyCode flipKey = KeyCode.Space;
    public KeyCode FlipKey => flipKey;

    [SerializeField]
    private KeyCode attackKey = KeyCode.Space;
    public KeyCode AttackKey => attackKey;

    [SerializeField]
    private KeyCode backKey = KeyCode.S;
    public KeyCode BackKey => backKey;

    [SerializeField]
    private KeyCode dialogueNextKey = KeyCode.Space;
    public KeyCode DialogueNextKey => dialogueNextKey;

    public KeyCode EscapeKey => KeyCode.Escape;

    // Make singleton
    public static InputKeyCodes Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
