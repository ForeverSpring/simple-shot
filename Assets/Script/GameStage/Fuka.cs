using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Fuka : MonoBehaviour
{
    public GameObject gameobjBoss;
    public GameObject gameobjDanmuBall;
    public GameObject gameobjDanmuBallReflect;
    public Rigidbody rbBoss;
    public AudioSource BossRayShot;
    public AudioSource BossTan01;
    public texStage textStage;
    public bool running;
    public string name;
    public int BossLife;
    public float FukaTime;
    public FukaType type;//0:时符 1:一般符卡
    //TODO:符卡类面向对象
    public enum FukaType {
        TimeFuka,LifeFuka
    }
    void Awake() {
        gameobjBoss = GameObject.Find("Boss");
        gameobjDanmuBall = (GameObject)Resources.Load("Prefab/danmuBall");
        gameobjDanmuBallReflect = (GameObject)Resources.Load("Prefab/danmuBallReflect");
        rbBoss = gameobjBoss.GetComponent<Rigidbody>();
        BossRayShot = GameObject.Find("ASRayShot").GetComponent<AudioSource>();
        BossTan01 = GameObject.Find("ASTan01").GetComponent<AudioSource>();
        textStage = GameObject.Find("texStage").GetComponent<texStage>();
        name = "";
        running = false;
    }

    public abstract void Run();

    public abstract void Stop();

}
