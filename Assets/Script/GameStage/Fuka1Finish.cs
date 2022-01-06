using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuka1Finish : Fuka
{
    private void Start() {
        fukaName = "Stage1Finish";
    }
    public override void Run() {
        Debug.Log("Fuka1Finish start");
        GameControl.Instance.WaitFuka();
        GameUIControl.Instance.SetTopSlideVisiable(false);
        StartCoroutine("Finish");
    }

    public override void Stop() {
        Debug.Log("Fuka1Finish end");
        StopCoroutine("Finish");
        DanmuPool.Instance.ClearDanmu();
        GameControl.Instance.SignalFuka();
    }

    IEnumerator Finish() {
        //show finish animation here
        GameControl.Instance.SetGameWin(true);
        AudioControl.Instance.StopBGM();
        yield return null;
    }
}
