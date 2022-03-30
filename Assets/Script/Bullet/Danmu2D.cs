using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmu2D : MonoBehaviour
{
    [SerializeField] public bool move, rotate;
    [SerializeField] public float moveSpeed = 2f;
    private void Start() {
        move = true;
        rotate = false;
    }

    private void FixedUpdate() {
        if (move) {
            this.transform.position += this.transform.up.normalized * moveSpeed * Time.fixedDeltaTime;
        }
        if (rotate) {

        }
    }

}
