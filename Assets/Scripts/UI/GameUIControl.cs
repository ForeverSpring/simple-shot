using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameUIControl : Singleton<GameUIControl>
{
    public Button btnReturnToGame, btnLostFirstSelect, btnWinFirstSelect;
    public GameObject CanvasPause;
    public GameObject CanvasWin;
    public GameObject CanvasLost;
    public GameObject CanvasGame;
    [Header("GameData")]
    public Slider TopProcessSlider;
    private Vector3 TopSliderPos;
    public TextMeshProUGUI texScoreValue; 
    public TextMeshProUGUI texPowerValue;
    public TextMeshProUGUI textFps, texFukaName, texSpellGet, texSpellGetValue;
    public TextMeshProUGUI textFukaUseTime;
    public GameObject lifeItem;
    public HorizontalLayoutGroup playerLifeGroup;
    public HorizontalLayoutGroup playerSpellGroup;
    public Image bossFukaImage;
    public Image playerFukaImage;
    public texStage textStage;
    [Header("Score")]
    public GameObject CanvasWriteScore;
    public TMP_InputField PlayerNameInputArea;
    public TextMeshProUGUI textScoreToWrite;
    public Button btnSubmitScore;
    [Header("DebugCanvas")]
    public GameObject CanvasDebug;
    public Text texDebug;
    public Fps fps;

    public void Start() {
        Debug.Log("GameUI loading...");
        StartCoroutine(InitGameUI());
        Debug.Log("GameUI ready.");
    }
    public void FixedUpdate() {
        UpdateFpsText();
        UpdateDebugText();
    }
    //Initial Game UI
    IEnumerator InitGameUI() {
        //lifeItem = (GameObject)Resources.Load("Prefab/UI/LifeUI");
        if (TopProcessSlider == null) 
            TopProcessSlider = GameObject.Find("GameProcess").GetComponent<Slider>();
        //TopSliderPos = TopProcessSlider.transform.position;
        TopSliderPos = new Vector3(-189, 425, 0);
        if (textFps == null)
            textFps = GameObject.Find("texFps").GetComponent<TextMeshProUGUI>();
        CanvasPause.SetActive(false);
        CanvasWin.SetActive(false);
        CanvasLost.SetActive(false);
        yield return new WaitUntil(() => GameData.Instance != null);
        UpdatePlayerScore();
        UpdatePlayerBomb();
        UpdatePlayerLife();
        UpdatePlayerPower();
    }
    public void PrintStageText(string stageName) {
        textStage.setText(stageName);
        textStage.printStage();
    }
    //Update function
    public void ButtonSwitch() { AudioControl.Instance.PlayButtonSwitch(); }
    public void ButtonOK() { AudioControl.Instance.PlayButtonOK(); }
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
        ret += "Fps:" + fps.GetFps() + "\nApp.targetFrameRate:" + Application.targetFrameRate + "\n";
        texDebug.text = ret;
    }
    void UpdateFpsText() {
            textFps.text = string.Format("{0:f1}fps", fps.GetFps());
    }
    public void UpdatePlayerLife() {
        foreach(Transform lifeItem in playerLifeGroup.transform.gameObject.GetComponentInChildren<Transform>()) {
            Destroy(lifeItem.gameObject);
        }
        for(int i = 0; i < GameData.Instance.numPlayer; i++) {
            GameObject newLifeItem = Instantiate(lifeItem, playerLifeGroup.gameObject.transform);
        }
    }
    public void UpdatePlayerBomb() {
        foreach (Transform lifeItem in playerSpellGroup.transform.gameObject.GetComponentInChildren<Transform>()) {
            Destroy(lifeItem.gameObject);
        }
        for (int i = 0; i < GameData.Instance.numBomb; i++) {
            GameObject newLifeItem = Instantiate(lifeItem, playerSpellGroup.gameObject.transform);
        }
    }
    public void UpdatePlayerScore() {
        int oldScore = int.Parse(texScoreValue.text), newScore = GameData.Instance.numScore;
        DOTween.To(() => oldScore, x => oldScore = x, newScore, 1).OnUpdate(()=> {
            texScoreValue.text = string.Format("{0:000000000}", oldScore);
        });
    }
    public void UpdatePlayerPower() {
        float power, newPower = GameData.Instance.numPower;
        if (texPowerValue.text == "MAX")
            power = 5f;
        else
            power = float.Parse(texPowerValue.text);
        DOTween.To(() => power, x => power = x, newPower, 1).OnUpdate(() => {
            texPowerValue.text = string.Format("{0:F2}", power);
            if (power == 5f)
                texPowerValue.text = "MAX";
        });
        
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
    public void EnterWriteScore() {
        CanvasLost.SetActive(false);
        CanvasWin.SetActive(false);
        CanvasWriteScore.SetActive(true);
        textScoreToWrite.text = texScoreValue.text;
        PlayerNameInputArea.text = "";
        PlayerNameInputArea.Select();
    }
    public void BossFukaImageShow() {
        bossFukaImage.gameObject.SetActive(true);
        bossFukaImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
        bossFukaImage.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f), 3f);
        bossFukaImage.DOColor(new Color(1, 1, 1, 0.5f), 2f).OnComplete(()=> {
            bossFukaImage.DOColor(new Color(1, 1, 1, 0), 2f).OnComplete(() => {
                bossFukaImage.gameObject.SetActive(false);
            });
        });
    }
    public void PlayerFukaImageShow() {
        playerFukaImage.gameObject.SetActive(true);
        playerFukaImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
        playerFukaImage.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f), 3f);
        playerFukaImage.DOColor(new Color(1, 1, 1, 0.5f), 2f).OnComplete(() => {
            playerFukaImage.DOColor(new Color(1, 1, 1, 0), 2f).OnComplete(() => {
                playerFukaImage.gameObject.SetActive(false);
            });
        });
    }
    //Text FukaName
    public void FukaNameStart(String aFukaName) {
        texFukaName.gameObject.SetActive(true);
        texFukaName.text = aFukaName;
        texFukaName.gameObject.transform.localPosition = new Vector3(-47.5f, -350f);
        Invoke(nameof(FukaNameReset), 1f);
    }
    void FukaNameReset() {
        texFukaName.gameObject.transform.DOLocalMove(new Vector3(-47.5f, 366.3f), 3f);
    }
    //Text Fuka Get
    public void GetSpellCardBonus(int spellScore) {
        texSpellGet.gameObject.SetActive(true);
        texSpellGetValue.gameObject.SetActive(true);
        texSpellGetValue.text = spellScore.ToString();
        Invoke("GetSpellCardBonusReset", 3f);
    }
    void GetSpellCardBonusReset() {
        texSpellGetValue.text = "";
        texSpellGet.gameObject.SetActive(false);
        texSpellGetValue.gameObject.SetActive(false);
    }
    public void SetFukaUseTime(float time) {
        textFukaUseTime.text = "用时 " + ((int)time).ToString() + "s";
        textFukaUseTime.gameObject.SetActive(true);
        textFukaUseTime.gameObject.transform.DOLocalMove(new Vector3(-200, 300, 0), 1f);
        Invoke(nameof(FukaUseTimeReset), 8f);
    }
    void FukaUseTimeReset() {
        textFukaUseTime.gameObject.transform.DOLocalMove(new Vector3(-200, 600, 0), 1f).OnComplete(()=> { 
            textFukaUseTime.text = "";
            textFukaUseTime.gameObject.SetActive(false);
        });
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
    //Button CanvasLost
    public void BtnLostReturnSelectClicked() {
        EnterWriteScore();
    }
    public void BtnLostRetryClicked() {
        GameControl.Instance.RetryGame();
    }
    //Button CanvasWin
    public void BtnWinContinueClicked() {
        //play win animation
        EnterWriteScore();
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
    //Button CanvasScore
    public void InputPlayerNameEndEdit() {
        AudioControl.Instance.PlayButtonOK();
        btnSubmitScore.Select();
    }
    public void BtnSubmitScore() {
        if (PlayerNameInputArea.text != "") {
            int score = int.Parse(textScoreToWrite.text);
            string name = PlayerNameInputArea.text;
#if UNITY_EDITOR
            Debug.Log("提交得分" + name + " " + score);
#endif
            ScoreManager.Instance.SaveNewPlayerScoreData(score, name);
            GameControl.Instance.ReturnSelect();
        }
        else {
            AudioControl.Instance.PlayButtonInvalid();
        }
    }
    //Other
    public void SwitchDebugText() {
        if(CanvasDebug.activeSelf)
            CanvasDebug.SetActive(false);
        else
            CanvasDebug.SetActive(true);
    }
}
