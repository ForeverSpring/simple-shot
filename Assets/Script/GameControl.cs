using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class Boundary {
    public static float xMin = -10f, xMax = 10f, yMin = -4.7f, yMax = 4.7f;
    public static bool IsOut(Vector3 v) {
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
    //TODO:添加互斥锁，让符卡在迭代器中顺序执行

    void Start() {
        rbBoss = gameobjBoss.GetComponent<Rigidbody>();
        //StartCoroutine(moveBoss());
        //StartCoroutine(FuKa());
        //StartCoroutine(DanmuTest());
        StartCoroutine(Stage1());
    }

    //测试新的弹幕功能
    IEnumerator DanmuTest() {
        float r = 2f;
        List<GameObject> lis = new List<GameObject>();
        for(int i = 0; i < 10; i++) {
            GameObject temp = Instantiate(gameobjDanmuBall);
            lis.Add(temp);
            temp.transform.position = gameobjBoss.transform.position + new Vector3(r * Mathf.Cos(36 * i * Mathf.PI / 180), r * Mathf.Sin(36 * i * Mathf.PI / 180), 0f);
            temp.GetComponent<moveDanmuBall>().SetRotateCenter(gameobjBoss.transform.position);
            temp.GetComponent<moveDanmuBall>().isRotating = true;
            temp.GetComponent<moveDanmuBall>().SetSpeed(0);
        }
        yield return new WaitForSeconds(1f);
        foreach(GameObject temp in lis) {
            temp.GetComponent<moveDanmuBall>().SetSpeed(10);
            temp.GetComponent<moveDanmuBall>().isRotating = false;
            temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
            yield return new WaitForSeconds(0.1f);
        }
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
                StartCoroutine(DanmuBallLike());
                yield return new WaitForSeconds(0.4f);
            }
            StartCoroutine(DanmuSpiralLike());
            yield return new WaitForSeconds(4f);
        }
        StartCoroutine(FuKa());
        yield return null;
    }
    //生成球状弹幕
    IEnumerator DanmuBallLike() {
        for (int i = 0; i < 36; i++) {
            GameObject temp = Instantiate(gameobjDanmuBall);
            //temp.transform.position = vSpawnDanmuBall;
            temp.transform.position = gameobjBoss.transform.position;
            temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 10 * i);
        }
        yield return null;
    }
    //生成螺旋状弹幕
    IEnumerator DanmuSpiralLike() {
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
    IEnumerator Stage1() {
        StartCoroutine(Fuka1_1());//50s
        yield return new WaitForSeconds(50f);
        StartCoroutine(Fuka1_2());//50s
        yield return new WaitForSeconds(50f);
        StartCoroutine(Fuka1_3());//50s
        yield return null;
    }
    //开幕雷击
    IEnumerator Fuka1_1() {
        Debug.Log("Fuka1_1 start");
        bool run = true;
        int times = 0;
        //两波10发自机狙
        while (run) {
            //两波自机狙
            for (int i = 0; i < 20; i++) {
                GameObject temp = Instantiate(gameobjDanmuBall);
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<moveDanmuBall>().SetSpeed(15);
                temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 20; i++) {
                GameObject temp = Instantiate(gameobjDanmuBall);
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<moveDanmuBall>().SetSpeed(15);
                temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2);
            //镜面反射弹幕
            for (int i = 0; i < 15; i++) {
                for (int j = 0; j < 10; j++) {
                    GameObject temp1 = Instantiate(gameobjDanmuBallReflect);
                    temp1.transform.position = gameobjBoss.transform.position;
                    temp1.transform.rotation = Quaternion.Euler(temp1.transform.forward * (115 - 2 * i));
                    temp1.GetComponent<moveDanmuBall>().SetSpeed(20);
                    GameObject temp2 = Instantiate(gameobjDanmuBallReflect);
                    temp2.transform.position = gameobjBoss.transform.position;
                    temp2.transform.rotation = Quaternion.Euler(temp2.transform.forward * (245 + 2 * i));
                    temp2.GetComponent<moveDanmuBall>().SetSpeed(20);
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(3f);
            times++;
            if (times == 2) {
                run = false;
            }
        }
        Debug.Log("Fuka1_1 end");
        yield return null;
    }
    //鱼雷型集合的自机狙
    IEnumerator Fuka1_2() {
        Debug.Log("Fuka1_2 start");
        float speedBoss = 5f;
        bool run = true;
        List<Vector3> moveLine = new List<Vector3> {
            //一次循环的4个移动方向
            new Vector3(0.866f, -0.5f, 0f),
            new Vector3(-0.866f, -0.5f, 0f),
            new Vector3(-0.866f, 0.5f, 0f),
            new Vector3(0.866f, 0.5f, 0f)
        };
        int times = 0;
        while (run) {
            rbBoss.velocity = moveLine[times % 4] * speedBoss;
            yield return new WaitForSeconds(0.8f);
            rbBoss.velocity = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(0.5f);
            //两波自机狙穿插随机弹幕
            List<GameObject> lis = new List<GameObject>();
            List<GameObject> lis2 = new List<GameObject>();
            for (int i = 0; i < 10; i++) {
                GameObject temp = Instantiate(gameobjDanmuBallReflect);
                lis.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 36 * i);
            }
            for (int i = 0; i < 10; i++) {
                GameObject temp = Instantiate(gameobjDanmuBallReflect);
                lis2.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 36 * i);
            }
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject temp in lis) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(0);
            }
            foreach (GameObject temp in lis2) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(0);
            }
            yield return new WaitForSeconds(0.3f);
            foreach (GameObject temp in lis) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(10);
                temp.transform.up = GameObject.Find("Player").transform.position - rbBoss.transform.position;
            }
            for (int i = 0; i < 30; i++) {
                GameObject temp = Instantiate(gameobjDanmuBall);
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<moveDanmuBall>().SetSpeed(3 + Random.Range(0f, 3f));
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * (90 + Random.Range(0f, 180f)));
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp in lis2) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(10);
                temp.transform.up = GameObject.Find("Player").transform.position - rbBoss.transform.position;
            }
            yield return new WaitForSeconds(1f);
            times++;
            if (times == 12) {
                run = false;
            }
        }
        Debug.Log("Fuka1_2 finish");
        yield return null;
    }
    //自机狙加密集型随机弹
    IEnumerator Fuka1_3() {
        Debug.Log("Fuka1_3 start");
        float speedBoss = 5f;
        rbBoss.velocity = new Vector3(0f, -1f, 0f) * speedBoss;
        yield return new WaitForSeconds(0.3f);
        rbBoss.velocity = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(0.3f);
        bool run = true;
        int times = 0;
        List<Vector3> moveLine = new List<Vector3> {
            new Vector3(-0.866f, -0.5f, 0f),
            new Vector3(0.965f, 0.258f, 0f),
        };
        while (run) {
            if (times == 3) {
                rbBoss.velocity = moveLine[0] * speedBoss;
                yield return new WaitForSeconds(0.8f);
                rbBoss.velocity = new Vector3(0f, 0f, 0f);
                yield return new WaitForSeconds(0.5f);
            }
            if (times == 6) {
                rbBoss.velocity = moveLine[1] * speedBoss;
                yield return new WaitForSeconds(1.6f);
                rbBoss.velocity = new Vector3(0f, 0f, 0f);
                yield return new WaitForSeconds(0.5f);
            }
            List<GameObject> lis = new List<GameObject>();
            for (int i = 0; i < 5; i++) {
                GameObject temp = Instantiate(gameobjDanmuBall);
                lis.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                temp.GetComponent<moveDanmuBall>().SetSpeed(3);
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 72 * i);
            }
            //暂停时间决定内圈大小
            yield return new WaitForSeconds(0.5f);
            List<GameObject> lis1 = new List<GameObject>();
            foreach (GameObject temp in lis) {
                temp.GetComponent<moveDanmuBall>().SetSpeed(0);
                for (int i = 0; i < 40; i++) {
                    GameObject temp1 = Instantiate(gameobjDanmuBall);
                    lis1.Add(temp1);
                    temp1.transform.position = temp.transform.position;
                    temp1.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    temp1.GetComponent<moveDanmuBall>().SetSpeed(3);
                    temp1.transform.rotation = Quaternion.Euler(temp.transform.forward * 9 * i);
                }
            }
            //暂停时间决定外圈大小
            yield return new WaitForSeconds(0.5f);
            foreach (GameObject temp1 in lis1) {
                temp1.GetComponent<moveDanmuBall>().SetSpeed(0);
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp1 in lis1) {
                temp1.GetComponent<moveDanmuBall>().SetSpeed(1 + Random.Range(0f, 1f));
                //temp1.transform.rotation = Quaternion.Euler(temp1.transform.forward * Random.Range(0f, 360f));
                temp1.transform.Rotate(new Vector3(0f, 0f, Random.Range(-5f, 5f)), Space.Self);
            }
            foreach (GameObject temp in lis) {
                temp.GetComponent<moveDanmuBall>().SetSpeed(5);
            }
            //弹幕旋转后变自机狙
            float r = 2f;
            List<GameObject> lis2 = new List<GameObject>();
            for (int i = 0; i < 10; i++) {
                GameObject temp = Instantiate(gameobjDanmuBall);
                lis2.Add(temp);
                temp.transform.position = gameobjBoss.transform.position + new Vector3(r * Mathf.Cos(36 * i * Mathf.PI / 180), r * Mathf.Sin(36 * i * Mathf.PI / 180), 0f);
                temp.GetComponent<moveDanmuBall>().SetRotateCenter(gameobjBoss.transform.position);
                temp.GetComponent<moveDanmuBall>().isRotating = true;
                temp.GetComponent<moveDanmuBall>().SetSpeed(0);
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp in lis2) {
                temp.GetComponent<moveDanmuBall>().SetSpeed(10);
                temp.GetComponent<moveDanmuBall>().isRotating = false;
                temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            times++;
            if (times == 9) {
                run = false;
            }
            yield return null;
        }
        Debug.Log("Fuka1_3 end");
    }
}
