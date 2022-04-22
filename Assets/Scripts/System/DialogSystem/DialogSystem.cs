using DG.Tweening;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogSystem : Singleton<DialogSystem> {
    PlayerInput input;
    public GameObject CanvasDialog;
    public Text texDialog;
    public Image playerImage, BossImage, DialogBG;
    public bool isDialoging;
    private float TweenPlayTime = 1f;
    Dialog curDialog;
    [SerializeField] public List<Dialog> dialogs;
    private void Start() {
        input = GameObject.Find("Player").GetComponent<PlayerInput>();
        isDialoging = false;
    }
    public void StartDialog(string DialogName) {
        isDialoging = true;
        StartCoroutine(StartADialogCoroutine(DialogName));
    }
    IEnumerator StartADialogCoroutine(string DialogName) {
        yield return new WaitForSeconds(1f);
        if (!ReadDialogFile(DialogName))
            StopCoroutine(StartADialogCoroutine(DialogName));
        CanvasDialog.SetActive(true);
        texDialog.gameObject.SetActive(true);
        DialogBG.gameObject.SetActive(true);
        playerImage.gameObject.SetActive(true);
        BossImage.gameObject.SetActive(true);
        input.DisableFireAndBomb();
        EnvironmentObjectsManager.Instance.ClearBullet();
        EnvironmentObjectsManager.Instance.BossObject.GetComponent<CircleCollider2D>().enabled = false;
        foreach (DialogSentence sentence in curDialog.sentences) {
            Tween loggerTween;
            bool playerOver = false;
            if (sentence.logger == "player") {
                texDialog.text = "";
                loggerTween = texDialog.DOText(sentence.content, 2f).OnComplete(() => { playerOver = true; });
                Tween tweenPlayerColor = playerImage.DOColor(new Color(1, 1, 1, 1), TweenPlayTime);
                Tween tweenPlayerMove = playerImage.gameObject.transform.DOScale(new Vector3(1, 1, 1), TweenPlayTime);
                Tween tweenBossColor = BossImage.DOColor(new Color(1, 1, 1, 0.5f), TweenPlayTime);
                Tween tweenBossMove = BossImage.gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), TweenPlayTime);
                loggerTween.OnUpdate(() => {
                    if (input.SkipDialog) {
                        loggerTween.Complete();
                        tweenBossColor.Complete();
                        tweenBossMove.Complete();
                        tweenPlayerColor.Complete();
                    }
                });
            }
            else if (sentence.logger == "boss") {
                texDialog.text = "";
                loggerTween = texDialog.DOText(sentence.content, 2f).OnComplete(() => { playerOver = true; });
                Tween tweenBossColor = BossImage.DOColor(new Color(1, 1, 1, 1), TweenPlayTime);
                Tween tweenBossMove = BossImage.gameObject.transform.DOScale(new Vector3(1, 1, 1), TweenPlayTime);
                Tween tweenPlayerColor = playerImage.DOColor(new Color(1, 1, 1, 0.5f), TweenPlayTime);
                Tween tweenPlayerMove = playerImage.gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), TweenPlayTime);
                loggerTween.OnUpdate(() => {
                    if (input.SkipDialog) {
                        loggerTween.Complete();
                        tweenBossColor.Complete();
                        tweenBossMove.Complete();
                        tweenPlayerColor.Complete();
                    }
                });
            }
            yield return new WaitUntil(() => playerOver && (input.Dialog || input.SkipDialog));
        }
        EnvironmentObjectsManager.Instance.BossObject.GetComponent<CircleCollider2D>().enabled = true;
        input.EnableFireAndBomb();
        texDialog.gameObject.SetActive(false);
        DialogBG.gameObject.SetActive(false);
        playerImage.gameObject.SetActive(false);
        BossImage.gameObject.SetActive(false);
        CanvasDialog.SetActive(false);
        isDialoging = false;
    }

    bool ReadDialogFile(string fileName) {
        foreach(Dialog dialog in dialogs) {
            if (dialog.name == fileName) {
                curDialog = dialog;
#if UNITY_EDITOR
                Debug.Log(fileName+" dialog is ready!");
#endif
                return true;
            }
        }
#if UNITY_EDITOR
        Debug.Log(fileName + " dialog is not found!");
#endif
        return false;
    }
}
