using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SceneLoader : PersistentSingleton<SceneLoader>
{
    const string GAMEPLAY = "GamePlayScene";
    const string Home = "MenuScene";
    public Image imgLoading;
    public TextMeshProUGUI texLoading, texLoadingValue;
    float FadeTime = 1f;

    public void LoadGamePlayScene() {
        StartCoroutine(CoroutineLoad(GAMEPLAY));
    }

    public void LoadMainMenuScene() {
        StartCoroutine(CoroutineLoad(Home));
    }
    IEnumerator CoroutineLoad(string scene) {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        imgLoading.gameObject.SetActive(true);
        Color color = new Color(0, 0, 0, 0);
        while (color.a < 1f) {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / FadeTime);
            imgLoading.color = color;
            yield return null;
        }
        texLoading.gameObject.SetActive(true);
        texLoadingValue.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        texLoadingValue.text = "0%";
        while (!asyncOperation.isDone) {
            float progress = asyncOperation.progress * 100;
            if (progress >= 0.9f) {
                asyncOperation.allowSceneActivation = true;
                progress = 100f;
            }
            texLoadingValue.text = progress + "%";
            yield return null;
        }
        texLoading.gameObject.SetActive(false);
        texLoadingValue.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        while (color.a > 0f) {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / FadeTime);
            imgLoading.color = color;
            yield return null;
        }
        imgLoading.gameObject.SetActive(false);
    }

    public void ExitGame() {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
}
