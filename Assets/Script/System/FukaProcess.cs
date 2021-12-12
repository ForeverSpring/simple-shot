using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukaProcess : Singleton<FukaProcess>
{
    public int BossLife { get; private set; }
    public int NowLife { get; private set; }
    
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
        GameUIControl.Instance.UpdateTopSlide(NowLife / (float)BossLife);
    }
}
