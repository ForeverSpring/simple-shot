using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuka1_2 : Fuka {
    void Start() {
        name = "鱼雷型集合的自机狙";
    }

    public override void Run() {
        StartCoroutine(_Fuka1_2());
    }

    public override void Stop() {
        StopCoroutine(_Fuka1_2());
    }

    IEnumerator _Fuka1_2() {
        mainControl.WaitFuka();
        Debug.Log("Fuka1_2 start");
        float speedBoss = 3f;
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
            float speed_round = 5f;
            foreach (GameObject temp in lis) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(0);
            }
            foreach (GameObject temp in lis2) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(0);
            }
            yield return new WaitForSeconds(0.3f);
            foreach (GameObject temp in lis) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(speed_round);
                temp.transform.up = GameObject.Find("Player").transform.position - rbBoss.transform.position;
            }
            for (int i = 0; i < 60; i++) {
                GameObject temp = Instantiate(gameobjDanmuBall);
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<moveDanmuBall>().SetSpeed(3 + Random.Range(0f, 3f));
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * (90 + Random.Range(0f, 180f)));
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp in lis2) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(speed_round);
                temp.transform.up = GameObject.Find("Player").transform.position - rbBoss.transform.position;
            }
            yield return new WaitForSeconds(1f);
            times++;
            if (times == 12) {
                run = false;
            }
        }
        Debug.Log("Fuka1_2 finish");
        mainControl.SignalFuka();
        yield return null;
    }
}
