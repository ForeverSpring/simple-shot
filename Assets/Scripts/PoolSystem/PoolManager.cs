using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    [SerializeField] Pool[] DanmuPool;
    [SerializeField] Pool[] EnemyPool;
    [SerializeField] Pool[] BulletPool;
    [SerializeField] Pool[] VfxPool;
    [SerializeField] Pool[] LootItemPool;
    [SerializeField] Pool[] BossPool;
    static Dictionary<GameObject, Pool> dictionary;
    void Start() {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(DanmuPool);
        Initialize(EnemyPool);
        Initialize(BulletPool);
        Initialize(VfxPool);
        Initialize(LootItemPool);
        Initialize(BossPool);
    }
#if UNITY_EDITOR
    private void OnDestroy() {
        CheckPoolSize(DanmuPool);
        CheckPoolSize(EnemyPool);
        CheckPoolSize(BulletPool);
        CheckPoolSize(VfxPool);
        CheckPoolSize(LootItemPool);
        CheckPoolSize(BossPool);
    }
#endif
    void CheckPoolSize(Pool[] pools) {
        foreach (var pool in pools) {
            if (pool.RuntimeSize > pool.Size) {
                Debug.LogWarning(
                    string.Format("对象{0}池运行尺寸{1}大于初始化时尺寸{2}.", pool.Prefab.name, pool.RuntimeSize, pool.Size));
            }
        }
    }

    void Initialize(Pool[] pools) {
        foreach (var pool in pools) {
#if UNITY_EDITOR
            if (dictionary.ContainsKey(pool.Prefab)) {
                Debug.Log("预制体重复：" + pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    public static GameObject Release(GameObject prefab) {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab)) {
            Debug.LogError("未找到预制体：" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].preparedObject();
    }

    public static GameObject Release(GameObject prefab, Vector3 position) {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab)) {
            Debug.LogError("未找到预制体：" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].preparedObject(position);
    }
}
