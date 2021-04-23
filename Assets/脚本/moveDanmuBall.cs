using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDanmuBall : MonoBehaviour {
    private Rigidbody rb;
    public float speedDanmuBall = 5;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        rb.velocity = transform.up * speedDanmuBall;
    }

}
