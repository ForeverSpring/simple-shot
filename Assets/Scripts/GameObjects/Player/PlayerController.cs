using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //TODO:添加Spell伤害效果和动画
    [Header("Fire")]
    [SerializeField] public GameObject autoBullet;
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject cubeBullet;
    [SerializeField] public float FireRate;
    [SerializeField] public Renderer DecisionPointRender;
    public float AutoFireRate;
    private float nextFire = 0f;
    private float nextAutoFire = 0f;
    [SerializeField] int FireLevel => GameData.Instance != null ? GameData.Instance.playerFireLevel : 0;
    [Header("Spell")]
    [SerializeField] public GameObject VfxBomb;
    [SerializeField] public float TimePerBomb = 5f;
    private bool FlagBomb = true;
    [Header("Other")]
    [SerializeField] public GameObject funnelRightObj, funnelLeftObj;
    public PlayerInput input;
    Rigidbody2D rigidbody2D;

    private void Awake() {
        input = GetComponent<PlayerInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        input.EnableGameplayInputs();
        input.DisableGameMenuInputs();
    }
    private void Update() {
        CheckFire();
        CheckBomb();
        CheckDecisionPoint();
    }
    public void Move(float speed) {
        if (input.MoveX)
            transform.localScale = new Vector3(input.AxisX, 1f);
        else
            transform.localScale = new Vector3(1f, 1f);
        //标准化保证移动速度不变
        SetVelocity(speed * input.Axis.normalized);
        if (input.Move)
            StartCoroutine(nameof(PlayerPositionLimitCoroutine));
        else
            StopCoroutine(nameof(PlayerPositionLimitCoroutine));
    }
    public void SetVelocity(Vector2 velocity) {
        //使用velocity会导致越出边界
        //rigidbody2D.velocity = velocity;
        rigidbody2D.position += new Vector2(velocity.x, velocity.y) * Time.fixedDeltaTime;
    }
    public void SetVelocityX(float velocityX) {
        rigidbody2D.velocity = new Vector2(velocityX, rigidbody2D.velocity.y);
    }
    public void SetVelocityY(float velocityY) {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, velocityY);
    }
    IEnumerator PlayerPositionLimitCoroutine() {
        while (true) {
            //限制坐标在边界内
            rigidbody2D.position = new Vector3(Mathf.Clamp(rigidbody2D.position.x, Boundary.xMin, Boundary.xMax),
                                        Mathf.Clamp(rigidbody2D.position.y, Boundary.yMin, Boundary.yMax));
            yield return null;
        }
    }
    void CheckDecisionPoint() {
        if (input.inLowSpeed)
            DecisionPointRender.enabled = true;
        else
            DecisionPointRender.enabled = false;
    }
    void CheckFire() {
        switch (FireLevel) {
            case 0:
                FireRate = 0.2f;
                AutoFireRate = 0.8f;
                CheckFireLevel0();
                break;
            case 1:
                FireRate = 0.2f;
                AutoFireRate = 1.5f;
                CheckFireLevel1();
                break;
            case 2:
                FireRate = 0.2f;
                AutoFireRate = 1.2f;
                CheckFireLevel2();
                break;
            case 3:
                FireRate = 0.2f;
                AutoFireRate = 1f;
                CheckFireLevel3();
                break;
            default:
                Debug.LogWarning("Invalid FireLevel " + FireLevel);
                break;
        }
    }
    void CheckFireLevel0() {
        nextFire += Time.fixedDeltaTime;
        if (input.Fire && nextFire >= FireRate) {
            nextFire = 0;
            FireLevel0();
        }
    }
    void CheckFireLevel1() {
        nextFire += Time.fixedDeltaTime;
        nextAutoFire += Time.fixedDeltaTime;
        if (input.Fire && nextFire >= FireRate) {
            nextFire = 0;
            FireLevel1Normal();
        }
        if (input.Fire && nextAutoFire >= AutoFireRate) {
            nextAutoFire = 0;
            FireLevel1Auto();
        }
    }
    void CheckFireLevel2() {
        nextFire += Time.fixedDeltaTime;
        nextAutoFire += Time.fixedDeltaTime;
        if (input.Fire && nextFire >= FireRate) {
            nextFire = 0;
            FireLevel2Normal();
        }
        if (input.Fire && nextAutoFire >= AutoFireRate) {
            nextAutoFire = 0;
            FireLevel2Auto();
        }
    }
    void CheckFireLevel3() {
        nextFire += Time.fixedDeltaTime;
        nextAutoFire += Time.fixedDeltaTime;
        if (input.Fire && nextFire >= FireRate) {
            nextFire = 0;
            FireLevel3Normal();
        }
        if (input.Fire && nextAutoFire >= AutoFireRate) {
            nextAutoFire = 0;
            FireLevel3Auto();
        }
    }
    void FireLevel0() {
        GameObject midCubeBullet = PoolManager.Release(cubeBullet);
        midCubeBullet.transform.position = rigidbody2D.transform.position;
        midCubeBullet.transform.up = rigidbody2D.transform.up;
        AudioControl.Instance.PlayPlayerShot();
    }
    void FireLevel1Normal() {
        GameObject midCubeBullet = PoolManager.Release(cubeBullet);
        midCubeBullet.transform.position = rigidbody2D.transform.position;
        midCubeBullet.transform.up = rigidbody2D.transform.up;
        AudioControl.Instance.PlayPlayerShot();
    }
    void FireLevel1Auto() {
        GameObject leftAutoBullet = PoolManager.Release(autoBullet);
        leftAutoBullet.transform.position = funnelLeftObj.transform.position;
        leftAutoBullet.transform.up = funnelLeftObj.transform.up;
        GameObject rightAutoBullet = PoolManager.Release(autoBullet);
        rightAutoBullet.transform.position = funnelRightObj.transform.position;
        rightAutoBullet.transform.up = funnelRightObj.transform.up;
        AudioControl.Instance.PlayPlayerShot();
    }
    void FireLevel2Normal() {
        GameObject midCubeBulletLeft = PoolManager.Release(cubeBullet);
        midCubeBulletLeft.transform.position = rigidbody2D.transform.position + new Vector3(0.2f, 0, 0);
        midCubeBulletLeft.transform.up = rigidbody2D.transform.up;
        GameObject midCubeBulletRight = PoolManager.Release(cubeBullet);
        midCubeBulletRight.transform.position = rigidbody2D.transform.position + new Vector3(-0.2f, 0, 0);
        midCubeBulletRight.transform.up = rigidbody2D.transform.up;
        AudioControl.Instance.PlayPlayerShot();
    }
    void FireLevel2Auto() {
        GameObject leftAutoBullet = PoolManager.Release(autoBullet);
        leftAutoBullet.transform.position = funnelLeftObj.transform.position;
        leftAutoBullet.transform.up = funnelLeftObj.transform.up;
        GameObject rightAutoBullet = PoolManager.Release(autoBullet);
        rightAutoBullet.transform.position = funnelRightObj.transform.position;
        rightAutoBullet.transform.up = funnelRightObj.transform.up;
        AudioControl.Instance.PlayPlayerShot();
    }
    void FireLevel3Normal() {
        GameObject midCubeBulletCenter = PoolManager.Release(cubeBullet);
        midCubeBulletCenter.transform.position = rigidbody2D.transform.position;
        midCubeBulletCenter.transform.up = rigidbody2D.transform.up;
        GameObject midCubeBulletLeft = PoolManager.Release(cubeBullet);
        midCubeBulletLeft.transform.position = rigidbody2D.transform.position + new Vector3(0.2f, 0, 0);
        midCubeBulletLeft.transform.up = funnelLeftObj.transform.up;
        GameObject midCubeBulletRight = PoolManager.Release(cubeBullet);
        midCubeBulletRight.transform.position = rigidbody2D.transform.position + new Vector3(-0.2f, 0, 0);
        midCubeBulletRight.transform.up = funnelRightObj.transform.up;
        AudioControl.Instance.PlayPlayerShot();
    }
    void FireLevel3Auto() {
        GameObject leftAutoBullet = PoolManager.Release(autoBullet);
        leftAutoBullet.transform.position = funnelLeftObj.transform.position;
        leftAutoBullet.transform.up = funnelLeftObj.transform.up;
        GameObject rightAutoBullet = PoolManager.Release(autoBullet);
        rightAutoBullet.transform.position = funnelRightObj.transform.position;
        rightAutoBullet.transform.up = funnelRightObj.transform.up;
        AudioControl.Instance.PlayPlayerShot();
    }
    void CheckBomb() {
        if (input.Bomb && FlagBomb) {
            StartCoroutine(UseBomb());
        }
    }
    IEnumerator UseBomb() {
        FlagBomb = false;
        //有B并且不在无敌状态可以使用B
        if (GameData.Instance.hasSpell() && !GetComponent<DecisionPoint>().isMuteki()) {
            EnvironmentObjectsManager.Instance.ClearDanmu();
            AudioControl.Instance.PlayUseBomb();
            GameData.Instance.useSpell();
            GameUIControl.Instance.PlayerFukaImageShow();
            GetComponent<DecisionPoint>().SetMutekiTime(TimePerBomb);
            GameObject tempVfx = PoolManager.Release(VfxBomb, rigidbody2D.transform.position);
            yield return new WaitForSeconds(TimePerBomb);
            tempVfx.SetActive(false);
        }
        FlagBomb = true;
        yield return null;
    }

}
