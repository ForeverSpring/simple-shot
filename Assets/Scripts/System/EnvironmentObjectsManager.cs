using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjectsManager : Singleton<EnvironmentObjectsManager>
{
    [SerializeField] public GameObject BossObject;
    [SerializeField] public GameObject PlayerObject;
    [SerializeField] public GameObject MarkObjectSquare;
    [SerializeField] public Camera mainCamera;
    public GameObject GreenPointLittle;
    private void Update() {
        CheckPlayerAuto();
    }
    void CheckPlayerAuto() {
        if (PlayerObject != null && PlayerObject.transform.position.y >= 2.5f) {
            LootItemsAllAuto();
        }
    }
    public void SetBoss(GameObject boss) {
        BossObject = boss;
    }
    public void ClearDanmu() {
        Debug.Log("clear screen danmu.");
        GameObject[] danmus = GameObject.FindGameObjectsWithTag("Danmu");
        foreach (GameObject danmu in danmus) {
            danmu.SetActive(false);
            PoolManager.Release(GreenPointLittle, danmu.transform.position).GetComponent<LootItem>().autoReceive = true;
        }
    }
    public void ClearBullet() {
        Debug.Log("clear screen bullet.");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            bullet.SetActive(false);
        }
    }
    public void LootItemsAllAuto() {
        Debug.Log("clear screen lootitem.");
        GameObject[] items = GameObject.FindGameObjectsWithTag("LootItem");
        foreach (GameObject item in items) {
            item.GetComponent<LootItem>().autoReceive = true;
        }
    }
}
