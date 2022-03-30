using UnityEngine;
static class Boundary {
    //TODO:通过场景中的初始对象设置位置，便于场景整体移动
    public static float xMin = -5.85f, xMax = 1.9f, yMin = -4.56f, yMax = 4.56f;
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
        if (GameObject.Find("[DOTween]")) {
            //TODO:修复场景重新加载时动画错误
#if UNITY_EDITOR
            Debug.Log("删除DOTween对象");
#endif
            Destroy(GameObject.Find("[DOTween]"));
        }
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
        if (numPlayer > 0) {
            setPlayer(numPlayer - 1);
            setBomb(GameSettings.Instance.playerStartBombNum);
        }
        else if (numPlayer == 0) {
            GameControl.Instance.gameover = true;
        }
        GameUIControl.Instance.UpdatePlayerLife();
        GameUIControl.Instance.UpdatePlayerBomb();
    }
}
