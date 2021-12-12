using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
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
        if (Input.GetKey(KeyCode.UpArrow)) {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            moveVertical = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveHorizontal = 1;
        }
        if (Input.GetKey(KeyCode.Z)) {
            signalFire = true;
        }
        if (Input.GetKey(KeyCode.X)) {
            signalBomb = true;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
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
