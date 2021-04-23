using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public GameObject gameobjDanmuBall;
    public GameObject gameobjBoss;
    private Rigidbody rbBoss;
    private Vector3 vSpawnDanmuBall = new Vector3(0f, 3f, 0f);

    void Start() {
        rbBoss = gameobjBoss.GetComponent<Rigidbody>();
        StartCoroutine(moveBoss());
        StartCoroutine(FuKa());
    }


    void Update() {

    }

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

    IEnumerator danmuBallLike() {
        for (int i = 0; i < 36; i++) {
            GameObject temp = Instantiate(gameobjDanmuBall);
            //temp.transform.position = vSpawnDanmuBall;
            temp.transform.position = gameobjBoss.transform.position;
            temp.transform.rotation = Quaternion.Euler(temp.transform.forward * 10 * i);
        }
        yield return null;
    }

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
}
