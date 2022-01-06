using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIcontroller : MonoBehaviour
{
    public Button btnStart;
    public GameObject mMainMenu,mOption,mStart;

    private void InitUI() {
        mMainMenu = GameObject.Find("MainMenu");
        mOption = GameObject.Find("CanvasOption");
        mStart = GameObject.Find("CanvasStart");
    }
    void Start() {
        //InitUI();
        EnterStart();
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
    }

    public void EnterMainMenu() {
        btnStart.Select();
        mMainMenu.gameObject.SetActive(true);
        mOption.gameObject.SetActive(false);
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
