using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MyTimer : MonoBehaviour {
    [SerializeField] float RunTime;
    [SerializeField] bool isRunning;
    void Start() {
        RunTime = 0f;
        isRunning = false;
    }
    void Update() {
        if (isRunning) {
            RunTime += Time.fixedDeltaTime;
        }
    }
    public bool StartTime() {
        if (isRunning)
            return false;
        isRunning = true;
        return true;
    }
    public bool EndTime() {
        if (!isRunning)
            return false;
        isRunning = false;
        return true;
    }
    public float GetRunTime() {
        return RunTime;
    }
    public void ResetTime() {
        RunTime = 0f;
    }
}
