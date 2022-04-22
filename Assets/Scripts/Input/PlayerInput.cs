using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    PlayerInputActions playerInputActions;
    Vector2 axes => playerInputActions.GamePlay.Axes.ReadValue<Vector2>();
    public bool inLowSpeed => playerInputActions.GamePlay.LowSpeed.IsPressed();
    public bool Fire => playerInputActions.GamePlay.Fire.IsPressed();
    public bool Bomb => playerInputActions.GamePlay.Bomb.WasPressedThisFrame();
    public bool Move => MoveX || MoveY;
    public bool MoveX => AxisX != 0;
    public bool MoveY => AxisY != 0;
    public float AxisX => axes.x;
    public float AxisY => axes.y;
    public Vector2 Axis => axes;
    public bool Pause => playerInputActions.GamePlay.Pause.WasPressedThisFrame();
    public bool Exit => playerInputActions.GameMenu.Exit.WasPressedThisFrame();
    public bool Dialog => playerInputActions.GamePlay.Dialog.WasPressedThisFrame();
    public bool SkipDialog => playerInputActions.GamePlay.SkipDialog.WasPressedThisFrame();
    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }
    public void EnableGameplayInputs() {
        playerInputActions.GamePlay.Enable();
    }
    public void DisableGameplayInputs() {
        playerInputActions.GamePlay.Disable();
    }
    public void EnableGameMenuInputs() {
        playerInputActions.GameMenu.Enable();
    }
    public void DisableGameMenuInputs() {
        playerInputActions.GameMenu.Disable();
    }
    public void DisableFireAndBomb() {
        playerInputActions.GamePlay.Fire.Disable();
        playerInputActions.GamePlay.Bomb.Disable();
    }
    public void EnableFireAndBomb() {
        playerInputActions.GamePlay.Fire.Enable();
        playerInputActions.GamePlay.Bomb.Enable();
    }
    public void EnterSeedTime() {
        playerInputActions.GamePlay.LowSpeed.Disable();
        playerInputActions.GamePlay.Axes.Disable();
        playerInputActions.GamePlay.Pause.Disable();
    }
    public void ExitSeedTime() {
        playerInputActions.GamePlay.LowSpeed.Enable();
        playerInputActions.GamePlay.Axes.Enable();
        playerInputActions.GamePlay.Pause.Enable();
    }
}
