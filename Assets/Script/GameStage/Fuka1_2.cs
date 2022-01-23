using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuka1_2 : Fuka {
    void Start() {
        fukaName = "鱼雷型集合的自机狙";
        fukaType = FukaType.LifeFuka;
        vBossSpawn = new Vector3(-1.975f, 3.5f, 0f);
    }

    public override void Run() {
        Debug.Log("Fuka1_2 start");
        GameControl.Instance.WaitFuka();
        GameUIControl.Instance.SetTopSlideVisiable(true);
        GameUIControl.Instance.FukaNameStart(fukaName);
        FukaProcess.Instance.SetNewProcessData(100);
        StartCoroutine("StartIEnumerator");
    }

    public override void Stop() {
        Debug.Log("Fuka1_2 finish");
        StopCoroutine("_Fuka1_2");
        DanmuPool.Instance.ClearDanmu();
        GameControl.Instance.SignalFuka();
    }
    IEnumerator StartIEnumerator() {
        float speedBoss = 3f;
        rbBoss.velocity = (vBossSpawn - rbBoss.transform.position).normalized * speedBoss;
        float waitTime = Mathf.Sqrt((vBossSpawn - rbBoss.transform.position).sqrMagnitude) / speedBoss;
        yield return new WaitForSeconds(waitTime);
        rbBoss.velocity = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(2f);
        StartCoroutine("_Fuka1_2");
        yield return null;
    }
    IEnumerator _Fuka1_2() {
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
                GameObject temp = DanmuFactory.Instance.GetBlueBallReflectDanmu();
                lis.Add(temp);
                DanmuPool.Instance.mArrDanmu.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 36 * i);
            }
            for (int i = 0; i < 10; i++) {
                GameObject temp = DanmuFactory.Instance.GetBlueBallReflectDanmu();
                DanmuPool.Instance.mArrDanmu.Add(temp);
                lis2.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 36 * i);
            }
            AudioControl.Instance.PlayBossTan02();
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
                //temp.transform.rotation = new Quaternion();
            }
            AudioControl.Instance.PlayBossTan01();
            for (int i = 0; i < 60; i++) {
                GameObject temp = DanmuFactory.Instance.GetGreenBallDanmu();
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<moveDanmuBall>().SetSpeed(3 + Random.Range(0f, 3f));
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * (90 + Random.Range(0f, 180f)));
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp in lis2) {
                temp.GetComponent<moveDanmuBallReflect>().SetSpeed(speed_round);
                temp.transform.up = GameObject.Find("Player").transform.position - rbBoss.transform.position;
                //temp.transform.rotation = new Quaternion();
            }
            AudioControl.Instance.PlayBossTan01();
            yield return new WaitForSeconds(1f);
            times++;
            if (times == 12) {
                run = false;
            }
        }
        StartCoroutine("_Fuka1_2");
        yield return null;
    }
}
