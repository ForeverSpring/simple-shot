using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBulletCleaner : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Bullet") {
            other.gameObject.SetActive(false);
            return;
        }
    }
}
