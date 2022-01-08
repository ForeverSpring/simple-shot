using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private float mBulletSpeed;
    private void Awake() {
        mBulletSpeed = GameSettings.Instance.playerBulletSpeed;
    }
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate() {
        move();
    }

    private void move() {
        transform.position = transform.position + transform.up * mBulletSpeed * Time.fixedDeltaTime;
    }
}
