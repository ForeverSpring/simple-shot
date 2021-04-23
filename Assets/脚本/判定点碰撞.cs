using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 判定点碰撞 : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Danmu") {
            Debug.Log("着弹");
        }
    }
}
