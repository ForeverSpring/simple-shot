using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texStage : MonoBehaviour
{
    private Text text;
    public float timeFull = 6.5f;
    private float timeLast = 4.5f;

    void Start()
    {
        text = GetComponent<Text>();
        text.CrossFadeAlpha(0, 0, true);
    }

    public void printStage() {
        StartCoroutine(printText());
    }

    public IEnumerator printText() {
        yield return new WaitForSeconds(2f);
        text.CrossFadeAlpha(1, timeLast / 4, true);
        yield return new WaitForSeconds((3 * timeLast) / 4);
        text.CrossFadeAlpha(0, timeLast / 4, true);
        yield return null;
    }

    public void setText(string tex) {
        text.text = tex;
    }
}
