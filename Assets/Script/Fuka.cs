using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Fuka : MonoBehaviour
{
    public GameControl mainControl;
    public GameObject gameobjBoss;
    public GameObject gameobjDanmuBall;
    public GameObject gameobjDanmuBallReflect;
    public Rigidbody rbBoss;
    public AudioSource BossRayShot;
    public AudioSource BossTan01;
    public bool running;
    public string name;
    //TODO:符卡类面向对象

    void Awake() {
        mainControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        gameobjBoss = GameObject.Find("Boss");
        gameobjDanmuBall = (GameObject)Resources.Load("Prefab/danmuBall");
        gameobjDanmuBallReflect = (GameObject)Resources.Load("Prefab/danmuBallReflect");
        rbBoss = gameobjBoss.GetComponent<Rigidbody>();
        BossRayShot = GameObject.Find("ASRayShot").GetComponent<AudioSource>();
        BossTan01 = GameObject.Find("ASTan01").GetComponent<AudioSource>();
        name = "";
        running = false;
    }

    private void Start() {
        running = false;
    }

    public abstract void Run();

    public abstract void Stop();

}
