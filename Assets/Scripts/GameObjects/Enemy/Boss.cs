using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            other.gameObject.SetActive(false);
            UpdateProcess();
            AudioControl.Instance.PlayBulletHurtEnemy();
        }
    }

    void UpdateProcess() {
        if (GameControl.Instance.GetRunningFuka().fukaType == Fuka.FukaType.LifeFuka) {
            FukaProcess.Instance.SetNowLife(FukaProcess.Instance.NowLife - 1);
        }
    }
}
