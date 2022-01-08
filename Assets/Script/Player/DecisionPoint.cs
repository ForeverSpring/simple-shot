using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DecisionPoint : MonoBehaviour {
    //被弹幕击中触发着弹
    private bool CanBeShot=true;
    public float TimeMuteki = 3f;

    public bool isMuteki() {
        return !CanBeShot;
    }

    //外部调用无敌时间
    public void SetMutekiTime(float MutekiTime) {
        StartCoroutine(FlagChange(MutekiTime));
    }

    //无敌时间内不可继续着弹
    IEnumerator FlagChange(float MutekiTime) {
        CanBeShot = false;
        yield return new WaitForSeconds(MutekiTime);
        CanBeShot = true;
        yield return null;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Danmu") {
            //Debug.Log("着弹");
            if (CanBeShot) {
                GameData.Instance.PlayerBeShot();
                AudioControl.Instance.PlayBeShot();
                StartCoroutine(FlagChange(TimeMuteki));
            }
        }
    }
}
