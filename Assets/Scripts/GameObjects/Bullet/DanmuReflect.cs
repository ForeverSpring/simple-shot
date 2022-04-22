using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmuReflect : Danmu {
    [SerializeField] private int reflectTimes;

    void ReflectInitSettings() {
        base.InitSettings();
        reflectTimes = GameSettings.Instance.settings.danmuReflectTimes;
    }
    void Awake() {
        ReflectInitSettings();
    }
    private void OnDisable() {
        ReflectInitSettings();
    }
    void FixedUpdate() {
        transform.position = transform.position + transform.up * speedDanmuBall * Time.fixedDeltaTime;
        if (reflectTimes > 0 && !Boundary.InBoundary(transform.position)) {
            transform.up = CheckPos();
            reflectTimes--;
        }
    }

    Vector3 CheckPos() {
        if (transform.position.x < Boundary.xMin || transform.position.x > Boundary.xMax) {
            return Vector3.Reflect(transform.up, new Vector3(1f, 0f, 0f));
        }
        return transform.up;
    }
}
