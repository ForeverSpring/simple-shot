using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldPlayerControl : MonoBehaviour {
    private Rigidbody2D rb;
    private float speedPlayerMove1;
    private float speedPlayerMove2;
    private bool inLowSpeed;
    private bool FlagBomb = true;
    public float TimePerBomb = 5f;
    private float FireRate;
    private float nextFire = 0f;
    public GameObject VfxBomb;
    public GameObject normalBullet;
    public GameObject autoBullet;
    public DecisionPoint DecisionPoint;
    public Animator animator;
    void InitSettings() {
        DecisionPoint = GetComponent<DecisionPoint>();
        speedPlayerMove1 = GameSettings.Instance.settings.playerMoveSpeedHigh;
        speedPlayerMove2 = GameSettings.Instance.settings.playerMoveSpeedLow;
        FireRate = GameSettings.Instance.settings.playerFireRate;
    }

    void Start() {
        InitSettings();
        inLowSpeed = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        normalBullet = (GameObject)Resources.Load("Prefab/Bullet/BulletPlayer");
        autoBullet = (GameObject)Resources.Load("Prefab/Bullet/AutoBulletPlayer");
    }

    void Update() {
        //低速模式显示判定点，高速模式隐藏判定点
        if (inLowSpeed) {
            GameObject.Find("DecisionPoint").GetComponent<Renderer>().enabled = true;
        }
        else {
            GameObject.Find("DecisionPoint").GetComponent<Renderer>().enabled = false;
        }
    }

    void FixedUpdate() {
        float moveHorizontal = 0;
        float moveVertical = 0;
        float moveSpeed = speedPlayerMove1;
        bool signalBomb = false;
        bool signalLowSpeed = false;
        bool signalFire = false;
        oldPlayerInput.Instance.GetInputSingal(ref moveHorizontal, ref moveVertical, ref signalBomb, ref signalLowSpeed, ref signalFire);

        if (signalBomb && FlagBomb) {
            StartCoroutine(UseBomb());
        }
        if (signalLowSpeed) {
            moveSpeed = speedPlayerMove2;
            inLowSpeed = true;
        }
        else {
            inLowSpeed = false;
        }
        if (signalFire && Time.fixedUnscaledTime >= nextFire) {
            nextFire = Time.fixedUnscaledTime + FireRate;
            Fire();
        }
        //向量标准化，保证移动速度大小不变
        Vector2 movement = Vector2.zero;
        if (moveHorizontal != 0 || moveVertical != 0) {
            movement = new Vector2(moveHorizontal, moveVertical).normalized;
        }
        animator.SetFloat("left", -movement.x);
        animator.SetFloat("right", movement.x);
        rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        if (movement != Vector2.zero)
            StartCoroutine(nameof(PlayerPositionLimitCoroutine));
        else
            StopCoroutine(nameof(PlayerPositionLimitCoroutine));
    }

    void Fire() {
        //GameObject temp= Instantiate(normalBullet);
        GameObject temp = Instantiate(autoBullet);
        temp.transform.position = rb.transform.position;
        temp.transform.forward = rb.transform.forward;
    }

    IEnumerator UseBomb() {
        FlagBomb = false;
        //有B并且不在无敌状态可以使用B
        if (GameData.Instance.hasSpell() && !DecisionPoint.isMuteki()) {
            AudioControl.Instance.PlayUseBomb();
            GameData.Instance.useSpell();
            DecisionPoint.SetMutekiTime(TimePerBomb);
            GameObject tempVfx = Instantiate(VfxBomb, rb.transform);
            yield return new WaitForSeconds(TimePerBomb);
            Destroy(tempVfx);
        }
        FlagBomb = true;
        yield return null;
    }

    IEnumerator PlayerPositionLimitCoroutine() {
        while (true) {
            //限制坐标在边界内
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, Boundary.xMin, Boundary.xMax),
                                        Mathf.Clamp(rb.position.y, Boundary.yMin, Boundary.yMax));
            yield return null;
        }
    }
}
