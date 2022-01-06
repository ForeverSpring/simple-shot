using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fuka1_1 : Fuka
{
    void Start(){
        fukaName = "开幕雷击";
        fukaType = FukaType.TimeFuka;
    }

    public override void Run() {
        GameUIControl.Instance.SetTopSlideVisiable(false);
        GameUIControl.Instance.FukaNameStart(fukaName);
        StartCoroutine("_Fuka1_1");
    }

    public override void Stop() {
        StopCoroutine("_Fuka1_1");
    }

    IEnumerator _Fuka1_1() {
        GameControl.Instance.WaitFuka();
        Debug.Log("Fuka1_1 start");
        bool run = true;
        int times = 0;
        //两波10发自机狙
        while (run) {
            yield return new WaitForSeconds(3f);
            //两波自机狙
            for (int i = 0; i < 20; i++) {
                AudioControl.Instance.PlayBossTan01();
                GameObject temp = Instantiate(gameobjDanmuBall);
                DanmuPool.Instance.mArrDanmu.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<moveDanmuBall>().speedDanmuBall = 15;
                temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 20; i++) {
                AudioControl.Instance.PlayBossTan01();
                GameObject temp = Instantiate(gameobjDanmuBall);
                DanmuPool.Instance.mArrDanmu.Add(temp);
                temp.transform.position = gameobjBoss.transform.position;
                temp.GetComponent<moveDanmuBall>().SetSpeed(15);
                temp.transform.up = GameObject.Find("Player").transform.position - temp.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2);
            //镜面反射弹幕
            for (int i = 0; i < 10; i++) {
                AudioControl.Instance.PlayBossRayShot();
                for (int j = 0; j < 10; j++) {
                    GameObject temp1 = Instantiate(gameobjDanmuBallReflect);
                    DanmuPool.Instance.mArrDanmu.Add(temp1);
                    temp1.transform.position = gameobjBoss.transform.position;
                    temp1.transform.rotation = Quaternion.Euler(temp1.transform.forward * (115 - 2 * i));
                    temp1.GetComponent<moveDanmuBall>().SetSpeed(15);
                    GameObject temp2 = Instantiate(gameobjDanmuBallReflect);
                    DanmuPool.Instance.mArrDanmu.Add(temp2);
                    temp2.transform.position = gameobjBoss.transform.position;
                    temp2.transform.rotation = Quaternion.Euler(temp2.transform.forward * (245 + 2 * i));
                    temp2.GetComponent<moveDanmuBall>().SetSpeed(15);
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
        GameControl.Instance.SignalFuka();
        yield return null;
    }
}
