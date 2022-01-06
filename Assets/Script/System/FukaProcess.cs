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
        this.BossLife = aBossLife;
        this.NowLife = aBossLife;
    }
    public void SetBossLife(int aBossLife) {
        this.BossLife = aBossLife;
    }
    public void SetNowLife(int aNowLife) {
        this.NowLife = aNowLife;
    }
    public void UpdateProcess() {
        GameUIControl.Instance.SetTopSlide(NowLife / (float)BossLife);
    }
}
