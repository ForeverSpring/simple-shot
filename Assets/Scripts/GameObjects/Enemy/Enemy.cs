using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] int life = 10;
    [SerializeField] int score = 10;
    [SerializeField] public GameObject deadVfx;
    LootSpawner lootSpawner;
    protected virtual void Awake() {
        lootSpawner = GetComponent<LootSpawner>();
    }
    public void Initialize(int life,int score) {
        this.life = life;
        this.score = score;
    }
    public void SetLife(int life) {
        this.life = life;
    }

    public void GetInjured(int injuredLife) {
        if (injuredLife >= life) {
            Dead();
            return;
        }
        else
            life -= injuredLife;
    }

    public void Dead() {
        if (GameData.Instance == null)
            Debug.LogWarning("未找到游戏数据对象");
        else
            GameData.Instance.addScore(this.score);
        lootSpawner.Spawn(gameObject.transform.position);
        PoolManager.Release(deadVfx, transform.position);
        EnemyManager.Instance.RemoveEnemy(this.gameObject);
        AudioControl.Instance.PlayEnemyDead();
        this.gameObject.transform.DOComplete();
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            other.gameObject.SetActive(false);
            GetInjured(1);
            AudioControl.Instance.PlayBulletHurtEnemy();
            if (GameData.Instance == null)
                Debug.LogWarning("未找到游戏数据对象");
            else
                GameData.Instance.addScore(1);
        }
    }

}
