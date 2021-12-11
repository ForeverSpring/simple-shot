using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPlayer : MonoBehaviour {
    private float speedPlayerMove1 = 5.8f;
    private float speedPlayerMove2 = 3.1f;
    private Rigidbody rb;
    private bool inLowSpeed;
    private bool FlagBomb = true;
    public float TimePerBomb = 5f;
    public GameObject VfxBomb;
    public 判定点碰撞 DecisionPoint;
    public AudioSource audBomb;

    void Update() {
        //低速模式显示判定点，高速模式隐藏判定点
        if (inLowSpeed) {
            GameObject.Find("DecisionPoint").GetComponent<Renderer>().enabled = true;
        }
        else {
            GameObject.Find("DecisionPoint").GetComponent<Renderer>().enabled = false;
        }
    }

    void Start() {
        inLowSpeed = false;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        //TODO:设置自定义按键
        float moveHorizontal = 0;
        float moveVertical = 0;
        float moveSpeed = speedPlayerMove1;
        bool signalBomb = false;
        bool signalLowSpeed = false;
        PlayerInput.Instance.GetInputSingal(ref moveHorizontal, ref moveVertical, ref signalBomb, ref signalLowSpeed);

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

        //根据移动方向设置移动速度，保证速度向量大小不变
        Vector3 movement = Vector3.zero;
        if (moveHorizontal != 0 || moveVertical != 0) {
            float temp = Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical);
            movement = new Vector3(moveHorizontal / temp, moveVertical / temp, 0.0f);
        }
        rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        if (movement != Vector3.zero) {
            StartCoroutine(nameof(PlayerPositionLimitCoroutine));
        }
        else {
            StopCoroutine(nameof(PlayerPositionLimitCoroutine));
        }
        
    }

    IEnumerator UseBomb() {
        FlagBomb = false;
        //有B并且不在无敌状态可以使用B
        if (GameControl.Instance.hasBomb() && !DecisionPoint.isMuteki()) {
            audBomb.Play();
            GameControl.Instance.useBomb();
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
                                        Mathf.Clamp(rb.position.y, Boundary.yMin, Boundary.yMax),
                                        0.0f);
            yield return null;
        }
    }
}
