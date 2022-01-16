using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIcontroller : MonoBehaviour
{
    public Button btnAnyButton, btnGameStart, btnBGMVolume;
    public GameObject mMainMenu,mOption,mStart;
    private void Update() {
        CheckInput();
    }
    private void InitUI() {
        mMainMenu = GameObject.Find("MainMenu");
        mOption = GameObject.Find("CanvasOption");
        mStart = GameObject.Find("CanvasStart");
    }
    void Start() {
        //InitUI();
        EnterStart();
    }
    void CheckInput() {
        if (mOption.active) {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonBGMVolume")) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    GameObject.Find("SliderBGMVolume").GetComponent<Slider>().value -= (float)0.05;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)){
                    GameObject.Find("SliderBGMVolume").GetComponent<Slider>().value += (float)0.05;
                }
            }
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSEVolume")) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    //TODO:保持数值在0到1之间mathf
                    GameObject.Find("SliderSEVolume").GetComponent<Slider>().value -= (float)0.05;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    GameObject.Find("SliderSEVolume").GetComponent<Slider>().value += (float)0.05;
                }
            }
        }
        if (mStart.active) {
            if (Input.anyKeyDown) {
                BtnStartContinueClicked();
            }
        }
    }
    public void EnterStart() {
        mStart.SetActive(true);
        mMainMenu.SetActive(false);
        mOption.SetActive(false);
    }

    public void ExitStart() {
        mStart.SetActive(false);
        EnterMainMenu();
    }

    public void EnterOption() {
        mMainMenu.gameObject.SetActive(false);
        mOption.gameObject.SetActive(true);
        btnBGMVolume.Select();
    }

    public void EnterMainMenu() {
        mMainMenu.gameObject.SetActive(true);
        mOption.gameObject.SetActive(false);
        btnGameStart.Select();
    }
    //Button of StartMenu
    public void BtnStartContinueClicked() {
        ExitStart();
    }
    //Button of MainMenu
    public void BtnMainStartClicked() {
        SceneLoader.Instance.LoadGamePlayScene();
    }
    public void BtnMainOptionClicked() {
        EnterOption();
    }
    public void BtnMainExitClicked() {
        SceneLoader.Instance.ExitGame();
    }
    //Button of OptionMenu
    public void BtnOptionBGMVolumeClicked() {
        AudioControl.Instance.UpdateBGMVolume();
        AudioControl.Instance.UpdateSettings();
    }
    public void BtnOptionSEVolumeClicked() {
        AudioControl.Instance.UpdateSEVolume();
        AudioControl.Instance.UpdateSettings();
    }
    public void BtnOptionDefaultClicked() {
        AudioControl.Instance.InitSettings();
        AudioControl.Instance.UpdateSettings();
    }
    public void BtnOptionQuitClicked() {
        AudioControl.Instance.UpdateSettings();
        EnterMainMenu();
    }
}
