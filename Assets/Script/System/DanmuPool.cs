using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmuPool : Singleton<DanmuPool>
{
    public List<GameObject> mArrDanmu = new List<GameObject>();
    Transform poolParent;
    private void Start() {
        poolParent = new GameObject("DanmuPool").transform;
        poolParent.parent = transform;
    }

    public void ClearDanmu() {
        Debug.Log("clear screen danmu.");
        GameObject[] danmus = GameObject.FindGameObjectsWithTag("Danmu");
        foreach(GameObject danmu in danmus) {
            danmu.SetActive(false);
        }
        //foreach(GameObject obj in mArrDanmu) {
        //    Destroy(obj);
        //}
    }

    public void AddNew(GameObject danmu) {
        mArrDanmu.Add(danmu);
        danmu.transform.SetParent(poolParent);
    }
}
