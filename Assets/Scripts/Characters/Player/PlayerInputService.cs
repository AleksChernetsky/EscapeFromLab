using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputService : PlayerInput.IPlayerMotionMapActions
{
    public PlayerInput PlayerInput { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool RunInput { get; private set; }

    public event Action OnInteractInput;
    public static event Action OnEscapeInput;

    public void Initialize()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();

        PlayerInput.PlayerMotionMap.Enable();
        PlayerInput.PlayerMotionMap.SetCallbacks(this);
    }
    public void DisableInput()
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
        if (context.performed)
            OnInteractInput?.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnEscapeInput?.Invoke();
    }
}
