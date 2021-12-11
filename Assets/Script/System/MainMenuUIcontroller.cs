using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIcontroller : MonoBehaviour
{
    public Button btnStart;
    public GameObject mMainMenu,mOption;

    private void InitUI() {
        mMainMenu = GameObject.Find("MainMenu");
        mOption = GameObject.Find("CanvasOption");
    }
    void Start()
    {
        InitUI();
        EnterMainMenu();
        btnStart.Select();
    }

    public void EnterOption() {
        mMainMenu.gameObject.SetActive(false);
        mOption.gameObject.SetActive(true);
    }

    public void EnterMainMenu() {
        mMainMenu.gameObject.SetActive(true);
        mOption.gameObject.SetActive(false);
    }
}
