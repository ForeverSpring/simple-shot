using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDanmu : MonoBehaviour {

    void OnTriggerExit(Collider other) {
        if (other.tag == "Danmu") {
            Destroy(other.gameObject);
            return;
        }
        Debug.Log(other.name);
    }
}
