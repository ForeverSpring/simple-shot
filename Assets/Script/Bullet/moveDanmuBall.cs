using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDanmuBall : MonoBehaviour {
    private Rigidbody2D rb;
    public float speedDanmuBall { set; get; }
    public float rotateSpeed { set; get; }
    public Vector3 rotateCenter=new Vector3();
    public bool isRotating = false;

    void InitSettings() {
        rb = GetComponent<Rigidbody2D>();
        speedDanmuBall = GameSettings.Instance.danmuMoveSpeed;
        rotateSpeed = GameSettings.Instance.danmuRotateSpeed;
    }
    void Awake() {
        InitSettings();
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
