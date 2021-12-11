using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDanmuBall : MonoBehaviour {
    private Rigidbody rb;
    public float speedDanmuBall = 5;
    public float rotateSpeed = 0.5f;
    public Vector3 rotateCenter=new Vector3();
    public bool isRotating = false;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        transform.position = transform.position + transform.up * speedDanmuBall * Time.fixedDeltaTime;
        if (isRotating) {
            transform.RotateAround(rotateCenter, Vector3.forward, (rotateSpeed * 180 / Mathf.PI) * Time.fixedDeltaTime);
        }
    }

    public float GetSpeed() {
        return this.speedDanmuBall;
    }

    public void SetTowards(Vector3 v) {
        rb.transform.up = v;
    }

    public void SetSpeed(float speed) {
        speedDanmuBall = speed;
    }

    public void SetRotation(float deg) {
        rb.transform.rotation = Quaternion.Euler(this.transform.forward * deg);
    }

    public void SetRotateCenter(Vector3 v) {
        rotateCenter = v;
    }

    public void SetRotateSpeed(float rspeed) {
        rotateSpeed = rspeed;
    }
}
