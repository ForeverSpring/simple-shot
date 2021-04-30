using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDanmu : MonoBehaviour {
    //边界盒消除离开的弹幕
    void OnTriggerExit(Collider other) {
        if (other.tag == "Danmu") {
            Destroy(other.gameObject);
            return;
        }
        Debug.Log(other.name);
    }
}
