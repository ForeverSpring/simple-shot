using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoint : LootItem {
    protected override void Awake() {
        base.Awake();
        this.mItemType = LootItemType.LifePoint;
    }

    protected override void PickUp() {
        GameData.Instance.addPlayer();
        this.gameObject.SetActive(false);
    }
}
