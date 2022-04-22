using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPoint : LootItem {
    protected override void Awake() {
        base.Awake();
        this.mItemType = LootItemType.RedPoint;
    }

    protected override void PickUp() {
        GameData.Instance.addScore(100);
        GameData.Instance.addPower(0.05f);
        this.gameObject.SetActive(false);
    }
}
