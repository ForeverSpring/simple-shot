using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmuPool : Singleton<DanmuPool>
{
    public List<GameObject> mArrDanmu = new List<GameObject>();
    public void ClearDanmu() {
        Debug.Log("clear screen danmu.");
        foreach(GameObject obj in mArrDanmu) {
            Destroy(obj);
        }
    }
}
