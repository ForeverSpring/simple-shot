using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class Boundary {
    //TODO:通过场景中的初始对象设置位置，便于场景整体移动
    public static float xMin = -5.85f, xMax = 1.9f, yMin = -4.56f, yMax = 4.56f;
    public static float height = xMax - xMin;
    public static float weight = yMax - yMin;
    public static Vector3 ViewPortMid = new Vector3((xMin + xMax) / 2f, (yMin + yMax) / 2f, 0f);
    public static Vector3 ViewPortLeftTop = new Vector3(xMin, yMax, 0f);
    public static Vector3 ViewPortRightTop = new Vector3(xMax, yMax, 0f);
    public static Vector3 ViewPortLeftBottom = new Vector3(xMin, yMin, 0f);
    public static Vector3 ViewPortRightBottom = new Vector3(xMax, yMin, 0f);
    public static bool InBoundary(Vector3 v) {
        if (v.x > xMin && v.x < xMax && v.y > yMin && v.y < yMax) {
            return true;
        }
        return false;
    }
}
public class GameData : Singleton<GameData> {
    /// <summary>
    /// 分数系统：
    /// 每次击中敌机+1，击破敌机+敌机分数
    /// 击破符卡+stage*1000+power*100
    /// 道具分：
    /// P点：+100，
    /// </summary>
    [SerializeField] public int numPlayer, numBomb, numScore;
    [SerializeField] public float numPower;
    [SerializeField] public int playerFireLevel => GetFireLevel();
        
    private void Start() {
        Debug.Log("GameData loading...");
        StartCoroutine(InitSettings());
        Debug.Log("GameData ready.");
    }
    IEnumerator InitSettings() {
        numPlayer = GameSettings.Instance.settings.playerStartLifeNum;
        numBomb = GameSettings.Instance.settings.playerStartBombNum;
        numScore = 0;
        numPower = 0f;
        yield return new WaitUntil(() => GameUIControl.Instance != null);
        GameUIControl.Instance.UpdatePlayerLife();
        GameUIControl.Instance.UpdatePlayerBomb();
//        if (GameObject.Find("[DOTween]")) {
//#if UNITY_EDITOR
//            Debug.Log("删除DOTween对象");
//#endif
//            Destroy(GameObject.Find("[DOTween]"));
//        }
    }
    protected void setSpell(int aNumBomb) {
        numBomb = aNumBomb;
        GameUIControl.Instance.UpdatePlayerBomb();
    }
    public bool hasSpell() {
        return numBomb > 0;
    }
    public void useSpell() {
        if (numBomb > 0) {
            setSpell(numBomb - 1);
        }
    }
    public void addSpell() {
        setSpell(numBomb + 1);
        AudioControl.Instance.PlayGetSpell();
    }
    public void setPlayer(int aNumPlayer) {
        numPlayer = aNumPlayer;
        GameUIControl.Instance.UpdatePlayerLife();
    }
    public void addPlayer() {
        setPlayer(numPlayer + 1);
        AudioControl.Instance.PlayLifeUp1();
    }
    public void addScore(int score) {
        numScore += score;
        GameUIControl.Instance.UpdatePlayerScore();
    }
    public void addPower(float power) {
        float oldPower = numPower;
        numPower += power;
        if (numPower > 5f) {
            numPower = 5f;
        }
        if (oldPower < 0.5 && (numPower >= 0.5 && numPower < 2.5))
            AudioControl.Instance.PlayPowerUp1();
        if ((oldPower > 0.5 && oldPower < 2.5) && (numPower >= 2.5 && numPower < 5))
            AudioControl.Instance.PlayPowerUp2();
        GameUIControl.Instance.UpdatePlayerPower();
    }
    public int GetFireLevel() {
        if (numPower < 0.5)
            return 0;
        if (numPower < 2.5)
            return 1;
        else if (numPower < 5)
            return 2;
        else
            return 3;
    }
    public void PlayerBeShot() {
        if (numPlayer > 0) {
            setPlayer(numPlayer - 1);
            setSpell(GameSettings.Instance.settings.playerStartBombNum);
        }
        else if (numPlayer == 0) {
            GameControl.Instance.gameover = true;
        }
        numPower = 0;
        GameUIControl.Instance.UpdatePlayerPower();
        GameUIControl.Instance.UpdatePlayerLife();
        GameUIControl.Instance.UpdatePlayerBomb();
    }
}
