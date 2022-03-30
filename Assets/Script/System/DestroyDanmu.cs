using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDanmu : MonoBehaviour {
    //边界盒消除离开的弹幕
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Danmu" || other.tag == "Bullet") {
            DanmuPool.Instance.mArrDanmu.Remove(other.gameObject);
            Destroy(other.gameObject);
            return;
        }
        Debug.Log("Destory by Boundary:"+other.name);
    }
}
