using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LootItem : MonoBehaviour {
    [SerializeField] float speed = 2f, autoSpeed = 10f;
    [SerializeField] public bool autoReceive;
    public LootItemType mItemType;
    Rigidbody2D rb;
    protected GameObject playerObj => EnvironmentObjectsManager.Instance.PlayerObject;
    public enum LootItemType {
        RedPoint,RedPointBig,BluePoint,LifePoint,SpellPoint,GreenPointLittle
    };
    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        autoReceive = false;
    }
    protected virtual void PickUp() { }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PickUp();
            AudioControl.Instance.PlayGetItem();
        }
    }
    private void OnDisable() {
        autoReceive = false;
        transform.up = new Vector2(0, 1);
    }
    private void Update() {
        CheckPlayerDistance();
    }
    void CheckPlayerDistance() {
        if (playerObj.activeSelf && (playerObj.transform.position - rb.transform.position).magnitude <= 1.5) {
            this.transform.DOKill();
            autoReceive = true;
        }
    }
    private void FixedUpdate() {
        if (playerObj.activeSelf && autoReceive) {
            transform.position += (playerObj.transform.position - rb.transform.position).normalized * autoSpeed * Time.fixedDeltaTime;
        }
        else {
            transform.position = transform.position + (-transform.up * speed * Time.fixedDeltaTime);
        }

    }
}
