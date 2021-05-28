using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 判定点碰撞 : MonoBehaviour {
    //被弹幕击中触发着弹
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Danmu") {
            //Debug.Log("着弹");
        }
    }
}
