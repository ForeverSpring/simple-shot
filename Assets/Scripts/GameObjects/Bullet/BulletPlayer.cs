using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float mBulletSpeed;
    private void Awake() {
        mBulletSpeed = GameSettings.Instance.settings.playerBulletSpeed;
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate() {
        move();
    }

    private void move() {
        transform.position = transform.position + transform.up * mBulletSpeed * Time.fixedDeltaTime;
    }
}
