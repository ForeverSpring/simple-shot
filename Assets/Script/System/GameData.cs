using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {
    [SerializeField] public int numPlayer, numBomb, numScore;
    private void Start() {
        Debug.Log("GameData reading.");
        InitSettings();
        Debug.Log("GameData read over.");
    }
    public void InitSettings() {
        numPlayer = GameSettings.Instance.playerStartLifeNum;
        numBomb = GameSettings.Instance.playerStartBombNum;
        numScore = 0;
        GameUIControl.Instance.UpdatePlayerLife();
        GameUIControl.Instance.UpdatePlayerBomb();
    }
    public void setBomb(int aNumBomb) {
        numBomb = aNumBomb;
        GameUIControl.Instance.UpdatePlayerBomb();
    }
    public bool hasBomb() {
        return numBomb > 0;
    }
    public void useBomb() {
        if (numBomb > 0) {
            setBomb(numBomb - 1);
        }
    }
    public void setPlayer(int aNumPlayer) {
        numPlayer = aNumPlayer;
        GameUIControl.Instance.UpdatePlayerLife();
    }
    public void addScore(int score) {
        numScore += score;
    }
    public void PlayerBeShot() {
        GameUIControl.Instance.UpdatePlayerLife();
        if (numPlayer > 0) {
            setPlayer(numPlayer - 1);
            setBomb(GameSettings.Instance.playerStartBombNum);
        }
        else if (numPlayer == 0) {
            GameControl.Instance.gameover = true;
        }
    }
}
