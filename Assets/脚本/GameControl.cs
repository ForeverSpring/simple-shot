using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class Boundary {
    public static float xMin = -10f, xMax = 10f, yMin = -4.7f, yMax = 4.7f;
    public static bool isOut(Vector3 v) {
        if (v.x > xMin && v.x < xMax && v.y > yMin && v.y < yMax) {
            return false;
        }
        return true;
    }
}
public class GameControl : MonoBehaviour {
    public GameObject gameobjDanmuBall;
    public GameObject gameobjDanmuBallReflect;
    public GameObject gameobjBoss;
    private Rigidbody rbBoss;
    private Vector3 vSpawnDanmuBall = new Vector3(0f, 3f, 0f);

    void Start() {
        rbBoss = gameobjBoss.GetComponent<Rigidbody>();
        //StartCoroutine(moveBoss());
        //StartCoroutine(FuKa());
        StartCoroutine(Fuka1());
    }

    //测试新的弹幕功能
    IEnumerator danmuTest() {

        yield return null;
    }
    //左右来回移动Boss
    IEnumerator moveBoss() {
        float speedBoss = 9f;
        while (true) {
            rbBoss.velocity = new Vector3(1f, 0f, 0f) * speedBoss;
            yield return new WaitForSeconds(1f);
            rbBoss.velocity = new Vector3(-1f, 0f, 0f) * speedBoss;
            yield return new WaitForSeconds(2f);
            rbBoss.velocity = new Vector3(1f, 0f, 0f) * 9;
            yield return new WaitForSeconds(1f);
        }
    }
    //弹幕的简单组合
    IEnumerator FuKa() {
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 10; j++) {
                StartCoroutine(danmuBallLike());
                yield return new WaitForSeconds(0.4f);
            }
            StartCoroutine(danmuSpiralLike());
            yield return new WaitForSeconds(4f);
        }
        StartCoroutine(FuKa());
        yield return null;
    }
    //生成球状弹幕
    IEnumerator danmuBallLike() {
        for (int i = 0; i < 36; i++) {
            GameObject temp = Instantiate(gameobjDanmuBall);
            //temp.transform.position = vSpawnDanmuBall;
            temp.transform.position = gameobjBoss.transform.position;
            temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 10 * i);
        }
        yield return null;
    }
    //生成螺旋状弹幕
    IEnumerator danmuSpiralLike() {
        for (int i = 0; i < 36; i++) {
            GameObject temp = Instantiate(gameobjDanmuBall);
            //temp.transform.position = vSpawnDanmuBall;
            temp.transform.position = gameobjBoss.transform.position;
            temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 10 * i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }


    /// <summary>
    /// 一面符卡
    /// </summary>
    /// <returns></returns>
    IEnumerator Fuka1() {
        StartCoroutine(Fuka1_1());
        yield return new WaitForSeconds(25f);
        StartCoroutine(Fuka1_1());
        yield return null;
    }
    //开幕雷击
    IEnumerator Fuka1_1() {
        //两波10发自机狙
        for(int i = 0; i < 20; i++) {
            GameObject temp = Instantiate(gameobjDanmuBall);
            temp.transform.position = gameobjBoss.transform.position;
            temp.GetComponent<moveDanmuBall>().setSpeed(15);
            temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 20; i++) {
            GameObject temp = Instantiate(gameobjDanmuBall);
            temp.transform.position = gameobjBoss.transform.position;
            temp.GetComponent<moveDanmuBall>().setSpeed(15);
            temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        //镜面反射弹幕
        for(int i = 0; i < 15; i++) {
            for (int j = 0; j < 10; j++) {
                GameObject temp1 = Instantiate(gameobjDanmuBallReflect);
                temp1.transform.position = gameobjBoss.transform.position;
                temp1.transform.rotation = Quaternion.Euler(temp1.transform.forward * (115 - 2 * i));
                temp1.GetComponent<moveDanmuBall>().setSpeed(20);
                GameObject temp2 = Instantiate(gameobjDanmuBallReflect);
                temp2.transform.position = gameobjBoss.transform.position;
                temp2.transform.rotation = Quaternion.Euler(temp2.transform.forward * (245 + 2 * i));
                temp2.GetComponent<moveDanmuBall>().setSpeed(20);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    //
    IEnumerator Fuka1_2() {

        yield return null;
    }
    //
    IEnumerator Fuka1_3() {

        yield return null;
    }
}
