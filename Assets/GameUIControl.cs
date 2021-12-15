using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIControl : Singleton<GameUIControl>
{
    private GameObject CanvasPause;
    private Slider TopProcessSlider;
    private Vector3 TopSliderPos;
    public void Start() {
        InitGameUI();
    }

    public void InitGameUI() {
        TopProcessSlider = GameObject.Find("GameProcess").GetComponent<Slider>();
        TopSliderPos = TopProcessSlider.transform.position;
        CanvasPause = GameObject.Find("CanvasPause");
        CanvasPause.SetActive(false);
    }

    public void EnterPause() {
        CanvasPause.SetActive(true);
    }

    public void ExitPause() {
        CanvasPause.SetActive(false);
    }
    public void SetTopSlideVisiable(bool visiable) {
        if (visiable) {
            //Debug.Log("set slider can be see");
            TopProcessSlider.transform.position = TopSliderPos;
        }
        else {
            //Debug.Log("set slider can not be see");
            TopProcessSlider.transform.position = TopSliderPos + new Vector3(0f, 1000f, 0f);
        }
    }
    public void UpdateTopSlide(float aValue) {
        TopProcessSlider.value = aValue;
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
        GameControl.Instance.InitSettings();
    }
}
