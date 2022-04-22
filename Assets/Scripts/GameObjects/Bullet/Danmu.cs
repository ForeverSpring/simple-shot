using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmu : MonoBehaviour {
    protected Rigidbody2D rb;
    [SerializeField] public float speedDanmuBall { set; get; }
    [SerializeField] public float rotateSpeed { set; get; }
    [SerializeField] public Vector2 rotateCenter { set; get; }
    [SerializeField] public bool isRotating = false;
    private void Awake() {
        InitSettings();
    }
    protected void InitSettings() {
        rb = GetComponent<Rigidbody2D>();
        speedDanmuBall = GameSettings.Instance.settings.danmuMoveSpeed;
        rotateSpeed = GameSettings.Instance.settings.danmuRotateSpeed;
    }
    private void OnDisable() {
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
