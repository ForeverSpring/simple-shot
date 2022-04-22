using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCleaner : MonoBehaviour {
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Danmu" || other.tag == "LootItem") {
            other.gameObject.SetActive(false);
            return;
        }
        if (other.tag == "Enemy") {
            EnemyManager.Instance.RemoveEnemy(other.gameObject);
            other.gameObject.SetActive(false);
            return;
        }
    }
}
