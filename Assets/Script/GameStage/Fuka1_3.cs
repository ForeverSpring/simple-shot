using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuka1_3 : Fuka {
    void Start() {
        fukaName = "自机狙加密集型随机弹";
        fukaType = FukaType.LifeFuka;
        vBossSpawn = new Vector3(-1.975f, 3.5f, 0f);
    }
    public override void Run() {
        Debug.Log("Fuka1_3 start");
        GameControl.Instance.WaitFuka();
        GameUIControl.Instance.SetTopSlideVisiable(true);
        GameUIControl.Instance.FukaNameStart(fukaName);
        FukaProcess.Instance.SetNewProcessData(100);
        StartCoroutine("StartIEnumerator");
    }
    public override void Stop() {
        Debug.Log("Fuka1_3 end");
        StopCoroutine("_Fuka1_3");
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
        StartCoroutine("_Fuka1_3");
        yield return null;
    }
    IEnumerator _Fuka1_3() {
        float speedBoss = 3f;
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
                DanmuPool.Instance.mArrDanmu.Add(temp);
                lis.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.transform.localScale = temp.transform.localScale * 0.5f;
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
                    DanmuPool.Instance.mArrDanmu.Add(temp1);
                    lis1.Add(temp1);
                    temp1.transform.position = temp.transform.position;
                    temp1.transform.localScale = temp.transform.localScale;
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
                DanmuPool.Instance.mArrDanmu.Add(temp);
                lis2.Add(temp);
                temp.transform.position = gameobjBoss.transform.position +
                new Vector3(r * Mathf.Cos(36 * i * Mathf.PI / 180), r * Mathf.Sin(36 * i * Mathf.PI / 180), 0f);
                temp.GetComponent<moveDanmuBall>().SetRotateCenter(gameobjBoss.transform.position);
                temp.GetComponent<moveDanmuBall>().isRotating = true;
                temp.GetComponent<moveDanmuBall>().SetSpeed(0);
            }
            yield return new WaitForSeconds(1f);
            foreach (GameObject temp in lis2) {
                temp.GetComponent<moveDanmuBall>().SetSpeed(8);
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
        StartCoroutine("_Fuka1_3");
    }
}
