using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReimuControl : MonoBehaviour {
    private Rigidbody rb;
    private float moveSpeed = 5f;
    public Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GameObject.Find("PlayerTex").GetComponent<Animator>();
    }

    void Update() {
    }

    void FixedUpdate() {
        int moveVertical = 0, moveHorizontal = 0;
        if (Input.GetKey(KeyCode.UpArrow)) {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            moveVertical = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveHorizontal = 1;
        }
        Vector3 movement = Vector3.zero;
        if (moveHorizontal != 0 || moveVertical != 0) {
            float temp = Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical);
            movement = new Vector3(moveHorizontal / temp, moveVertical / temp, 0.0f);
        }
        animator.SetFloat("left", -movement.x);
        animator.SetFloat("right", movement.x);
        rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

    }

}
