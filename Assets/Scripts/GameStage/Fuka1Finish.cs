using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fuka1Finish : Fuka
{
    private void Start() {
        fukaName = "Stage1Finish";
    }
    public override void Run() {
        Debug.Log("Fuka1Finish start");
        GameControl.Instance.WaitFuka();
        EnemyManager.Instance.RemoveEnemy(gameobjBoss);
        GameUIControl.Instance.SetTopSlideVisiable(false);
        StartCoroutine("Finish");
    }

    public override void Stop() {
        Debug.Log("Fuka1Finish end");
        StopCoroutine("Finish");
        EnvironmentObjectsManager.Instance.ClearDanmu();
        GameControl.Instance.SignalFuka();
    }

    IEnumerator Finish() {
        //show finish animation here
        EnvironmentObjectsManager.Instance.mainCamera.DOShakePosition(2f, new Vector3(0.3f, 0.3f, 0));
        AudioControl.Instance.PlayBeatBoss();
        AudioControl.Instance.StopBGM();
        EnvironmentObjectsManager.Instance.ClearBullet();
        if (EnvironmentObjectsManager.Instance.BossObject) {
            EnvironmentObjectsManager.Instance.BossObject.GetComponent<CircleCollider2D>().enabled = false;
            EnvironmentObjectsManager.Instance.BossObject = null;
        }
        yield return new WaitForSeconds(5f);
        EnvironmentObjectsManager.Instance.mainCamera.transform.position = new Vector3(0, 0, -10);
        GameControl.Instance.SetGameWin(true);
        yield return null;
    }
}
