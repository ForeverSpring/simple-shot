using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[System.Serializable]
public class LootSetting
{
    public GameObject prefab;
    [Range(0f, 100f)] public float dropPercentage;
    public int Times;
    public void Spawn(Vector2 position) {
        for(int i=0;i<Times;i++)
            if (Random.Range(0f, 100f) <= dropPercentage) {
                GameObject item = PoolManager.Release(prefab, position);
                Vector3 RandomPos = position + Random.insideUnitCircle;
                RandomPos.x = Mathf.Clamp(RandomPos.x, Boundary.xMin, Boundary.xMax);
                item.transform.DORotate(new Vector3(0, 0, 720), 1f, RotateMode.WorldAxisAdd);
                item.transform.DOMove(RandomPos, 1f);
            }
    }
}
