using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DecisionPoint : MonoBehaviour {
    //被弹幕击中触发着弹
    private bool CanBeShot = true;
    public float SeedTime = 0.4f;
    public float TimeMuteki = 3f;
    protected PlayerController player;
    public Renderer DicisionPointRender;
    public GameObject playerDeadVFX;
    [SerializeField] public float ShineSpeed = 0.5f;
    SpriteRenderer playerSprite;
    Tween tweenMuteki; 
    LootSpawner spawner;
    private void Awake() {
        playerSprite = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
        spawner = GetComponent<LootSpawner>();
    }
    void PlayerSpriteToBlue() {
        if (!CanBeShot)
            tweenMuteki = playerSprite.DOColor(Color.blue, ShineSpeed).OnComplete(PlayerSpriteToWhite);
        else {
            tweenMuteki.Complete();
            playerSprite.color = Color.white;
        }
    }
    void PlayerSpriteToWhite() {
        if (!CanBeShot)
            tweenMuteki = playerSprite.DOColor(Color.white, ShineSpeed).OnComplete(PlayerSpriteToBlue);
        else {
            tweenMuteki.Complete();
            playerSprite.color = Color.white;
        }
    }
    public bool isMuteki() {
        return !CanBeShot;
    }
    //外部调用无敌时间
    public void SetMutekiTime(float MutekiTime) {
        StartCoroutine(FlagChange(MutekiTime));
    }
    //无敌时间内不可继续着弹
    IEnumerator FlagChange(float MutekiTime) {
        CanBeShot = false;
        PlayerSpriteToBlue();
        yield return new WaitForSeconds(MutekiTime);
        CanBeShot = true;
        yield return null;
    }
    IEnumerator SeedTimeCoroutine() {
        Time.timeScale = 0f;
        DicisionPointRender.enabled = true;
        player.input.EnterSeedTime();
        Tween tween = EnvironmentObjectsManager.Instance.MarkObjectSquare.GetComponent<Text>().DOText("", SeedTime).SetUpdate(true);
        tween.OnUpdate(()=> {
            if (isMuteki()) {
                tween.Complete();
            }
        });
        yield return tween.WaitForCompletion();
        DicisionPointRender.enabled = false;
        player.input.ExitSeedTime();
        if (CanBeShot) {
            DeadLoot();
            //PoolManager.Release(playerDeadVFX, transform.position);
            GameObject vfx = Instantiate(playerDeadVFX);
            vfx.transform.position = transform.position;
            GameData.Instance.PlayerBeShot();
            StartCoroutine(FlagChange(TimeMuteki));
        }
        Time.timeScale = 1;
    }
    public void DeadLoot() {
        spawner.Spawn(transform.position + new Vector3(0, 2f, 0));
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Danmu" || other.tag == "Enemy" || other.tag == "Boss") {
            if (CanBeShot) {
                AudioControl.Instance.PlayBeShot();
                StartCoroutine(nameof(SeedTimeCoroutine));
            }
        }

    }
}
