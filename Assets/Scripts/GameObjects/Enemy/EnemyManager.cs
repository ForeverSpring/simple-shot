using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {
    [SerializeField] List<GameObject> arrEnemy = new List<GameObject>();
    public List<GameObject> GetAllEnemys => arrEnemy;
    public bool EnemyExist => arrEnemy.Count != 0;
    public void AddEnemy(GameObject enemy) {
        if (enemy.tag != "Enemy" && enemy.tag != "Boss") {
            Debug.LogWarning("Invalid enemy object to add in EnemyManager, Tag Error.");
            return;
        }
        arrEnemy.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy) {
        arrEnemy.Remove(enemy);
    }
    public GameObject GetRandomEnemy() {
        if (arrEnemy.Count == 0)
            return null;
        int index = Random.Range(0, arrEnemy.Count);
        return arrEnemy[index];
    }

}
