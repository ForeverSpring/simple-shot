using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName = "Player Input")]//可以直接在Project右键创建
public class PlayerInput : ScriptableObject, InputActions.IGamePlayActions {
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };
    public event UnityAction onLowSpeed = delegate { };
    public event UnityAction onNormalSpeed = delegate { };
    InputActions InputActions;
    void OnEnable() {
        InputActions = new InputActions();
        InputActions.GamePlay.SetCallbacks(this);
    }

    void OnDisable() {
        DisableAllInputs();
    }

    public void EnableGameplayInput() {
        InputActions.GamePlay.Enable();
    }

    public void DisableAllInputs() {
        InputActions.GamePlay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            onMove.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled) {
            onStopMove.Invoke();
        }
    }

    public void OnLowSpeed(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            onLowSpeed.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled) {
            onNormalSpeed.Invoke();
        }
    }
}
