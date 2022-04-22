using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPointLittle : LootItem {
    protected override void Awake() {
        base.Awake();
        this.mItemType = LootItemType.GreenPointLittle;
    }

    protected override void PickUp() {
        GameData.Instance.addScore(10);
        this.gameObject.SetActive(false);
    }
}
