using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static ScoreManager;

public class MainMenuUIcontroller : MonoBehaviour {
    [Header("LOGO")]
    public GameObject LogoObject;
    [Header("各个页面首选按钮")]
    public Button btnGameStart;
    public Button btnBGMVolume;
    public Button btnPlayerDataExit;
    public Button btnManualExit;
    public Button btnPressAnyButton;
    [Header("各个页面对象")]
    public GameObject CanvasMainMenu;
    public GameObject CanvasOption;
    public GameObject CanvasStart;
    public GameObject CanvasManual;
    public GameObject CanvasPlayerData;
    [Header("TMP对象")]
    public TextMeshProUGUI textBGMVolume;
    public TextMeshProUGUI textSEVolume;
    public TextMeshProUGUI texPlayerDataValue;
    [Header("Mask对象")]
    public Image maskImage;
    private void Update() {
        CheckInput();
    }
    private void Awake() {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
    }
    void Start() {
        EnterStart();
    }
    void UpdateVolumeText() {
        textBGMVolume.text = string.Format("{0}%", Mathf.Round(AudioControl.Instance.mBGMVolume * 100));
        textSEVolume.text = string.Format("{0}%", Mathf.Round(AudioControl.Instance.mSEVolume * 100));
    }
    void CheckInput() {
        //TODO:重构该部分
        if (CanvasOption.activeSelf) {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonBGMVolume")) {
                if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
                    AudioControl.Instance.mBGMVolume = Mathf.Clamp(AudioControl.Instance.mBGMVolume - (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
                }
                if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
                    AudioControl.Instance.mBGMVolume = Mathf.Clamp(AudioControl.Instance.mBGMVolume + (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
                }
            }
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSEVolume")) {
                if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
                    AudioControl.Instance.mSEVolume = Mathf.Clamp(AudioControl.Instance.mSEVolume - (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
                }
                if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
                    AudioControl.Instance.mSEVolume = Mathf.Clamp(AudioControl.Instance.mSEVolume + (float)0.05, 0, 1);
                    UpdateVolumeText();
                    AudioControl.Instance.PlayButtonOK();
                }
            }
        }
    }
    public void ButtonSwitch() { AudioControl.Instance.PlayButtonSwitch(); }
    public void ButtonOK() { AudioControl.Instance.PlayButtonOK(); }
    void ReadPlayerScore() {
        texPlayerDataValue.text = "";
        PlayerScoreData playerScoreData = ScoreManager.Instance.LoadPlayerScoreData();
        for (int i = 0; i < 10; i++) {
            texPlayerDataValue.text += (i + 1) + "\t"
                + playerScoreData.list[i].playerName + "\t"
                + string.Format("{0:000000000}", playerScoreData.list[i].score) + "\t"
                + playerScoreData.list[i].datetime + "\n";
        }
    }
    void MaskImageShow() {
        maskImage.DOColor(new Color(0, 0, 0, 1), 0.6f).OnComplete(()=> {
            maskImage.DOColor(new Color(0, 0, 0, 0), 0.6f);
        });
    }
    //Switch Main Canvas
    public void EnterStart() {
        CanvasStart.SetActive(true);
        CanvasMainMenu.SetActive(false);
        CanvasOption.SetActive(false);
        CanvasPlayerData.SetActive(false);
        CanvasManual.SetActive(false);
        btnPressAnyButton.Select();
        LogoObject.transform.localPosition = new Vector3(0, 100, -8640);
        LogoObject.transform.localScale = new Vector3(150, 150, 150);
    }
    public void ExitStart() {
        CanvasStart.SetActive(false);
        EnterMainMenu();
    }
    public void EnterPlayerData() {
        CanvasMainMenu.SetActive(false);
        CanvasPlayerData.SetActive(true);
        ReadPlayerScore();
        btnPlayerDataExit.Select();
    }
    public void EnterManual() {
        CanvasMainMenu.SetActive(false);
        CanvasManual.SetActive(true);
        btnManualExit.Select();
    }
    public void EnterOption() {
        CanvasMainMenu.SetActive(false);
        CanvasOption.SetActive(true);
        btnBGMVolume.Select();
        UpdateVolumeText();
    }
    public void EnterMainMenu() {
        CanvasMainMenu.SetActive(true);
        CanvasPlayerData.SetActive(false);
        CanvasOption.SetActive(false);
        CanvasManual.SetActive(false);
        btnGameStart.Select();
        LogoObject.transform.localPosition = new Vector3(-270, 230, -8640);
        LogoObject.transform.localScale = new Vector3(96, 96, 96);
        //LogoObject.transform.DOKill();
        //Debug.Log("START Move LOGO");
        //LogoObject.transform.DOLocalMove(new Vector3(-270, 230, -8640), 2f).OnStart(() => {
        //    Debug.Log("Move LOGO");
        //    LogoObject.transform.DOScale(new Vector3(96, 96, 96), 2f).SetAutoKill(false);
        //}).SetAutoKill(false);
    }
    //Button of StartMenu
    public void BtnStartContinueClicked() {
        MaskImageShow();
        Invoke(nameof(ExitStart), 0.5f);
    }
    //Button of MainMenu
    public void BtnMainStartClicked() {
        SceneLoader.Instance.LoadGamePlayScene();
    }
    public void BtnMainPlayerDataClicked() {
        MaskImageShow();
        Invoke(nameof(EnterPlayerData), 0.5f);
    }
    public void BtnMainManualClicked() {
        MaskImageShow();
        Invoke(nameof(EnterManual), 0.5f);
    }
    public void BtnMainOptionClicked() {
        MaskImageShow();
        Invoke(nameof(EnterOption), 0.5f);
    }
    public void BtnMainExitClicked() {
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
        MaskImageShow();
        Invoke(nameof(EnterMainMenu), 0.5f);
    }
    //Button of PlayerData
    public void BtnPlayerDataExitClicked() {
        MaskImageShow();
        Invoke(nameof(EnterMainMenu), 0.5f);
    }
    //Button of Manual
    public void BtnManualExitClicked() {
        MaskImageShow();
        Invoke(nameof(EnterMainMenu), 0.5f);
    }
}
