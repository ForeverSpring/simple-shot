using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
static class Boundary {
    public static float xMin = -5.85f, xMax = 1.9f, yMin = -4.56f, yMax = 4.56f;
    public static bool InBoundary(Vector3 v) {
        if (v.x > xMin && v.x < xMax && v.y > yMin && v.y < yMax) {
            return true;
        }
        return false;
    }
}
public class GameControl : Singleton<GameControl> {
    public GameObject FukaManager;
    public bool gameover,gamewin;
    private bool Pause;
    private bool isRuningFuka;
    private bool BreakFuka;
    private bool isCheckingGameState;
    private float PauseRate = 1.5f;
    private float nextPause = 0f;
    private int posFuka;
    public List<Fuka> arrFuka = new List<Fuka>();//协程名
    

    //数据初始化
    public void InitSettings() {
        Time.timeScale = 1;
        AudioControl.Instance.GetAudioSource();
        AudioControl.Instance.StopBGM();
        AudioControl.Instance.PlayBGM();
        posFuka = -1;
        BreakFuka = false;
        gameover = false;
        isRuningFuka = false;
        Pause = false;
        isCheckingGameState = true;
        arrFuka.Add(FukaManager.GetComponent<Stage1>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_1>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_2>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_3>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1Finish>());
    }

    private void Start() {
        InitSettings();
    }

    void Update() {
        if (isCheckingGameState) {
            CheckGameState();
        }
        //TODO: Refacor system of game process  EXAMPLE: updateProcess()
        if (Input.GetKey(KeyCode.Escape) && !gameover && Time.unscaledTime >= nextPause) {
            nextPause = Time.unscaledTime + PauseRate;
            if (Pause) {
                ReturnGame();
            }
            else {
                PauseGame();
            }
        }
        //协程顺序依次执行
        if (!isRuningFuka) {
            if (posFuka < arrFuka.Capacity) {
                posFuka++;
                arrFuka[posFuka].Run();
                Debug.Log(posFuka + ":" + arrFuka[posFuka].fukaName);
            }
        }
        //符卡被击破时中断符卡协程
        if (BreakFuka) {
            arrFuka[posFuka].Stop();
            isRuningFuka = false;
        }
    }
    private void CheckGameState() {
        if (gameover) {
            GameOver();
            isCheckingGameState = false;
        }
        if (gamewin) {
            GameWin();
            isCheckingGameState = false;
        }
    }
    public Fuka GetRunningFuka() {
        if (posFuka == -1)
            return null;
        return arrFuka[posFuka];
    }
    public void SetGameWin(bool aGameWin) {
        gamewin = aGameWin;
    }
    public void PauseGame() {
        Pause = true;
        Time.timeScale = 0;
        GameUIControl.Instance.EnterPause();
        AudioControl.Instance.PlayPause();
        AudioControl.Instance.PauseBGM();
    }

    public void ReturnGame() {
        Pause = false;
        Time.timeScale = 1;
        GameUIControl.Instance.ExitPause();
        AudioControl.Instance.PlayPause();
        AudioControl.Instance.PlayBGM();
    }
    public void RetryGame() {
        //plya retry animation here
        SceneLoader.Instance.LoadGamePlayScene();
        GameControl.Instance.InitSettings();
    }
    public void ReturnSelect() {
        SceneLoader.Instance.LoadMainMenuScene();
    }
    void GameOver() {
        Time.timeScale = 0;
        GameUIControl.Instance.EnterLost();
        AudioControl.Instance.StopBGM();
    }
    void GameWin() {
        Time.timeScale = 0;
        GameUIControl.Instance.EnterWin();
    }

    //协程顺序执行互斥锁
    public void WaitFuka() {
        isRuningFuka = true;
    }
    public void SignalFuka() {
        isRuningFuka = false;
    }
    
}
