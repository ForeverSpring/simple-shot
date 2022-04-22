using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePoint : LootItem {
    protected override void Awake() {
        base.Awake();
        this.mItemType = LootItemType.BluePoint;
    }

    protected override void PickUp() {
        GameData.Instance.addScore(100);
        this.gameObject.SetActive(false);
    }
}
