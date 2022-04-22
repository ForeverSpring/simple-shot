using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPointBig : LootItem {
    protected override void Awake() {
        base.Awake();
        this.mItemType = LootItemType.RedPointBig;
    }

    protected override void PickUp() {
        GameData.Instance.addScore(500);
        GameData.Instance.addPower(0.5f);
        this.gameObject.SetActive(false);
    }
}
