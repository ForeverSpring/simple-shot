using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Fuka : MonoBehaviour
{
    //TODO:����ģʽ������Ļ
    public GameObject gameobjBoss;
    public Rigidbody rbBoss;
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
        rbBoss = gameobjBoss.GetComponent<Rigidbody>();
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
