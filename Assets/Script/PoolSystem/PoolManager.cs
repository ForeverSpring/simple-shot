using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] BulletPool;
    void Start() {
        Initialize(BulletPool);
    }

    void Initialize(Pool[] pools) {
        foreach(var pool in pools) {
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }
}
