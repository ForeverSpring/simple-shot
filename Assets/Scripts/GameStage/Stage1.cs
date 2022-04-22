using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Fuka {
    void Start() {
        fukaName = "Stage1";
    }

    public override void Run() {
        StartCoroutine("_Stage1");
    }

    public override void Stop() {
        StopCoroutine("_Stage1");
    }

    /// <summary>
    /// 一面符卡
    /// </summary>
    IEnumerator _Stage1() {
        GameUIControl.Instance.SetTopSlideVisiable(false);
        GameUIControl.Instance.PrintStageText("Stage 1");
        GameControl.Instance.WaitFuka();
        yield return new WaitForSeconds(7f);
        GameControl.Instance.SignalFuka();
    }
}
