using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Midway : Fuka {
    GameObject ship;
    Sequence sequence;
    bool enemyExist;
    private void Start() {
        fukaName = "stage1midway";
        fukaType = FukaType.TimeFuka;
        ship = (GameObject)Resources.Load("Prefab/Enemy/ship1");
        sequence = DOTween.Sequence();
    }

    private void Update() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0) {
            enemyExist = true;
        }
        else {
            enemyExist = false;
        }
    }

    public override void Run() {
        GameControl.Instance.WaitFuka();
        if (gameobjBoss.activeSelf) {
            gameobjBoss.SetActive(false);
        }
        GameUIControl.Instance.SetTopSlideVisiable(false);
        GameUIControl.Instance.FukaNameStart(fukaName);
        CreatEnemy1();
        //CreatEnemy5_1();
    }

    public override void Stop() {
        GameControl.Instance.SignalFuka();
    }

    public void DeleteAllEnemy() {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in arr) {
            enemy.SetActive(false);
        }
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
        GameObject bullet = DanmuFactory.Instance.Getfireball_red_tail_big();
        bullet.transform.position = transform.position;
        bullet.transform.up = GameObject.Find("Player").transform.position - bullet.transform.position;
        bullet.GetComponent<Danmu2D>().moveSpeed = 6f;
    }

    void CreatBigTowardsPlayerDamuAt(Transform transform) {
        if (!transform.gameObject.activeSelf)
            return;
        float left_len = 0.5f, up_len = 0.5f;
        Vector3 vecUp = GameObject.Find("Player").transform.position - transform.position;
        for (int i = 0; i < 3; i++) {
            float mid = (2 - (float)i) / 2;
            for (int j = 0; j < 3 - i; j++) {
                GameObject bullet = DanmuFactory.Instance.Getfireball_red_tail_big();
                bullet.transform.up = vecUp;
                bullet.transform.position = transform.position + left_len * (mid - j) * bullet.transform.right.normalized + up_len * i * bullet.transform.up.normalized;
                bullet.transform.up = vecUp;
            }
        }
    }

    void CreatEnemy1() {
        GameObject ship1 = PoolManager.Release(ship, GameObject.Find("Square").transform.position);
        sequence.Insert(0f, ship1.transform.DOMove(new Vector3(Boundary.xMin + 2f, 2, 0), 2).SetSpeedBased());
        float roundRate = 1.5f, roundTimer = 1f;
        Tween t = ship1.transform.DOMoveY(Boundary.yMin - 1f, 7).SetSpeedBased();
        t.OnUpdate(() => {
            roundTimer += Time.fixedDeltaTime;
            if (roundTimer >= roundRate) {
                roundTimer = 0f;
                CreatRoundDanmuAt(ship1.transform);
            }
        });
        t.OnComplete(() => {
#if UNITY_EDITOR
            Debug.Log("Enemy1 Tween Over.");
#endif
            ship1.SetActive(false);
            sequence = DOTween.Sequence();
            CreatEnemy2();
        });
        sequence.Append(t);
        sequence.PrependInterval(2f);
    }

    void CreatEnemy2() {
        GameObject shipl = PoolManager.Release(ship, GameObject.Find("Square").transform.position);
        GameObject shipr = PoolManager.Release(ship, GameObject.Find("Square").transform.position);
        sequence = DOTween.Sequence();
        Tween shiplTween = shipl.transform.DOMove(new Vector3(Boundary.xMax - 2f, 2, 0), 2);
        Tween shiprTween = shipr.transform.DOMove(new Vector3(Boundary.xMin + 2f, 2, 0), 2);
        shiplTween.OnComplete(() => {
#if UNITY_EDITOR
            Debug.Log("Enemy2 Move Over.");
#endif
            CreatEnemy2_2(shipl, shipr);
        });
        //先移动到相应位置
        sequence.Insert(0, shiplTween);
        sequence.Insert(0, shiprTween);
    }

    void CreatEnemy2_2(GameObject shipl, GameObject shipr) {
        Debug.Log("开始发射");
        sequence = DOTween.Sequence();
        float roundRate = 1.5f, roundTimer = 0f;
        Tween doubleShipTween = GameObject.Find("Square").GetComponent<Text>().DOColor(Color.black, 10);
        //移动到位置后开始发射
        doubleShipTween.OnUpdate(() => {
            roundTimer += Time.fixedDeltaTime;
            if (roundTimer >= roundRate) {
                roundTimer = 0f;
                CreatRoundDanmuAt(shipl.transform);
                CreatRoundDanmuAt(shipr.transform);
            }
        });
        sequence.Append(doubleShipTween);
        //发射完或被击破后播放动画
        doubleShipTween.OnComplete(() => {
#if UNITY_EDITOR
            Debug.Log("Enemy2 Shot Over.");
#endif
            CreatEnemy2_3(shipl, shipr);
        });
    }

    void CreatEnemy2_3(GameObject shipl, GameObject shipr) {
        sequence = DOTween.Sequence();
        sequence.Append(shipl.transform.DOMove(GameObject.Find("Square").transform.position, 2));
        sequence.Join(shipr.transform.DOMove(GameObject.Find("Square").transform.position, 2));
        sequence.OnComplete(() => {
            shipl.SetActive(false);
            shipr.SetActive(false);
            StartCoroutine(nameof(CreatEnemy3_1));
        });
    }

    IEnumerator CreatEnemy3_1() {
#if UNITY_EDITOR
        Debug.Log("Enemy3 Start.");
#endif
        sequence = DOTween.Sequence();
        Vector3[] pos1 = { Boundary.ViewPortLeftTop+new Vector3(-1f,1f,0f),
            Boundary.ViewPortMid,
            Boundary.ViewPortRightTop+ new Vector3(1f, 1f, 0f) };
        Vector3[] pos2 = { Boundary.ViewPortRightTop+ new Vector3(1f, 1f, 0f),
            Boundary.ViewPortMid,
            Boundary.ViewPortLeftTop+new Vector3(-1f,1f,0f)};
        for (int i = 0; i < 10; i++) {
            GameObject tmpship = PoolManager.Release(ship, pos1[0]);
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
            GameObject tmpship = PoolManager.Release(ship, pos2[0]);
            Tween tmpshipTween = tmpship.transform.DOPath(pos2, 10, PathType.CatmullRom, PathMode.Sidescroller2D, 50).OnStart(() => {
                CreatTowardsPlayerDanmuAt(tmpship.transform);
            });
            tmpshipTween.OnComplete(() => {
                tmpship.SetActive(false);
            });
            yield return new WaitForSeconds(0.5f);
            CreatTowardsPlayerDanmuAt(tmpship.transform);
        }
        yield return new WaitUntil(() => enemyExist != true);
#if UNITY_EDITOR
        Debug.Log("Enemy3 Tween Over.");
#endif
        StartCoroutine(nameof(CreatEnemy4));
    }

    IEnumerator CreatEnemy4() {
        for (int i = 0; i < 4; i++) {
            int nums = Random.Range(4, 7);
            for (int j = 0; j < nums; j++) {
                GameObject tmpship = PoolManager.Release(ship, GameObject.Find("Square").transform.position);
                Vector3 spawn = Boundary.ViewPortMid + new Vector3(Random.Range(-4f, 4f), Random.Range(-1f, 1f), 0f);
                tmpship.transform.DOMove(spawn, 4);
                yield return new WaitForSeconds(Random.Range(0f, 0.5f));
            }
        }
        yield return new WaitUntil(() => enemyExist != true);
        CreatEnemy5_1();
    }
    void CreatEnemy5_1() {
        GameObject shipl = PoolManager.Release(ship, GameObject.Find("Square").transform.position);
        GameObject shipr = PoolManager.Release(ship, GameObject.Find("Square").transform.position);
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
                CreatBigTowardsPlayerDamuAt(shipl.transform);
                CreatBigTowardsPlayerDamuAt(shipr.transform);
                Debug.Log("发射");
            }
            yield return new WaitUntil(() => enemyExist == true);
        };
        yield return new WaitUntil(() => enemyExist != true);
        Stop();
    }
}
