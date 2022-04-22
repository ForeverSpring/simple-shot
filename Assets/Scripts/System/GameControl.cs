using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GameControl : Singleton<GameControl> {
    PlayerInput input;
    public GameObject FukaManager;
    public bool gameover, gamewin;
    private bool Pause;
    private bool isRuningFuka;
    private bool BreakFuka;
    private bool isCheckingGameState;
    private float PauseRate = 1.5f;
    private float nextPause = 0f;
    private int posFuka;
    public List<Fuka> arrFuka = new List<Fuka>();//协程名
    public void InitSettings() {
        input = GameObject.Find("Player").GetComponent<PlayerInput>();
        Time.timeScale = 1;
        AudioControl.Instance.StopBGM();
        posFuka = -1;
        BreakFuka = false;
        gameover = false;
        isRuningFuka = false;
        Pause = false;
        isCheckingGameState = true;
        arrFuka.Add(FukaManager.GetComponent<Stage1>());
        arrFuka.Add(FukaManager.GetComponent<Stage1Midway>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_1>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_2>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1_3>());
        arrFuka.Add(FukaManager.GetComponent<Fuka1Finish>());
    }

    private void Start() {
        InitSettings();
    }

    void Update() {
        if (isCheckingGameState)
            CheckGameState();
        if ((input.Pause || input.Exit) && !(gameover || gamewin) && Time.unscaledTime >= nextPause) {
            nextPause = Time.unscaledTime + PauseRate;
            if (Pause)
                ReturnGame();
            else
                PauseGame();
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
        if (Keyboard.current.f1Key.wasPressedThisFrame) {
            GameUIControl.Instance.SwitchDebugText();
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
        input.EnableGameMenuInputs();
        input.DisableGameplayInputs();
        Pause = true;
        Time.timeScale = 0;
        GameUIControl.Instance.EnterPause();
        AudioControl.Instance.PlayPause();
        AudioControl.Instance.PauseBGM();
    }

    public void ReturnGame() {
        input.DisableGameMenuInputs();
        input.EnableGameplayInputs();
        Pause = false;
        Time.timeScale = 1;
        GameUIControl.Instance.ExitPause();
        AudioControl.Instance.PlayPause();
        AudioControl.Instance.PlayBGM();
    }
    public void RetryGame() {
        AudioControl.Instance.StopBGM();
        SceneLoader.Instance.LoadGamePlayScene();
        GameControl.Instance.InitSettings();
    }
    public void ReturnSelect() {
        AudioControl.Instance.StopBGM();
        SceneLoader.Instance.LoadMainMenuScene();
    }
    void GameOver() {
        Time.timeScale = 0;
        input.EnableGameMenuInputs();
        input.DisableGameplayInputs();
        GameUIControl.Instance.EnterLost();
        AudioControl.Instance.StopBGM();
    }
    void GameWin() {
        Time.timeScale = 0;
        input.EnableGameMenuInputs();
        input.DisableGameplayInputs();
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
