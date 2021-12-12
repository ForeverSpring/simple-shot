using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 判定点碰撞 : MonoBehaviour {
    //被弹幕击中触发着弹
    private bool CanBeShot=true;
    public float TimeMuteki = 3f;
    public AudioSource BeShot;

    private void Start() {
    }

    public bool isMuteki() {
        return !CanBeShot;
    }

    //外部调用无敌时间
    public void SetMutekiTime(float MutekiTime) {
        StartCoroutine(FlagChange(MutekiTime));
    }

    // 无敌时间内不可继续着弹
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
                GameControl.Instance.PlayerBeShot();
                BeShot.Play();
                StartCoroutine(FlagChange(TimeMuteki));
            }
        }
    }

    private void FixedUpdate() {
        if (!CanBeShot) {
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Debug MUTEKI";
        }
        else {
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Debug";
        }
    }
}
