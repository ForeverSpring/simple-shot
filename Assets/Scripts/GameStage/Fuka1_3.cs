using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuka1_3 : Fuka {
    List<Vector3> movePos = new() {
        new Vector3(-3.5f, 2f, 0f),
        new Vector3(-5f, 0f, 0f),
        new Vector3(1.2f, 1.2f, 0f),
        new Vector3(-1.975f, 3f, 0f)
    };
    void Start() {
        fukaName = "扎古弹幕风暴";
        fukaType = FukaType.LifeFuka;
        fukaScore = 1000;
    }
    public override void Run() {
        Debug.Log("Fuka1_3 start");
        GameControl.Instance.WaitFuka();
        GameUIControl.Instance.SetTopSlideVisiable(true);
        GameUIControl.Instance.FukaNameStart(fukaName);
        GameUIControl.Instance.BossFukaImageShow();
        AudioControl.Instance.PlayFukaExtend();
        gameobjBoss.SetActive(true);
        FukaProcess.Instance.SetNewProcessData(200);
        StartCoroutine("StartIEnumerator");
    }
    public override void Stop() {
        StopCoroutine("_Fuka1_3");
        FinishGetScore();
        Debug.Log("Fuka1_3 end");
        EnvironmentObjectsManager.Instance.BossObject.GetComponent<LootSpawner>().SpawnLocal();
        EnvironmentObjectsManager.Instance.ClearDanmu();
        timer.EndTime();
        GameUIControl.Instance.SetFukaUseTime(timer.GetRunTime());
        GameControl.Instance.SignalFuka();
    }
    IEnumerator StartIEnumerator() {
        Tween tween = rbBoss.DOMove(new Vector3(-1.975f, 3f, 0f), 2f);
        yield return tween.WaitForCompletion();
        StartCoroutine(nameof(_Fuka1_3));
        timer.ResetTime();
        timer.StartTime();
    }
    IEnumerator _Fuka1_3() {
        bool run = true;
        int times = 0;
        while (run) {
            Tween bossMove = rbBoss.DOMove(movePos[times], 2f).SetEase(Ease.Linear);
            yield return bossMove.WaitForCompletion();
            yield return new WaitForSeconds(1f);
            List<GameObject> lis = new List<GameObject>();
            for (int i = 0; i < 5; i++) {
                GameObject temp = DanmuFactory.Instance.GetRedBallDanmu();
                lis.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                //temp.transform.localScale = temp.transform.localScale * 0.5f;
                temp.GetComponent<Danmu>().SetSpeed(3);
                temp.transform.rotation = Quaternion.Euler(72 * i * temp.transform.forward);
                AudioControl.Instance.PlayBossTanKira();
            }
            //暂停时间决定内圈大小
            yield return new WaitForSeconds(0.5f);
            AudioControl.Instance.PlayBossTanKira();
            List<GameObject> lis1 = new List<GameObject>();
            foreach (GameObject temp in lis) {
                temp.GetComponent<Danmu>().SetSpeed(0);
                for (int i = 0; i < 40; i++) {
                    GameObject temp1 = DanmuFactory.Instance.GetWhiteSmallBallDanmu();
                    lis1.Add(temp1);
                    temp1.transform.position = temp.transform.position;
                    temp1.transform.localScale = temp.transform.localScale;
                    temp1.GetComponent<Danmu>().SetSpeed(3);
                    temp1.transform.rotation = Quaternion.Euler(9 * i * temp.transform.forward);
                }
                AudioControl.Instance.PlayBossTanWoo();
            }
            //暂停时间决定外圈大小
            yield return new WaitForSeconds(0.5f);
            foreach (GameObject temp1 in lis1) {
                temp1.GetComponent<Danmu>().SetSpeed(0);
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp1 in lis1) {
                temp1.GetComponent<Danmu>().SetSpeed(1 + Random.Range(0f, 1f));
                //temp1.transform.rotation = Quaternion.Euler(temp1.transform.forward * Random.Range(0f, 360f));
                temp1.transform.Rotate(new Vector3(0f, 0f, Random.Range(-5f, 5f)), Space.Self);
            }
            foreach (GameObject temp in lis) {
                temp.GetComponent<Danmu>().SetSpeed(5);
            }
            //弹幕旋转后变自机狙
            float r = 2f;
            List<GameObject> lis2 = new List<GameObject>();
            for (int i = 0; i < 10; i++) {
                GameObject temp = DanmuFactory.Instance.GetRedKnifeDanmu();
                lis2.Add(temp);
                temp.transform.position = gameobjBoss.transform.position +
                new Vector3(r * Mathf.Cos(36 * i * Mathf.PI / 180), r * Mathf.Sin(36 * i * Mathf.PI / 180), 0f);
                temp.GetComponent<Danmu>().SetRotateCenter(gameobjBoss.transform.position);
                temp.GetComponent<Danmu>().isRotating = true;
                temp.GetComponent<Danmu>().SetSpeed(0);
            }
            AudioControl.Instance.PlayBossTan01();
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp in lis2) {
                temp.GetComponent<Danmu>().SetSpeed(8);
                temp.GetComponent<Danmu>().isRotating = false;
                temp.transform.up = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - temp.transform.position;
                AudioControl.Instance.PlayBossTan01();
                yield return new WaitForSeconds(0.1f);
            }
            times++;
            if (times == movePos.Count) {
                run = false;
            }
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(nameof(_Fuka1_3));
    }
}
