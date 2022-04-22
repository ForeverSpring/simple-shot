using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {
    [SerializeField] Vector2 scrollVelocity = new Vector2(0.1f, 0f);
    Material material;
    void Awake() {
        material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        material.mainTextureOffset += scrollVelocity * Time.deltaTime;
    }
}
