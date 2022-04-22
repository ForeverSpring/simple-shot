using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fuka1_1 : Fuka {
    void Start() {
        fukaName = "开幕雷击";
        fukaType = FukaType.TimeFuka;
        fukaScore = 1000;
    }

    public override void Run() {
        GameUIControl.Instance.SetTopSlideVisiable(false);
        GameControl.Instance.WaitFuka();
        StartCoroutine(nameof(_Fuka1_1));
    }

    public override void Stop() {
        StopCoroutine(nameof(_Fuka1_1));
        FinishGetScore();
        EnvironmentObjectsManager.Instance.BossObject.GetComponent<LootSpawner>().SpawnLocal();
        Debug.Log("Fuka1_1 end");
        GameControl.Instance.SignalFuka();
    }

    IEnumerator _Fuka1_1() {
        Debug.Log("start fuka 1_1");
        yield return new WaitForSeconds(1f);
        GameObject bossObj = EnemyFactory.Instance.GetZakuBoss();
        EnvironmentObjectsManager.Instance.SetBoss(bossObj);
        bossObj.transform.position = EnvironmentObjectsManager.Instance.MarkObjectSquare.transform.position;
        bossObj.transform.DOMove(Boundary.ViewPortMid + new Vector3(0, 3.5f), 2f);
        yield return new WaitForSeconds(5f);
        DialogSystem.Instance.StartDialog("Stage1");
        yield return new WaitUntil(() => DialogSystem.Instance.isDialoging != true);
        Debug.Log("Fuka1_1 start");
        AudioControl.Instance.PlayStage1Boss();
        GameUIControl.Instance.FukaNameStart(fukaName);
        GameUIControl.Instance.BossFukaImageShow();
        AudioControl.Instance.PlayFukaExtend();
        bool run = true;
        int times = 0;
        //两波10发自机狙
        while (run) {
            yield return new WaitForSeconds(3f);
            AudioControl.Instance.PlayTanWarning();
            yield return new WaitForSeconds(3f);
            //两波自机狙
            for (int i = 0; i < 20; i++) {
                AudioControl.Instance.PlayBossTan01();
                GameObject temp = DanmuFactory.Instance.GetRedKnifeDanmu();
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<Danmu>().speedDanmuBall = 15;
                temp.transform.up = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - temp.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            AudioControl.Instance.PlayTanWarning();
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < 20; i++) {
                AudioControl.Instance.PlayBossTan01();
                GameObject temp = DanmuFactory.Instance.GetRedKnifeDanmu();
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<Danmu>().SetSpeed(15);
                temp.transform.up = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - temp.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2);
            //镜面反射弹幕
            for (int i = 0; i < 10; i++) {
                AudioControl.Instance.PlayBossRayShot();
                for (int j = 0; j < 10; j++) {
                    GameObject temp1 = DanmuFactory.Instance.GetBlueBallReflectDanmu();
                    temp1.transform.position = gameobjBoss.transform.position;
                    temp1.transform.rotation = Quaternion.Euler(temp1.transform.forward * (115 - 2 * i));
                    temp1.GetComponent<Danmu>().SetSpeed(15);
                    GameObject temp2 = DanmuFactory.Instance.GetBlueBallReflectDanmu();
                    temp2.transform.position = gameobjBoss.transform.position;
                    temp2.transform.rotation = Quaternion.Euler(temp2.transform.forward * (245 + 2 * i));
                    temp2.GetComponent<Danmu>().SetSpeed(15);
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
        Stop();
        yield return null;
    }
}
