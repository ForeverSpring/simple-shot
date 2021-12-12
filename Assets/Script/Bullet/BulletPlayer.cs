using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    private Rigidbody rb;
    //TODO:use data class to store speed and can change it easily
    private float mBulletSpeed = 8f;
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
