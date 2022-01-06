using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {
    [SerializeField] public int numPlayer, numBomb, numScore;
    private void Start() {
        InitSettings();
    }
    public void InitSettings() {
        numPlayer = GameSettings.Instance.playerStartLifeNum;
        numBomb = GameSettings.Instance.playerStartBombNum;
        numScore = 0;
    }
    public bool hasBomb() {
        return numBomb > 0;
    }
    public void useBomb() {
        if (numBomb > 0) {
            numBomb--;
        }
    }
    public void addScore(int score) {
        numScore += score;
    }
    public void PlayerBeShot() {
        if (numPlayer > 0) {
            numPlayer--;
            numBomb = GameSettings.Instance.playerStartBombNum;
        }
        else if (numPlayer == 0) {
            GameControl.Instance.gameover = true;
        }
    }
}
