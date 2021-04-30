using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDanmuBall : MonoBehaviour {
    private Rigidbody rb;
    public float speedDanmuBall = 5;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        transform.position = transform.position + transform.up * speedDanmuBall * Time.fixedDeltaTime;
    }

    public float getSpeed() {
        return this.speedDanmuBall;
    }

    public void setTowards(Vector3 v) {
        rb.transform.up = v;
    }

    public void setSpeed(float speed) {
        speedDanmuBall = speed;
    }

    public void setRotation(float deg) {
        rb.transform.rotation = Quaternion.Euler(this.transform.forward * deg);
    }
}
