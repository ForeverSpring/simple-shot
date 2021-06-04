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
    public GameControl Control;
    public GameObject VfxBomb;
    public 判定点碰撞 DecisionPoint;
    public AudioSource audBomb;

    IEnumerator UseBomb() {
        FlagBomb = false;
        //有B并且不在无敌状态可以使用B
        if (Control.hasBomb() && !DecisionPoint.isMuteki()) {
            audBomb.Play();
            Control.useBomb();
            DecisionPoint.SetMutekiTime(TimePerBomb);
            GameObject tempVfx=Instantiate(VfxBomb, rb.transform);
            yield return new WaitForSeconds(TimePerBomb);
            Destroy(tempVfx);
        }
        FlagBomb = true;
        yield return null;
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

    void Start() {
        inLowSpeed = false;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float moveHorizontal = 0;
        float moveVertical = 0;
        float moveSpeed = speedPlayerMove1;
        if (Input.GetKey(KeyCode.UpArrow)) {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            moveVertical = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveHorizontal = 1;
        }
        if (Input.GetKey(KeyCode.Z)) {
            if (FlagBomb) {
                StartCoroutine(UseBomb());
            }
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = speedPlayerMove2;
            inLowSpeed = true;
        }
        else {
            inLowSpeed = false;
        }
        //根据移动方向设置移动速度，保证速度向量大小不变
        Vector3 movement;
        if (moveHorizontal != 0 || moveVertical != 0) {
            float temp = Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical);
            movement = new Vector3(moveHorizontal / temp, moveVertical / temp, 0.0f);
        }
        else {
            movement = new Vector3(0f, 0f, 0f);
        }
        rb.velocity = movement * moveSpeed;
        //TODO：修改边界的反弹错误
        //transform.position = transform.position + movement * moveSpeed * Time.fixedDeltaTime;

        //限制坐标在边界内
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, Boundary.xMin, Boundary.xMax),
        Mathf.Clamp(rb.position.y, Boundary.yMin, Boundary.yMax),
        0.0f);
    }
}
