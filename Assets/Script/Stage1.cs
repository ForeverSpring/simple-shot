using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Fuka {
    void Start() {
        name = "Stage1";
    }

    public override void Run() {
        StartCoroutine(_Stage1());
    }

    public override void Stop() {
        StopCoroutine(_Stage1());
    }

    /// <summary>
    /// Ò»Ãæ·û¿¨
    /// </summary>
    IEnumerator _Stage1() {
        mainControl.WaitFuka();
        textStage.setText("Stage 1");
        textStage.printStage();
        yield return new WaitForSeconds(textStage.timeFull);
        mainControl.SignalFuka();
    }
}
