using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmuFactory : Singleton<DanmuFactory>
{
    [Header("Ball弹幕")]
    public GameObject redBallDanmu;
    public GameObject blueBallDanmu;
    public GameObject greenBallDanmu;
    [Header("knife弹幕")]
    public GameObject redKnifeDanmu;
    [Header("反弹Ball弹幕")]
    public GameObject blueBallReflectDanmu;
    [Header("大玉弹幕")]
    public GameObject blueBigBallDanmu;
    [Header("点弹幕")]
    public GameObject whiteSmallBallDanmu;
    [Header("STGFire弹幕")]
    public GameObject fireball_red_tail_big;
    [Header("Diamond弹幕")]
    public GameObject redDiamondDanmu;
    public GameObject blueDiamondDanmu;
    public GameObject greenDiamondDanmu;
    public GameObject yellowDiamondDanmu;
    public GameObject whiteDiamondDanmu;
    [Header("Cube弹幕")]
    public GameObject redCubeDanmu;
    public GameObject blueCubeDanmu;
    public GameObject greenCubeDanmu;
    public GameObject yellowCubeDanmu;
    public GameObject blackCubeDanmu;
    private void Start() {
        Debug.Log("Danmu GameObject Ready.");
    }
    public GameObject GetRedKnifeDanmu() {
        return PoolManager.Release(redKnifeDanmu);
    }
    public GameObject GetBlueBallDanmu() {
        return PoolManager.Release(blueBallDanmu);
    }
    public GameObject GetGreenBallDanmu() {
        return PoolManager.Release(greenBallDanmu);
    }
    public GameObject GetRedBallDanmu() {
        return PoolManager.Release(redBallDanmu);
    }
    public GameObject GetBlueBallReflectDanmu() {
        return PoolManager.Release(blueBallReflectDanmu);
    }
    public GameObject GetBlueBigBallDanmu() {
        return PoolManager.Release(blueBigBallDanmu);
    }
    public GameObject GetWhiteSmallBallDanmu() {
        return PoolManager.Release(whiteSmallBallDanmu);
    }
    public GameObject Getfireball_red_tail_big() {
        return PoolManager.Release(fireball_red_tail_big);
    }
    public GameObject GetRedDiamondDanmu() {
        return PoolManager.Release(redDiamondDanmu);
    }
    public GameObject GetBlueDiamondDanmu() {
        return PoolManager.Release(blueDiamondDanmu);
    }
    public GameObject GetGreenDiamondDanmu() {
        return PoolManager.Release(greenDiamondDanmu);
    }
    public GameObject GetYellowDiamondDanmu() {
        return PoolManager.Release(yellowDiamondDanmu);
    }
    public GameObject GetWhiteDiamondDanmu() {
        return PoolManager.Release(whiteDiamondDanmu);
    }
    public GameObject GetRedCubeDanmu() {
        return PoolManager.Release(redDiamondDanmu);
    }
    public GameObject GetBlueCubeDanmu() {
        return PoolManager.Release(blueCubeDanmu);
    }
    public GameObject GetGreenCubeDanmu() {
        return PoolManager.Release(greenCubeDanmu);
    }
    public GameObject GetYellowCubeDanmu() {
        return PoolManager.Release(yellowCubeDanmu);
    }
    public GameObject GetBlackCubeDanmu() {
        return PoolManager.Release(blackCubeDanmu);
    }
}
