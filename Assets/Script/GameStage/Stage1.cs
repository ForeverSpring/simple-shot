using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Fuka {
    void Start() {
        fukaName = "Stage1";
    }

    public override void Run() {
        GameUIControl.Instance.SetTopSlideVisiable(false);
        StartCoroutine("_Stage1");
    }

    public override void Stop() {
        StopCoroutine("_Stage1");
    }

    /// <summary>
    /// Ò»Ãæ·û¿¨
    /// </summary>
    IEnumerator _Stage1() {
        GameControl.Instance.WaitFuka();
        textStage.setText("Stage 1");
        textStage.printStage();
        yield return new WaitForSeconds(textStage.timeFull);
        GameControl.Instance.SignalFuka();
    }
}
