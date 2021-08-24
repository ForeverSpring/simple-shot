using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
    float moveHorizontal = 0;
    float moveVertical = 0;
    bool signalBomb = false;
    bool signalLowSpeed = false;
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
            signalBomb = true;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            signalLowSpeed = true;
        }
    }

    public void GetInputSingal(ref float _moveH, ref float _moveV, ref bool _signalBomb, ref bool _signalLowSpeed) {
        _moveH = this.moveHorizontal;
        _moveV = this.moveVertical;
        _signalBomb = this.signalBomb;
        _signalLowSpeed = this.signalLowSpeed;
    }

    public void Reset() {
        moveHorizontal = 0;
        moveVertical = 0;
        signalBomb = false;
        signalLowSpeed = false;
    }
}
