using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBulletPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject objBoss;
    private float mBulletSpeed;
    private void Awake() {
        mBulletSpeed = GameSettings.Instance.playerBulletSpeed;
    }
    private void Start() {
        objBoss = GameObject.Find("Boss");
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        move();
    }

    private void move() {
        transform.up = objBoss.transform.position - rb.transform.position;
        transform.position = transform.position + transform.up * mBulletSpeed * Time.fixedDeltaTime;
    }
}
