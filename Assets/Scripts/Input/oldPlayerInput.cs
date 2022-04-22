using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class oldPlayerInput : Singleton<oldPlayerInput>
{
    float moveHorizontal = 0;
    float moveVertical = 0;
    bool signalBomb = false;
    bool signalLowSpeed = false;
    bool signalFire = false;
    void Start() {
        Reset();
    }

    void FixedUpdate()
    {
        Reset();
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) {
            moveVertical = 1;
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) {
            moveVertical = -1;
        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
            moveHorizontal = -1;
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
            moveHorizontal = 1;
        }
        if (Keyboard.current.zKey.wasPressedThisFrame) {
            signalFire = true;
        }
        if (Keyboard.current.xKey.wasPressedThisFrame) {
            signalBomb = true;
        }
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame) {
            signalLowSpeed = true;
        }
    }

    public void GetInputSingal(ref float aMoveH, ref float aMoveV, ref bool aSignalBomb, ref bool aSignalLowSpeed,ref bool aSignalFire) {
        aMoveH = this.moveHorizontal;
        aMoveV = this.moveVertical;
        aSignalBomb = this.signalBomb;
        aSignalLowSpeed = this.signalLowSpeed;
        aSignalFire = this.signalFire;
    }

    public void Reset() {
        moveHorizontal = 0;
        moveVertical = 0;
        signalBomb = false;
        signalLowSpeed = false;
        signalFire = false;
    }
}
