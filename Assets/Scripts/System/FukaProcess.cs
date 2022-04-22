using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FukaProcess : Singleton<FukaProcess> {
    public int BossLife { get; private set; }
    public int NowLife { get; private set; }
    private void FixedUpdate() {
        CheckProcess();
    }
    void CheckProcess() {
        if(GameControl.Instance.GetRunningFuka() != null) {
            if (Instance.NowLife <= 0 && GameControl.Instance.GetRunningFuka().fukaType == Fuka.FukaType.LifeFuka) {
                GameControl.Instance.GetRunningFuka().Stop();
            }
        }
    }
    public void SetNewProcessData(int aBossLife) {
        BossLife = aBossLife;
        NowLife = aBossLife;
        UpdateProcess();
    }
    public void SetBossLife(int aBossLife) {
        BossLife = aBossLife;
        UpdateProcess();
    }
    public void SetNowLife(int aNowLife) {
        NowLife = aNowLife;
        UpdateProcess();
    }
    public void UpdateProcess() {
        GameUIControl.Instance.SetTopSlide(NowLife / (float)BossLife);
    }
}
