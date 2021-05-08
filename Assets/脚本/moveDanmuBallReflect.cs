using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDanmuBallReflect : moveDanmuBall {
    
    private int reflectTimes = 5;//反弹次数，防止水平循环反弹
    void Start() {

    }

    void FixedUpdate() {
        transform.position = transform.position + transform.up * speedDanmuBall * Time.fixedDeltaTime;
        if (reflectTimes > 0 && Boundary.IsOut(transform.position)) {
            transform.up = CheckPos();
            reflectTimes--;
        }
    }

    Vector3 CheckPos() {
        if (transform.position.x < Boundary.xMin || transform.position.x > Boundary.xMax) {
            return Vector3.Reflect(transform.up, new Vector3(1f, 0f, 0f));
        }
        return transform.up;
    }
}
