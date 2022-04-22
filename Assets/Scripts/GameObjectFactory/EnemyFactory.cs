using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : Singleton<EnemyFactory>
{
    [Header("Boss")]
    public GameObject BullBoss;
    public GameObject ZakuBoss;
    [Header("飞船敌机")]
    public GameObject ship1Enemy;
    public GameObject ship2Enemy;
    [Header("蝴蝶敌机")]
    public GameObject butterflyEnemy;
    [Header("Normal敌机")]
    public GameObject redNormalEnemy;
    public GameObject blueNormalEnemy;
    public GameObject greenNormalEnemy;
    public GameObject yellowNormalEnemy;
    [Header("Hyper敌机")]
    public GameObject redHyperEnemy;
    public GameObject blueHyperEnemy;
    public GameObject greenHyperEnemy;
    public GameObject yellowHyperEnemy;
    void Start(){
        Debug.Log("Enemy GameObject Ready.");
    }
    public GameObject GetShip1Enemy() {
        GameObject ret = PoolManager.Release(ship1Enemy);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetShip2Enemy() {
        GameObject ret = PoolManager.Release(ship2Enemy);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetButterflyEnemy() {
        GameObject ret = PoolManager.Release(butterflyEnemy);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetRedNormalEnemy() {
        GameObject ret = PoolManager.Release(redNormalEnemy);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetBlueNormalEnemy() {
        GameObject ret = PoolManager.Release(blueNormalEnemy);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetGreenNormalEnemy() {
        GameObject ret = PoolManager.Release(greenNormalEnemy);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetYellowNormalEnemy() {
        GameObject ret = PoolManager.Release(yellowNormalEnemy);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetBullBoss() {
        GameObject ret = PoolManager.Release(BullBoss);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
    public GameObject GetZakuBoss() {
        GameObject ret = PoolManager.Release(ZakuBoss);
        EnemyManager.Instance.AddEnemy(ret);
        return ret;
    }
}
