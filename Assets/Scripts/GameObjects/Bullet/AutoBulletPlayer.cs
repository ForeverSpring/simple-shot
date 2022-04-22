using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBulletPlayer : MonoBehaviour {
    private Rigidbody2D rb;
    private GameObject targetObj;
    const float maxBallisticAngle = 10f;
    private float mBulletSpeed;
    float ballisticAngle;
    private void Awake() {
        mBulletSpeed = GameSettings.Instance.settings.playerBulletSpeed;
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        targetObj = EnemyManager.Instance.GetRandomEnemy();
        ballisticAngle = Random.Range(-maxBallisticAngle, maxBallisticAngle);
    }
    private void FixedUpdate() {
        move();
    }
    void move() {
        if (targetObj != null && targetObj.activeSelf) {
            transform.up = targetObj.transform.position - rb.transform.position;
            transform.position = transform.position + transform.up * mBulletSpeed * Time.fixedDeltaTime;
            //为子弹添加弧度
            transform.rotation *= Quaternion.Euler(0f, 0f, ballisticAngle);
        }
        else {
            transform.position += transform.up * mBulletSpeed * Time.fixedDeltaTime;
        }
    }
}
