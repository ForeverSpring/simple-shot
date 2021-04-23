using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPlayer : MonoBehaviour {
    private float speedPlayerMove1 = 9;
    private float speedPlayerMove2 = 4.5f;
    private float xMin = -10f, xMax = 10f, yMin = -4.7f, yMax = 4.7f;
    private Rigidbody rb;
    private bool inLowSpeed;

    void Update() {
        if (inLowSpeed) {
            this.GetComponent<Renderer>().enabled = true;
        }
        else {
            this.GetComponent<Renderer>().enabled = false;
        }
    }

    void Start() {
        inLowSpeed = false;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float moveHorizontal = 0;
        float moveVertical = 0;
        float moveSpeed = speedPlayerMove1;
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
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = speedPlayerMove2;
            inLowSpeed = true;
        }
        else {
            inLowSpeed = false;
        }
        Vector3 movement;
        if (moveHorizontal != 0 || moveVertical != 0) {
            float temp = Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical);
            movement = new Vector3(moveHorizontal / temp, moveVertical / temp, 0.0f);
        }
        else {
            movement = new Vector3(0f, 0f, 0f);
        }
        rb.velocity = movement * moveSpeed;
        /*限制坐标*/
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, xMin, xMax),
        Mathf.Clamp(rb.position.y, yMin, yMax),
        0.0f);
    }
}
