using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Midway : Fuka {
    GameObject ship, butterfly, BlueNormalEnemy, RedNormalEnemy, GreenNormalEnemy, YellowNormalEnemy, YellowHyperEnemy;
    Sequence sequence;
    bool enemyExist => EnemyManager.Instance.EnemyExist;
    private void Start() {
        fukaName = "stage1midway";
        fukaType = FukaType.TimeFuka;
        ship = (GameObject)Resources.Load("Prefab/Enemy/ship1");
        butterfly = (GameObject)Resources.Load("Prefab/Enemy/Butterfly");
        BlueNormalEnemy = (GameObject)Resources.Load("Prefab/Enemy/BlueNormalEnemy");
        RedNormalEnemy = (GameObject)Resources.Load("Prefab/Enemy/RedNormalEnemy");
        GreenNormalEnemy = (GameObject)Resources.Load("Prefab/Enemy/GreenNormalEnemy");
        YellowNormalEnemy = (GameObject)Resources.Load("Prefab/Enemy/YellowNormalEnemy");
        YellowHyperEnemy = (GameObject)Resources.Load("Prefab/Enemy/YellowHyperEnemy");
        sequence = DOTween.Sequence();
    }
    public override void Run() {
        GameControl.Instance.WaitFuka();
        GameUIControl.Instance.SetTopSlideVisiable(false);
        AudioControl.Instance.PlayStage1Midway();
        StartCoroutine(CreatEnemy1());
    }
    public override void Stop() {
        GameControl.Instance.SignalFuka();
    }
    void CreatRoundDanmuAt(Transform transform) {
        if (!transform.gameObject.activeSelf)
            return;
        for (int i = 0; i < 8; i++) {
            GameObject bullet = DanmuFactory.Instance.Getfireball_red_tail_big();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.transform.Rotate(transform.forward * 45 * i);
        }
    }
    void CreatTowardsPlayerDanmuAt(Transform transform) {
        if (!transform.gameObject.activeSelf)
            return;
        GameObject bullet = DanmuFactory.Instance.GetYellowCubeDanmu();
        bullet.transform.position = transform.position;
        bullet.transform.up = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - bullet.transform.position;
        bullet.GetComponent<Danmu>().SetSpeed(6f);
        AudioControl.Instance.PlayBossRayShot();
    }
    IEnumerator CreatBigTowardsPlayerDamuAt(Transform transform) {
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
        float left_len = 0.5f, up_len = 0.5f;
        Vector3 vecUp = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - transform.position;
        for (int i = 0; i < 3 && transform.gameObject.activeSelf; i++) {
            float mid = (2 - (float)i) / 2;
            for (int j = 0; j < 3 - i; j++) {
                GameObject bullet = DanmuFactory.Instance.GetBlueDiamondDanmu();
                bullet.transform.up = vecUp;
                bullet.transform.position = transform.position + left_len * (mid - j) * bullet.transform.right.normalized + up_len * i * bullet.transform.up.normalized;
                bullet.transform.up = vecUp;
            }
        }
        AudioControl.Instance.PlayBossTan02();
    }
    IEnumerator CreatGatherDanmuTowardsCoroutine(Transform transform, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if (transform.gameObject.activeSelf)
            CreatGatherDanmuTowardsPlayerAt(transform);
        yield return null;
    }
    void CreatGatherDanmuTowardsPlayerAt(Transform transform) {
        Vector3 vecUp = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - transform.position;
        for (int i = 0; i < 4; i++) {
            GameObject danmu = DanmuFactory.Instance.GetBlueBallDanmu();
            danmu.GetComponent<Danmu>().SetSpeed(2);
            danmu.transform.position = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);
            danmu.transform.up = vecUp;
            AudioControl.Instance.PlayBossTan02();
        }
    }
    IEnumerator CreatDiamondDanmuTowardsCoroutine(Transform transform, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Vector3 vecUp = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - transform.position;
        if (transform.gameObject.activeSelf) {
            for (int i = 0; i < 2; i++) {
                GameObject danmu = DanmuFactory.Instance.GetWhiteDiamondDanmu();
                danmu.GetComponent<Danmu>().SetSpeed(5);
                danmu.transform.position = transform.position;
                danmu.transform.up = vecUp;
                yield return new WaitForSeconds(0.05f);
            }
            AudioControl.Instance.PlayBossTan01();
        }
        yield return null;
    }
    IEnumerator CreatRoundTowardsCoroutine(Transform transform, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Vector3 vecUp = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - transform.position;
        if (transform.gameObject.activeSelf)
            for (int i = 0; i < 8; i++) {
                GameObject bullet = DanmuFactory.Instance.GetRedBallDanmu();
                bullet.transform.position = transform.position;
                bullet.transform.up = vecUp;
                bullet.transform.Rotate(transform.forward * 45 * i);
            }
        AudioControl.Instance.PlayBossTan02();
        yield return new WaitForSeconds(2f);
        if (transform.gameObject.activeSelf) {
            if (transform.position.x >= Boundary.xMin + 0.5f * Boundary.weight) {
                transform.gameObject.transform.DOMoveX(Boundary.xMax + 2f, 2f).OnComplete(() => {
                    transform.gameObject.SetActive(false);
                });
            }
            else {
                transform.gameObject.transform.DOMoveX(Boundary.xMin - 2f, 2f).OnComplete(() => {
                    transform.gameObject.SetActive(false);
                });
            }
        }
        yield return null;
    }
    IEnumerator CreatTowardsCoroutine(Transform transform, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < 12 && transform.gameObject.activeSelf; i++) {
            GameObject danmu = DanmuFactory.Instance.GetWhiteDiamondDanmu();
            danmu.GetComponent<Danmu>().SetSpeed(2.5f);
            danmu.transform.position = transform.position;
            danmu.transform.up = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - transform.position;
            yield return new WaitForSeconds(0.5f);
            AudioControl.Instance.PlayBossTan01();
        }
        yield return null;
    }
    IEnumerator CreatEnemy1() {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++) {
            GameObject tmpship = PoolManager.Release(ship, EnvironmentObjectsManager.Instance.MarkObjectSquare.transform.position);
            tmpship.GetComponent<Enemy>().Initialize(3, 10);
            EnemyManager.Instance.AddEnemy(tmpship);
            Vector3 toPos = new Vector3(Random.Range(Boundary.xMin, Boundary.xMax),
                Random.Range((Boundary.yMin + Boundary.yMax) / 2, Boundary.yMax),
                0f);
            tmpship.transform.DOMove(toPos, 2f).OnComplete(() => {
                StartCoroutine(CreatGatherDanmuTowardsCoroutine(tmpship.transform, 1f));
            });
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
        }
        yield return new WaitForSeconds(1f);
        GameObject CenterEnemy = PoolManager.Release(butterfly, EnvironmentObjectsManager.Instance.MarkObjectSquare.transform.position);
        EnemyManager.Instance.AddEnemy(CenterEnemy);
        CenterEnemy.GetComponent<Enemy>().Initialize(20, 50);
        Tween CenterEnemyMove = CenterEnemy.transform.DOMove(Boundary.ViewPortMid + new Vector3(0, (Boundary.yMax - Boundary.yMin) / 4, 0), 2f);
        yield return CenterEnemyMove.WaitForCompletion();
        yield return new WaitForSeconds(2f);
        float dangle = 360 / (float)15;
        for (int times = 0; times < 2; times++) {
            for (int i = 0; i < 2 && CenterEnemy.activeSelf; i++) {
                for (int j = 0; j < 15; j++) {
                    GameObject danmuBall = DanmuFactory.Instance.GetBlueBigBallDanmu();
                    danmuBall.transform.position = CenterEnemy.transform.position;
                    danmuBall.transform.rotation = Quaternion.Euler(CenterEnemy.transform.forward * (dangle * j + 10 * i));
                }
                AudioControl.Instance.PlayBossTan02();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(3f);
        //Enemys leave screen
        List<GameObject> enemys = EnemyManager.Instance.GetAllEnemys;
        foreach (GameObject enemy in enemys) {
            enemy.transform.DOMove(enemy.transform.position +
                (enemy.transform.position - Boundary.ViewPortMid).normalized * 7f, 3f)
                .OnComplete(() => {
                    enemy.SetActive(false);
                });
        }
        StartCoroutine(CreatEnemy2());
    }
    IEnumerator CreatEnemy2_1() {
        float rowDis = 1f;
        Vector3 startPos = new Vector3(Boundary.xMin - 2f, Boundary.yMin + 0.75f * Boundary.height, 0f);
        for (int i = 0; i < 8; i++) {
            for (int row = 0; row < 3; row++) {
                GameObject rowEnemy = PoolManager.Release(RedNormalEnemy, startPos + new Vector3(0f, row * rowDis, 0f));
                EnemyManager.Instance.AddEnemy(rowEnemy);
                rowEnemy.GetComponent<Enemy>().Initialize(2, 10);
                Tween rowTween = rowEnemy.transform.DOMove((startPos + new Vector3(0f, row * rowDis, 0f) + new Vector3(15f, 0, 0)), 32)
                    .OnStart(() => {
                        StartCoroutine(CreatDiamondDanmuTowardsCoroutine(rowEnemy.transform, 3f));
                    });
                rowTween.OnComplete(() => { rowEnemy.SetActive(false); });
                yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator CreatEnemy2_2() {
        Vector3 rightCenter = new Vector3(Boundary.xMin + 0.6f * Boundary.weight, Boundary.yMin + 0.75f * Boundary.height);
        List<Vector3> rightVectors = new List<Vector3> {
            rightCenter + new Vector3(-1f,0),
            rightCenter + new Vector3(0,0.7f),
            rightCenter + new Vector3(1f,0),
            rightCenter + new Vector3(-0.5f,-0.7f),
            rightCenter + new Vector3(0.5f,-0.7f)
        };
        Vector3 rightSpawnPos = Boundary.ViewPortRightTop + new Vector3(2f, 2f);
        for (int i = 0; i < rightVectors.Count; i++) {
            GameObject rightEnemy = PoolManager.Release(YellowHyperEnemy, rightSpawnPos);
            EnemyManager.Instance.AddEnemy(rightEnemy);
            rightEnemy.GetComponent<Enemy>().Initialize(4, 30);
            rightEnemy.transform.DOMove(rightVectors[i], 2f).OnComplete(() => {
                StartCoroutine(CreatRoundTowardsCoroutine(rightEnemy.transform, 0.5f));
            });
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator CreatEnemy2_3() {
        Vector3 leftCenter = new Vector3(Boundary.xMin + 0.4f * Boundary.weight, Boundary.yMin + 0.75f * Boundary.height);
        List<Vector3> leftVectors = new List<Vector3> {
            leftCenter + new Vector3(-1f,0),
            leftCenter + new Vector3(0,0.7f),
            leftCenter + new Vector3(1f,0),
            leftCenter + new Vector3(-0.5f,-0.7f),
            leftCenter + new Vector3(0.5f,-0.7f)
        };
        Vector3 rightSpawnPos = Boundary.ViewPortLeftTop - new Vector3(2f, 2f);
        for (int i = 0; i < leftVectors.Count; i++) {
            GameObject leftEnemy = PoolManager.Release(YellowHyperEnemy, rightSpawnPos);
            EnemyManager.Instance.AddEnemy(leftEnemy);
            leftEnemy.GetComponent<Enemy>().Initialize(4, 30);
            leftEnemy.transform.DOMove(leftVectors[i], 2f).OnComplete(() => {
                StartCoroutine(CreatRoundTowardsCoroutine(leftEnemy.transform, 0.5f));
            });
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator CreatEnemy2() {
        StartCoroutine(CreatEnemy2_1());
        yield return new WaitForSeconds(5f);
        StartCoroutine(CreatEnemy2_2());
        yield return new WaitForSeconds(5f);
        StartCoroutine(CreatEnemy2_3());
        yield return new WaitForSeconds(12f);
        Debug.Log("Enemy2 over");
        StartCoroutine(CreatEnemy3());
    }
    IEnumerator CreatEnemy3_1() {
        Vector3[] pos = { Boundary.ViewPortRightTop + new Vector3(1f, 1f, 0f),
            Boundary.ViewPortMid+new Vector3(2f,0,0),
            Boundary.ViewPortLeftBottom+new Vector3(-1f,2f,0)
        };
        for (int i = 0; i < 4; i++) {
            GameObject tmpenemy = PoolManager.Release(RedNormalEnemy, pos[0]);
            EnemyManager.Instance.AddEnemy(tmpenemy);
            tmpenemy.GetComponent<Enemy>().Initialize(5, 20);
            Tween enemyTween = tmpenemy.transform.DOPath(pos, 15, PathType.CatmullRom, PathMode.Sidescroller2D, 50).OnStart(() => {
                //Create Danmu here
                StartCoroutine(CreatTowardsCoroutine(tmpenemy.transform, 2f));
            });
            enemyTween.OnComplete(() => {
                tmpenemy.SetActive(false);
            });
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    IEnumerator CreatEnemy3_2_1() {
        Vector3[] pos = { Boundary.ViewPortRightTop + new Vector3(-0.25f * Boundary.weight, 1f, 0f),
            Boundary.ViewPortMid+new Vector3(2f,3f,0),
            Boundary.ViewPortLeftBottom+new Vector3(-1f,-1f,0)
        };
        for (int i = 0; i < 4; i++) {
            GameObject tmpenemy = PoolManager.Release(RedNormalEnemy, pos[0]);
            EnemyManager.Instance.AddEnemy(tmpenemy);
            tmpenemy.GetComponent<Enemy>().Initialize(3, 20);
            Tween enemyTween = tmpenemy.transform.DOPath(pos, 10, PathType.CatmullRom, PathMode.Sidescroller2D, 50).OnStart(() => {
                //Create Danmu here
                StartCoroutine(CreatTowardsCoroutine(tmpenemy.transform, 2f));
            });
            enemyTween.OnComplete(() => {
                tmpenemy.SetActive(false);
            });
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    IEnumerator CreatEnemy3_2_2() {
        Vector3[] pos = { Boundary.ViewPortLeftTop + new Vector3(0.25f * Boundary.weight, 1f, 0f),
            Boundary.ViewPortMid+new Vector3(-2f,3f,0),
            Boundary.ViewPortRightBottom+new Vector3(1f,-1f,0)
        };
        for (int i = 0; i < 4; i++) {
            GameObject tmpenemy = PoolManager.Release(RedNormalEnemy, pos[0]);
            EnemyManager.Instance.AddEnemy(tmpenemy);
            tmpenemy.GetComponent<Enemy>().Initialize(3, 20);
            Tween enemyTween = tmpenemy.transform.DOPath(pos, 10, PathType.CatmullRom, PathMode.Sidescroller2D, 50).OnStart(() => {
                //Create Danmu here
                StartCoroutine(CreatTowardsCoroutine(tmpenemy.transform, 2f));
            });
            enemyTween.OnComplete(() => {
                tmpenemy.SetActive(false);
            });
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    IEnumerator CreatEnemy3_3() {
        //TODO:通过工厂模式获取敌机对象，实现random的获取方式
        Vector3[] pos = { Boundary.ViewPortMid + new Vector3(-0.5f * Boundary.weight - 1f, 4f, 0f),
            Boundary.ViewPortMid,
            Boundary.ViewPortMid + new Vector3(0.5f * Boundary.weight + 1f, 1f, 0f)
        };
        for (int i = 0; i < 4; i++) {
            GameObject tmpenemy = PoolManager.Release(RedNormalEnemy, pos[0]);
            EnemyManager.Instance.AddEnemy(tmpenemy);
            tmpenemy.GetComponent<Enemy>().Initialize(5, 20);
            Tween enemyTween = tmpenemy.transform.DOPath(pos, 10, PathType.CatmullRom, PathMode.Sidescroller2D, 50).OnStart(() => {
                //Create Danmu here
                StartCoroutine(CreatTowardsCoroutine(tmpenemy.transform, 2f));
            });
            enemyTween.OnComplete(() => {
                tmpenemy.SetActive(false);
            });
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    IEnumerator CreatEnemy3() {
#if UNITY_EDITOR
        Debug.Log("Enemy3 Start.");
#endif
        yield return new WaitForSeconds(1f);
        StartCoroutine(CreatEnemy3_1());
        yield return new WaitForSeconds(6f);
        StartCoroutine(CreatEnemy3_2_1());
        StartCoroutine(CreatEnemy3_2_2());
        yield return new WaitForSeconds(6f);
        StartCoroutine(CreatEnemy3_3());

        yield return new WaitUntil(() => enemyExist != true);
#if UNITY_EDITOR
        Debug.Log("Enemy3 Tween Over.");
#endif
        StartCoroutine(nameof(CreatEnemy4));
    }
    IEnumerator CreatEnemy4() {
        sequence = DOTween.Sequence();
        Vector3[] pos1 = { Boundary.ViewPortLeftTop+new Vector3(-1f,1f,0f),
            Boundary.ViewPortMid,
            Boundary.ViewPortRightTop+ new Vector3(1f, 1f, 0f) };
        Vector3[] pos2 = { Boundary.ViewPortRightTop+ new Vector3(1f, 1f, 0f),
            Boundary.ViewPortMid,
            Boundary.ViewPortLeftTop+new Vector3(-1f,1f,0f)};
        for (int i = 0; i < 10; i++) {
            GameObject tmpship = PoolManager.Release(RedNormalEnemy, pos1[0]);
            EnemyManager.Instance.AddEnemy(tmpship);
            tmpship.GetComponent<Enemy>().Initialize(2, 5);
            Tween tmpshipTween = tmpship.transform.DOPath(pos1, 10, PathType.CatmullRom, PathMode.Sidescroller2D, 50).OnStart(() => {
                CreatTowardsPlayerDanmuAt(tmpship.transform);
            });
            tmpshipTween.OnComplete(() => {
                tmpship.SetActive(false);
            });
            yield return new WaitForSeconds(0.5f);
            CreatTowardsPlayerDanmuAt(tmpship.transform);
        }
        for (int i = 0; i < 10; i++) {
            GameObject tmpship = PoolManager.Release(YellowNormalEnemy, pos2[0]);
            EnemyManager.Instance.AddEnemy(tmpship);
            tmpship.GetComponent<Enemy>().Initialize(2, 5);
            Tween tmpshipTween = tmpship.transform.DOPath(pos2, 10, PathType.CatmullRom, PathMode.Sidescroller2D, 50).OnStart(() => {
                CreatTowardsPlayerDanmuAt(tmpship.transform);
            });
            tmpshipTween.OnComplete(() => {
                tmpship.SetActive(false);
            });
            yield return new WaitForSeconds(0.5f);
            CreatTowardsPlayerDanmuAt(tmpship.transform);
        }
        //GameObject[] arrEnemys = { RedNormalEnemy, BlueNormalEnemy, GreenNormalEnemy, YellowNormalEnemy };
        //for (int i = 0; i < 4; i++) {
        //    int nums = Random.Range(4, 7);
        //    for (int j = 0; j < nums; j++) {
        //        GameObject tmpship = PoolManager.Release(arrEnemys[j % 4], GameObject.Find("Square").transform.position);
        //        EnemyManager.Instance.AddEnemy(tmpship);
        //        tmpship.GetComponent<Enemy>().Initialize(3, 10);
        //        Vector3 spawn = Boundary.ViewPortMid + new Vector3(Random.Range(-4f, 4f), Random.Range(-1f, 1f), 0f);
        //        tmpship.transform.DOMove(spawn, 4);
        //        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        //    }
        //}
        //yield return new WaitUntil(() => enemyExist != true);
        CreatEnemy5_1();
    }
    void CreatEnemy5_1() {
        GameObject shipl = PoolManager.Release(butterfly, EnvironmentObjectsManager.Instance.MarkObjectSquare.transform.position);
        GameObject shipr = PoolManager.Release(butterfly, EnvironmentObjectsManager.Instance.MarkObjectSquare.transform.position);
        EnemyManager.Instance.AddEnemy(shipl);
        EnemyManager.Instance.AddEnemy(shipr);
        shipl.GetComponent<Enemy>().Initialize(30, 50);
        shipr.GetComponent<Enemy>().Initialize(30, 50);
        sequence = DOTween.Sequence();
        Tween shiplTween = shipl.transform.DOMove(new Vector3(Boundary.xMax - 2f, 2, 0), 2);
        Tween shiprTween = shipr.transform.DOMove(new Vector3(Boundary.xMin + 2f, 2, 0), 2);
        sequence.Append(shiplTween);
        sequence.Join(shiprTween);
        sequence.OnComplete(() => {
            StartCoroutine(CreatEnemy5_2(shipl, shipr));
        });
    }
    IEnumerator CreatEnemy5_2(GameObject shipl, GameObject shipr) {
        float roundRate = 1.5f, roundTimer = 0f;
        Debug.Log("开始发射");
        while (enemyExist) {
            roundTimer += Time.fixedDeltaTime;
            if (roundTimer >= roundRate) {
                roundTimer = 0f;
                StartCoroutine(CreatBigTowardsPlayerDamuAt(shipl.transform));
                StartCoroutine(CreatBigTowardsPlayerDamuAt(shipr.transform));
                Debug.Log("发射");
            }
            yield return new WaitUntil(() => enemyExist == true);
        };
        yield return new WaitUntil(() => enemyExist != true);
        StartCoroutine(CreatEnemy6());
    }
    IEnumerator CreatEnemy6_1() {
        Vector3 spwanPosLeft = Boundary.ViewPortLeftBottom + new Vector3(-1f, 0.8f * Boundary.height);
        for (int i = 0; i < 10; i++) {
            GameObject enemyLeft = EnemyFactory.Instance.GetBlueNormalEnemy();
            enemyLeft.GetComponent<Enemy>().Initialize(1, 10);
            Vector3 randVec = Random.insideUnitCircle;
            enemyLeft.transform.position = spwanPosLeft + randVec;
            enemyLeft.transform.DOMoveX(Boundary.xMax + 2f, 13f).OnComplete(() => {
                enemyLeft.SetActive(false);
            });
            yield return new WaitForSeconds(0.4f);
        }
        yield return null;
    }
    IEnumerator CreatEnemy6_2() {
        Vector3 spwanPosRight = Boundary.ViewPortRightBottom + new Vector3(1f, 0.8f * Boundary.height);
        for (int i = 0; i < 10; i++) {
            GameObject enemyLeft = EnemyFactory.Instance.GetBlueNormalEnemy();
            enemyLeft.GetComponent<Enemy>().Initialize(1, 10);
            Vector3 randVec = Random.insideUnitCircle;
            enemyLeft.transform.position = spwanPosRight + randVec;
            enemyLeft.transform.DOMoveX(Boundary.xMin - 2f, 13f).OnComplete(() => {
                enemyLeft.SetActive(false);
            });
            yield return new WaitForSeconds(0.4f);
        }
        yield return null;
    }
    IEnumerator DoubleRoundWithTowardsCoroutine(Transform transform) {
        while (transform.gameObject.activeSelf) {
            Vector3 vecUp = EnvironmentObjectsManager.Instance.PlayerObject.transform.position - transform.position;
            for (int i = 0; i < 7 && gameObject.activeSelf; i++) {
                GameObject danmuL = DanmuFactory.Instance.GetWhiteDiamondDanmu();
                GameObject danmuR = DanmuFactory.Instance.GetWhiteDiamondDanmu();
                danmuL.GetComponent<Danmu>().SetSpeed(5);
                danmuR.GetComponent<Danmu>().SetSpeed(5);
                danmuL.transform.up = vecUp;
                danmuR.transform.up = vecUp;
                danmuL.transform.position = transform.position;
                danmuR.transform.position = transform.position;
                danmuL.transform.position = transform.position + danmuL.transform.right.normalized * 0.2f;
                danmuR.transform.position = transform.position - danmuR.transform.right.normalized * 0.2f;
                danmuL.transform.up = vecUp;
                danmuR.transform.up = vecUp;
                AudioControl.Instance.PlayBossRayShot();
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 0; i < 36 && transform.gameObject.activeSelf; i++) {
                GameObject roundDanmu = DanmuFactory.Instance.GetWhiteDiamondDanmu();
                roundDanmu.transform.position = transform.position;
                roundDanmu.transform.rotation = transform.rotation;
                roundDanmu.transform.Rotate(transform.forward * 10 * i);
                roundDanmu.GetComponent<Danmu>().SetSpeed(2f);
                AudioControl.Instance.PlayBossTan02();
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 36 && transform.gameObject.activeSelf; i++) {
                GameObject roundDanmu = DanmuFactory.Instance.GetWhiteDiamondDanmu();
                roundDanmu.transform.position = transform.position;
                roundDanmu.transform.rotation = transform.rotation;
                roundDanmu.transform.Rotate(transform.forward * (10 * i + 10));
                roundDanmu.GetComponent<Danmu>().SetSpeed(2f);
                AudioControl.Instance.PlayBossTan02();
            }
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }
    IEnumerator CreatEnemy6_3() {
        GameObject enemyLeft = PoolManager.Release(YellowHyperEnemy, EnvironmentObjectsManager.Instance.MarkObjectSquare.transform.position);
        GameObject enemyRight = PoolManager.Release(YellowHyperEnemy, EnvironmentObjectsManager.Instance.MarkObjectSquare.transform.position);
        EnemyManager.Instance.AddEnemy(enemyLeft);
        EnemyManager.Instance.AddEnemy(enemyRight);
        enemyLeft.GetComponent<Enemy>().Initialize(30, 50);
        enemyRight.GetComponent<Enemy>().Initialize(30, 50);
        Tween shiplTween = enemyLeft.transform.DOMove(new Vector3(Boundary.xMax - 2f, 2, 0), 2);
        Tween shiprTween = enemyRight.transform.DOMove(new Vector3(Boundary.xMin + 2f, 2, 0), 2);
        yield return shiplTween.WaitForCompletion();
        StartCoroutine(DoubleRoundWithTowardsCoroutine(enemyLeft.transform));
        StartCoroutine(DoubleRoundWithTowardsCoroutine(enemyRight.transform));
    }
    IEnumerator CreatEnemy6() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(CreatEnemy6_1());
        StartCoroutine(CreatEnemy6_2());
        yield return new WaitForSeconds(3f);
        StartCoroutine(CreatEnemy6_3());
        yield return new WaitUntil(() => enemyExist != true);
        Stop();
    }
}
