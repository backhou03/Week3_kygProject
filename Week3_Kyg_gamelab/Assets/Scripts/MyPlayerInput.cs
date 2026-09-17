using UnityEngine;
using UnityEngine.InputSystem;
public class MyPlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 move;
    public Vector2 look;

    private void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        if (actions == null)
        { return; }

        actions.Player.Enable();
        actions.Player.Move.started += OnMove;
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;

        actions.Player.Look.started += OnLook;
        actions.Player.Look.performed += OnLook;
        actions.Player.Look.canceled += OnLook;

    }
    private void OnDisable()
    {
        if (actions == null)
        { return; }
        actions.Player.Move.started -= OnMove;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Move.canceled -= OnMove;

        actions.Player.Look.started -= OnLook;
        actions.Player.Look.performed -= OnLook;
        actions.Player.Look.canceled -= OnLook;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            move = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            move = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            move = Vector2.zero;
        }

    }
    private void OnLook(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            look = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            look = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            look = Vector2.zero;
        }
    }
}
