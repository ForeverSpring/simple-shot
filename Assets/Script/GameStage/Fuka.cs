using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Fuka : MonoBehaviour
{
    public GameObject gameobjBoss;
    public Rigidbody2D rbBoss;
    public texStage textStage;
    public bool running;
    public string fukaName;
    public int BossLife;
    public float FukaTime;
    public FukaType fukaType;
    public Vector3 vBossSpawn;
    public enum FukaType {
        TimeFuka,LifeFuka,NULL
    }
    void Awake() {
        gameobjBoss = GameObject.Find("Boss");
        rbBoss = gameobjBoss.GetComponent<Rigidbody2D>();
        textStage = GameObject.Find("texStage").GetComponent<texStage>();
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

}
