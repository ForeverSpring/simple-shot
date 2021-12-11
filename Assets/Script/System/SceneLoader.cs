using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    void Load(string scene) {
        SceneManager.LoadScene(scene,LoadSceneMode.Single);
    }

    public void LoadGamePlayScene() {
        Load("GamePlayScene");
    }

    public void LoadMainMenuScene() {
        Load("MenuScene");
    }

    public void ExitGame() {
        UnityEditor.EditorApplication.isPlaying = false;//buildʱɾ��
        Application.Quit();
    }
}
