using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIControl : Singleton<GameUIControl>
{
    public GameObject CanvasPause;
    public void Start() {
        InitGameUI();
    }

    public void InitGameUI() {
        CanvasPause = GameObject.Find("CanvasPause");
        CanvasPause.SetActive(false);
    }

    public void EnterPause() {
        CanvasPause.SetActive(true);
    }

    public void ExitPause() {
        CanvasPause.SetActive(false);
    }

    //Button Pause
    public void BtnPauseReturnGameClicked() {
        ExitPause();
        GameControl.Instance.ReturnGame();
    }

    public void BtnPauseReturnSelectClicked() {
        SceneLoader.Instance.LoadMainMenuScene();
    }

    public void BtnPauseRetryClicked() {
        //plya retry animation here
        SceneLoader.Instance.LoadGamePlayScene();
        GameControl.Instance.InitialSet();
    }
}
