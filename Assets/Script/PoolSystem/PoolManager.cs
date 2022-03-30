using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    [SerializeField] Pool[] BulletPool;
    [SerializeField] Pool[] EnemyPool;

    static Dictionary<GameObject, Pool> dictionary;
    void Start() {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(BulletPool);
        Initialize(EnemyPool);
    }
#if UNITY_EDITOR
    private void OnDestroy() {
        CheckPoolSize(BulletPool);
        CheckPoolSize(EnemyPool);
    }
#endif
    void CheckPoolSize(Pool[] pools) {
        foreach (var pool in pools) {
            if (pool.RuntimeSize > pool.Size) {
                Debug.LogWarning(
                    string.Format("����{0}�����гߴ�{1}���ڳ�ʼ��ʱ�ߴ�{2}.", pool.Prefab.name, pool.RuntimeSize, pool.Size));
            }
        }
    }

    void Initialize(Pool[] pools) {
        foreach (var pool in pools) {
#if UNITY_EDITOR
            if (dictionary.ContainsKey(pool.Prefab)) {
                Debug.Log("Ԥ�����ظ���" + pool.Prefab.name);
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
            Debug.LogError("δ�ҵ�Ԥ���壺" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].preparedObject();
    }

    public static GameObject Release(GameObject prefab, Vector3 position) {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab)) {
            Debug.LogError("δ�ҵ�Ԥ���壺" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].preparedObject(position);
    }
}
