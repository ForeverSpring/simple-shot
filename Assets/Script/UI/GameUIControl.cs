using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIControl : Singleton<GameUIControl>
{
    public Button btnReturnToGame, btnLostFirstSelect, btnWinFirstSelect;
    public GameObject CanvasPause;
    public GameObject CanvasWin;
    public GameObject CanvasLost;
    public GameObject CanvasGame;
    public Slider TopProcessSlider;
    private Vector3 TopSliderPos;
    public TextMeshProUGUI textFps;
    public Text texPlayer, texBomb, texScore, texFukaName;
    public Animator textFukaNameAnimator;
    public List<GameObject> arrLife = new List<GameObject>();
    public List<GameObject> arrBomb = new List<GameObject>();
    public void Start() {
        InitGameUI();
    }
    public void Update() {
        UpdateDataText();
    }
    public void FixedUpdate() {
        UpdateFpsText();
        UpdateDebugText();
    }
    //Initial Game UI
    public void InitGameUI() {
        if (TopProcessSlider == null) 
            TopProcessSlider = GameObject.Find("GameProcess").GetComponent<Slider>();
        //TopSliderPos = TopProcessSlider.transform.position;
        TopSliderPos = new Vector3(-189, 425, 0);
        if (textFps == null)
            textFps = GameObject.Find("texFps").GetComponent<TextMeshProUGUI>();
        CanvasPause.SetActive(false);
        CanvasWin.SetActive(false);
        CanvasLost.SetActive(false);
        InitPlayerLife();
        InitPlayerBomb();
    }
    void InitPlayerLife() {
        Vector3 firstPos = new Vector3(380, 355, 0);
        int between = 35;
        GameObject objLife = (GameObject)Resources.Load("Prefab/UI/life");
        for (int i = 0; i < GameData.Instance.numPlayer; i++) {
            GameObject life = Instantiate(objLife, CanvasGame.transform);
            life.gameObject.name = "life" + i;
            life.transform.localPosition = firstPos;
            firstPos.x += between;
            arrLife.Add(life);
        }
    }
    void InitPlayerBomb() {
        Vector3 firstPos = new Vector3(380, 305, 0);
        int between = 35;
        GameObject objBomb = (GameObject)Resources.Load("Prefab/UI/bomb");
        for (int i = 0; i < GameData.Instance.numBomb; i++) {
            GameObject bomb = Instantiate(objBomb, CanvasGame.transform);
            bomb.gameObject.name = "bomb" + i;
            bomb.transform.localPosition = firstPos;
            firstPos.x += between;
            arrBomb.Add(bomb);
        }
    }
    //Update function
    void UpdateDebugText() {
        DecisionPoint decisionPoint = GameObject.Find("Player").GetComponent<DecisionPoint>();
        string ret = "Debug\n";
        if (decisionPoint.isMuteki())
            ret += "player state: MUTEKI\n";
        else
            ret += "player state:\n";
        if (GameControl.Instance.GetRunningFuka() != null)
            ret += "Fuka:" + GameControl.Instance.GetRunningFuka().fukaName + "\n";
        else
            ret += "Fuka:\n";
        GameObject.Find("TextDebug").GetComponent<Text>().text = ret;
    }
    void UpdateFpsText() {
        textFps.text = string.Format("{0:f1}fps", Fps.Instance.GetFps());
    }
    void UpdateDataText() {
        texPlayer.text = "Player  " + GameData.Instance.numPlayer;
        texBomb.text = "Spell  " + GameData.Instance.numBomb;
        texScore.text = "Score  " + GameData.Instance.numScore;
    }
    public void UpdatePlayerLife() {
        int index_obj = 0;
        foreach(GameObject obj in arrLife) {
            if (index_obj + 1 > GameData.Instance.numPlayer) {
                obj.SetActive(false);
            }
            else {
                obj.SetActive(true);
            }
            index_obj++;
        }
    }
    public void UpdatePlayerBomb() {
        int index_obj = 0;
        foreach (GameObject obj in arrBomb) {
            if (index_obj + 1 > GameData.Instance.numBomb) {
                obj.SetActive(false);
            }
            else {
                obj.SetActive(true);
            }
            index_obj++;
        }
    }
    //UI state
    public void EnterPause() {
        CanvasPause.SetActive(true);
        btnReturnToGame.Select();
    }
    public void ExitPause() {
        CanvasPause.SetActive(false);
    }
    public void EnterWin() {
        CanvasWin.SetActive(true);
        btnWinFirstSelect.Select();
    }
    public void EnterLost() {
        CanvasLost.SetActive(true);
        btnLostFirstSelect.Select();
    }
    //Text FukaName
    public void FukaNameStart(String aFukaName) {
        texFukaName.text = aFukaName;
        textFukaNameAnimator.Play("Start");
    }
    //Slider Top process
    public void SetTopSlideVisiable(bool visiable) {
        if (visiable) {
            TopProcessSlider.transform.localPosition = TopSliderPos;
        }
        else {
            TopProcessSlider.transform.localPosition = TopSliderPos + new Vector3(0f, 1000f, 0f);
        }
    }
    public void SetTopSlide(float aValue) {
        TopProcessSlider.value = aValue;
    }
    //Button Event Trigger
    public void ButtonSwitch() {
        AudioControl.Instance.PlayButtonSwitch();
    }
    public void ButtonOK() {
        AudioControl.Instance.PlayButtonOK();
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
