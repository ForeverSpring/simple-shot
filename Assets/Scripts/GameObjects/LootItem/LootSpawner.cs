using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] public LootSetting[] lootSettings;
    public void Spawn(Vector2 position) {
        foreach(var item in lootSettings) {
            item.Spawn(position);
        }
    }
    public void SpawnLocal() {
        Spawn(this.transform.position);
    }
}
