using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputService : MonoBehaviour, PlayerInput.IPlayerMotionMapActions
{
    public PlayerInput PlayerInput { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool RunInput { get; private set; }

    public event Action OnInteractInput;
    public static event Action OnEscapeInput;

    private void OnEnable()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();

        PlayerInput.PlayerMotionMap.Enable();
        PlayerInput.PlayerMotionMap.SetCallbacks(this);
    }
    private void OnDisable()
    {
        PlayerInput.PlayerMotionMap.Disable();
        PlayerInput.PlayerMotionMap.RemoveCallbacks(this);
    }

    // on input hold
    public void OnMove(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();
    public void OnLook(InputAction.CallbackContext context) => LookInput = context.ReadValue<Vector2>().normalized;
    public void OnRun(InputAction.CallbackContext context) => RunInput = context.performed;

    // on input click
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            OnInteractInput?.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
            OnEscapeInput?.Invoke();
    }
}
