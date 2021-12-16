using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps : PresistentSingleton<Fps>
{
    private float fpsMeasuringDelta = 2.0f;
    private int TargetFrame = 60;

    private float timePassed;
    private int m_FrameCount = 0;
    private float m_FPS = 0.0f;

    private void Start() {
        timePassed = 0.0f;
        Application.targetFrameRate = TargetFrame;
    }

    private void Update() {
        m_FrameCount = m_FrameCount + 1;
        timePassed = timePassed + Time.deltaTime;

        if (timePassed > fpsMeasuringDelta) {
            m_FPS = m_FrameCount / timePassed;

            timePassed = 0.0f;
            m_FrameCount = 0;
        }
    }

    public float GetFps() {
        return m_FPS;
    }
}
