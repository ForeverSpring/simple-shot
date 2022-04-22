using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPoint : LootItem {
    protected override void Awake() {
        base.Awake();
        this.mItemType = LootItemType.SpellPoint;
    }

    protected override void PickUp() {
        GameData.Instance.addSpell();
        this.gameObject.SetActive(false);
    }
}
