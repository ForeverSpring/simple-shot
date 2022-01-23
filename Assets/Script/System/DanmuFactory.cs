using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmuFactory : Singleton<DanmuFactory>
{
    public GameObject redBallDanmu;
    public GameObject blueBallDanmu;
    public GameObject greenBallDanmu;
    public GameObject redKnifeDanmu;
    public GameObject blueBallReflectDanmu;
    public GameObject blueBigBallDanmu;
    public GameObject whiteSmallBallDanmu;

    private void Start() {
        redBallDanmu = (GameObject)Resources.Load("Prefab/danmu/redBallDanmu");
        blueBallDanmu = (GameObject)Resources.Load("Prefab/danmu/blueBallDanmu");
        greenBallDanmu = (GameObject)Resources.Load("Prefab/danmu/greenBallDanmu");
        redKnifeDanmu = (GameObject)Resources.Load("Prefab/danmu/redKnifeDanmu");
        blueBallReflectDanmu = (GameObject)Resources.Load("Prefab/danmu/blueBallReflectDanmu");
        blueBigBallDanmu = (GameObject)Resources.Load("Prefab/danmu/blueBigBallDanmu");
        whiteSmallBallDanmu = (GameObject)Resources.Load("Prefab/danmu/whiteSmallBallDanmu");
        Debug.Log("Danmu GameObject Ready.");
    }

    public GameObject GetRedKnifeDanmu() {
        GameObject ret = Instantiate(redKnifeDanmu);
        DanmuPool.Instance.mArrDanmu.Add(ret);
        return ret;
    }
    public GameObject GetBlueBallDanmu() {
        GameObject ret = Instantiate(blueBallDanmu);
        DanmuPool.Instance.mArrDanmu.Add(ret);
        return ret;
    }
    public GameObject GetGreenBallDanmu() {
        GameObject ret = Instantiate(greenBallDanmu);
        DanmuPool.Instance.mArrDanmu.Add(ret);
        return ret;
    }
    public GameObject GetRedBallDanmu() {
        GameObject ret = Instantiate(redBallDanmu);
        DanmuPool.Instance.mArrDanmu.Add(ret);
        return ret;
    }
    public GameObject GetBlueBallReflectDanmu() {
        GameObject ret = Instantiate(blueBallReflectDanmu);
        DanmuPool.Instance.mArrDanmu.Add(ret);
        return ret;
    }
    public GameObject GetBlueBigBallDanmu() {
        GameObject ret = Instantiate(blueBigBallDanmu);
        DanmuPool.Instance.mArrDanmu.Add(ret);
        return ret;
    }
    public GameObject GetWhiteSmallBallDanmu() {
        GameObject ret = Instantiate(whiteSmallBallDanmu);
        DanmuPool.Instance.mArrDanmu.Add(ret);
        return ret;
    }
    
}
