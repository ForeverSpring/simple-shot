using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Bullet") {
            Debug.Log("»÷ÖÐBoss");
            FukaProcess.Instance.SetNowLife(FukaProcess.Instance.NowLife - 1);
            FukaProcess.Instance.UpdateProcess();
        }
    }
}
