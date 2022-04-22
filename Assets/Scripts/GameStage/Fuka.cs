using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Fuka : MonoBehaviour
{
    protected GameObject gameobjBoss => EnvironmentObjectsManager.Instance.BossObject;
    protected Rigidbody2D rbBoss=> EnvironmentObjectsManager.Instance.BossObject.GetComponent<Rigidbody2D>();
    public MyTimer timer;
    public bool running;
    public string fukaName;
    public FukaType fukaType;
    public int fukaScore;
    public enum FukaType {
        TimeFuka,LifeFuka,NULL
    }
    void Awake() {
        fukaType = FukaType.NULL;
        fukaName = "";
        running = false;
    }

    public virtual void Run() { }

    /// <summary>
    /// Stop() use method
    /// if Fuka is life type, Stop() in FukaProcess
    ///            time type, Stop() in GameController TODO:conbine in one class
    /// use Singal Var in Stop()
    /// </summary>
    public virtual void Stop() { }
    protected void FinishGetScore() {
        if (GameData.Instance == null)
            Debug.LogWarning("未找到游戏数据对象");
        else
            GameData.Instance.addScore(this.fukaScore + (int)GameData.Instance.numPower * 100);
        if (GameUIControl.Instance == null)
            Debug.LogWarning("未找到游戏UI对象");
        else
            GameUIControl.Instance.GetSpellCardBonus(this.fukaScore);
    }
}
