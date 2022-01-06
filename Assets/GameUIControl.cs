using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIControl : Singleton<GameUIControl>
{
    public GameObject CanvasPause;
    public GameObject CanvasWin;
    public GameObject CanvasLost;
    private Slider TopProcessSlider;
    private Vector3 TopSliderPos;
    public Text TextFps;
    public Text texPlayer, texBomb, texScore, texFukaName;
    public Animator textFukaNameAnimator;
    public void Start() {
        InitGameUI();
    }
    public void Update() {
        UpdateFpsText();
        UpdateDataText();
        UpdateAnimator();
    }

    public void InitGameUI() {
        TopProcessSlider = GameObject.Find("GameProcess").GetComponent<Slider>();
        TopSliderPos = TopProcessSlider.transform.position;
        TextFps = GameObject.Find("texFps").GetComponent<Text>();
        CanvasPause.SetActive(false);
        CanvasWin.SetActive(false);
        CanvasLost.SetActive(false);
    }
    //Update function
    void UpdateFpsText() {
        TextFps.text = string.Format("FPS:{0:f2}", Fps.Instance.GetFps());
    }
    void UpdateDataText() {
        texPlayer.text = "Player  " + GameData.Instance.numPlayer;
        texBomb.text = "Bomb  " + GameData.Instance.numBomb;
        texScore.text = "Score  " + GameData.Instance.numScore;
    }
    void UpdateAnimator() {
        textFukaNameAnimator.SetBool("Start", false);
    }
    //UI state
    public void EnterPause() {
        CanvasPause.SetActive(true);
    }
    public void ExitPause() {
        CanvasPause.SetActive(false);
    }
    public void EnterWin() {
        CanvasWin.SetActive(true);
    }
    public void EnterLost() {
        CanvasLost.SetActive(true);
    }
    //Text FukaName
    public void FukaNameStart(String aFukaName) {
        texFukaName.text = aFukaName;
        textFukaNameAnimator.SetBool("Start", true);
    }
    //Slider Top process
    public void SetTopSlideVisiable(bool visiable) {
        if (visiable) {
            TopProcessSlider.transform.position = TopSliderPos;
        }
        else {
            TopProcessSlider.transform.position = TopSliderPos + new Vector3(0f, 1000f, 0f);
        }
    }
    public void SetTopSlide(float aValue) {
        TopProcessSlider.value = aValue;
    }
    //Button CanvasLost
    public void BtnLostReturnSelectClicked() {
        GameControl.Instance.ReturnSelect();
    }
    public void BtnLostRetryClicked() {
        GameControl.Instance.RetryGame();
    }
    //Button CanvasWin
    public void BtnWinContinueClicked() {
        //play win animation
        GameControl.Instance.ReturnSelect();
    }
    //Button CanvasPause
    public void BtnPauseReturnGameClicked() {
        ExitPause();
        GameControl.Instance.ReturnGame();
    }
    public void BtnPauseReturnSelectClicked() {
        GameControl.Instance.ReturnSelect();
    }
    public void BtnPauseRetryClicked() {
        GameControl.Instance.RetryGame();
    }
}
