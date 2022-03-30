using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIcontroller : MonoBehaviour
{
    public Button btnAnyButton, btnGameStart, btnBGMVolume;
    public GameObject mMainMenu,mOption,mStart;
    public TextMeshProUGUI textBGMVolume, textSEVolume;
    private void Update() {
        CheckInput();
    }
    private void InitUI() {
        if (mMainMenu == null)
            mMainMenu = GameObject.Find("MainMenu");
        if (mOption == null)
            mOption = GameObject.Find("CanvasOption");
        if (mStart == null)
            mStart = GameObject.Find("CanvasStart");
    }
    void Start() {
        //InitUI();
        EnterStart();
    }
    void UpdateVolumeText() {
        textBGMVolume.text = string.Format("{0}%", Mathf.Round(AudioControl.Instance.mBGMVolume * 100));
        textSEVolume.text = string.Format("{0}%", Mathf.Round(AudioControl.Instance.mSEVolume * 100));
    }
    void CheckInput() {
        if (mOption.active) {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonBGMVolume")) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    AudioControl.Instance.mBGMVolume = Mathf.Clamp(AudioControl.Instance.mBGMVolume - (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)){
                    AudioControl.Instance.mBGMVolume = Mathf.Clamp(AudioControl.Instance.mBGMVolume + (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
                }
            }
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSEVolume")) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    AudioControl.Instance.mSEVolume = Mathf.Clamp(AudioControl.Instance.mSEVolume - (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    AudioControl.Instance.mSEVolume = Mathf.Clamp(AudioControl.Instance.mSEVolume + (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
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
        UpdateVolumeText();
    }

    public void EnterMainMenu() {
        mMainMenu.gameObject.SetActive(true);
        mOption.gameObject.SetActive(false);
        btnGameStart.Select();
    }
    //Button Event Trigger
    public void ButtonSwitch() {
        AudioControl.Instance.PlayButtonSwitch();
    }
    public void ButtonOK() {
        AudioControl.Instance.PlayButtonOK();
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
        //TODO:等待按钮音效播放完毕后退出游戏
        SceneLoader.Instance.ExitGame();
    }
    //Button of OptionMenu
    public void BtnOptionBGMVolumeClicked() {
        AudioControl.Instance.UpdateSettings();
    }
    public void BtnOptionSEVolumeClicked() {
        AudioControl.Instance.UpdateSettings();
    }
    public void BtnOptionDefaultClicked() {
        AudioControl.Instance.InitSettings();
        AudioControl.Instance.UpdateSettings();
        UpdateVolumeText();
    }
    public void BtnOptionQuitClicked() {
        AudioControl.Instance.UpdateSettings();
        EnterMainMenu();
    }
}
