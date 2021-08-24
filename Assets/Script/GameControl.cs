using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
static class Boundary {
    public static float xMin = -5.85f, xMax = 1.9f, yMin = -4.56f, yMax = 4.56f;
    public static bool IsOut(Vector3 v) {
        if (v.x > xMin && v.x < xMax && v.y > yMin && v.y < yMax) {
            return false;
        }
        return true;
    }
}
public class GameControl : Singleton<GameControl> {
    public GameObject FukaManager;
    public Text texPlayer, texBomb, texScore, texFukaName;
    public AudioControl SeControl;
    public AudioSource BossRayShot;
    public AudioSource BossTan01;
    public int numPlayer, numBomb, numScore;
    private bool gameover;
    private bool Pause;
    private bool isRuningFuka;
    private bool BreakFuka;
    private float PauseRate = 1.5f;
    private float nextPause = 0f;
    private int posFuka;
    List<Fuka> arrFuka = new List<Fuka>();//协程名

    //数据初始化
    void InitialSet() {
        SeControl.PlayBGM();
        posFuka = -1;
        numPlayer = 5; numBomb = 3; numScore = 0;
        BreakFuka = false;
        gameover = false;
        isRuningFuka = false;
        Pause = false;
        UpdataText();
        arrFuka.Add(FukaManager.GetComponent<Stage1>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_1>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_2>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_3>());
    }

    private void Start() {
        InitialSet();
    }

    void Update() {
        if (gameover) {
            GameOver();
        }
        if (Input.GetKey(KeyCode.Escape) && !gameover && Time.unscaledTime >= nextPause) {
            nextPause = Time.unscaledTime + PauseRate;
            if (Pause) {
                Time.timeScale = 1;
                SeControl.PlayPause();
                SeControl.PlayBGM();
                Pause = false;
            }
            else {
                Time.timeScale = 0;
                SeControl.PlayPause();
                SeControl.PauseBGM();
                Pause = true;
            }
        }
        //协程顺序依次执行
        if (!isRuningFuka) {
            if (posFuka < arrFuka.Capacity) {
                posFuka++;
                arrFuka[posFuka].Run();
                Debug.Log(posFuka + ":" + arrFuka[posFuka].name);
            }
        }
        //符卡被击破时中断符卡协程
        if (BreakFuka) {
            arrFuka[posFuka].Stop();
            isRuningFuka = false;
        }
    }

    void GameOver() {
        //Debug.Log("GameOver!");
        Time.timeScale = 0;
        SeControl.StopBGM();
    }

    /// <summary>
    /// 数据操作函数
    /// </summary>
    void UpdataText() {
        texPlayer.text = "Player  " + numPlayer;
        texBomb.text = "Bomb  " + numBomb;
        texScore.text = "Score  " + numScore;
    }
    public bool hasBomb() {
        return numBomb > 0;
    }
    public void useBomb() {
        if (numBomb > 0) {
            numBomb--;
        }
        UpdataText();
    }
    public void addScore(int score) {
        numScore += score;
        UpdataText();
    }
    public void PlayerBeShot() {
        if (numPlayer > 0) {
            numPlayer--;
            numBomb = 3;
        }
        else if (numPlayer == 0) {
            gameover = true;
        }
        UpdataText();
    }
    //协程顺序执行互斥锁
    public void WaitFuka() {
        isRuningFuka = true;
    }
    public void SignalFuka() {
        isRuningFuka = false;
    }
    
}
