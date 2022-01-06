using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Fuka : MonoBehaviour
{
    //TODO:工厂模式创建弹幕
    //TODO:pool模式用于符卡结束时清除弹幕
    public GameObject gameobjBoss;
    public GameObject gameobjDanmuBall;
    public GameObject gameobjDanmuBallReflect;
    public Rigidbody rbBoss;
    public AudioSource BossRayShot;
    public AudioSource BossTan01;
    public texStage textStage;
    public bool running;
    public string fukaName;
    public int BossLife;
    public float FukaTime;
    public FukaType fukaType;
    public Vector3 vBossSpawn;
    //TODO:符卡类面向对象
    public enum FukaType {
        TimeFuka,LifeFuka,NULL
    }
    void Awake() {
        gameobjBoss = GameObject.Find("Boss");
        gameobjDanmuBall = (GameObject)Resources.Load("Prefab/danmuBall");
        gameobjDanmuBallReflect = (GameObject)Resources.Load("Prefab/danmuBallReflect");
        rbBoss = gameobjBoss.GetComponent<Rigidbody>();
        BossRayShot = GameObject.Find("ASRayShot").GetComponent<AudioSource>();
        BossTan01 = GameObject.Find("ASTan01").GetComponent<AudioSource>();
        textStage = GameObject.Find("texStage").GetComponent<texStage>();
        fukaType = FukaType.NULL;
        fukaName = "";
        running = false;
    }

    public abstract void Run();

    /// <summary>
    /// Stop() use method
    /// if Fuka is life type, Stop() in FukaProcess
    ///            time type, Stop() in GameController TODO:conbine in one class
    /// use Singal Var in Stop()
    /// </summary>
    public abstract void Stop();

}
