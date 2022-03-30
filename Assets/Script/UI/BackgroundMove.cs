using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {
    [SerializeField] public float moveSpeed;
    public GameObject StartTopBG, StartBottomBG;
    bool StartBottomStillBottom = true;
    static float BGHeight = 19f;
    void Update()
    {
        CheckPosition();
        StartTopBG.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        StartBottomBG.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    void CheckPosition() {
        if (StartBottomBG.transform.position.y <= -5 && StartTopBG.transform.position.y <= -5) {
            if (StartBottomStillBottom) {
                StartBottomBG.transform.position += new Vector3(0, BGHeight, 0);
                StartBottomStillBottom = false;
            }
            else {
                StartTopBG.transform.position += new Vector3(0, BGHeight, 0);
                StartBottomStillBottom = true;
            }
        }
    }
}
