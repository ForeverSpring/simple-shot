using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fuka1_2 : Fuka {
    
    void Start() {
        fukaName = "太空型扎古手榴弹";
        fukaType = FukaType.LifeFuka;
        fukaScore = 1000;
    }

    public override void Run() {
        Debug.Log("Fuka1_2 start");
        GameControl.Instance.WaitFuka();
        GameUIControl.Instance.SetTopSlideVisiable(true);
        GameUIControl.Instance.FukaNameStart(fukaName);
        GameUIControl.Instance.BossFukaImageShow();
        AudioControl.Instance.PlayFukaExtend();
        gameobjBoss.SetActive(true);
        FukaProcess.Instance.SetNewProcessData(200);
        StartCoroutine(nameof(StartIEnumerator));
    }

    public override void Stop() {
        Debug.Log("Fuka1_2 finish");
        FinishGetScore();
        StopCoroutine(nameof(_Fuka1_2));
        EnvironmentObjectsManager.Instance.BossObject.GetComponent<LootSpawner>().SpawnLocal();
        EnvironmentObjectsManager.Instance.ClearDanmu();
        timer.EndTime();
        GameUIControl.Instance.SetFukaUseTime(timer.GetRunTime());
        GameControl.Instance.SignalFuka();
    }
    IEnumerator StartIEnumerator() {
        yield return new WaitForSeconds(2f);
        Tween bossMove = rbBoss.transform.DOMove(new Vector3(-1.975f, 3.5f, 0f), 2f);
        yield return bossMove.WaitForCompletion();
        yield return new WaitForSeconds(2f);
        StartCoroutine(nameof(_Fuka1_2));
        timer.ResetTime();
        timer.StartTime();
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
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 36 * i);
            }
            for (int i = 0; i < 10; i++) {
                GameObject temp = DanmuFactory.Instance.GetBlueBallReflectDanmu();
                lis2.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 36 * i);
            }
            AudioControl.Instance.PlayBossTan02();
            yield return new WaitForSeconds(0.2f);
            float speed_round = 5f;
            foreach (GameObject temp in lis) {
                temp.GetComponent<DanmuReflect>().SetSpeed(0);
            }
            foreach (GameObject temp in lis2) {
                temp.GetComponent<DanmuReflect>().SetSpeed(0);
            }
            yield return new WaitForSeconds(0.3f);
            foreach (GameObject temp in lis) {
                temp.GetComponent<DanmuReflect>().SetSpeed(speed_round);
                temp.transform.up = GameObject.Find("Player").transform.position - rbBoss.transform.position;
                //temp.transform.rotation = new Quaternion();
            }
            AudioControl.Instance.PlayBossTan01();
            for (int i = 0; i < 60; i++) {
                GameObject temp = DanmuFactory.Instance.GetGreenBallDanmu();
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<Danmu>().SetSpeed(3 + Random.Range(0f, 3f));
                temp.transform.rotation = Quaternion.Euler(temp.transform.forward * (90 + Random.Range(0f, 180f)));
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp in lis2) {
                temp.GetComponent<DanmuReflect>().SetSpeed(speed_round);
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
        StartCoroutine(nameof(_Fuka1_2));
        yield return null;
    }
}
