using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] float speedPlayerMove1 = 5.8f;
    [SerializeField] float speedPlayerMove2 = 3.1f;
    private bool isLowSpeed = false;
    new Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onLowSpeed += LowSpeed;
        input.onNormalSpeed += NormalSpeed;
    }

    void OnDisable() {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
        input.onLowSpeed -= LowSpeed;
        input.onNormalSpeed -= NormalSpeed;
    }

    void Start()
    {
        rigidbody.gravityScale = 0f;
        input.EnableGameplayInput();
    }

    void Move(Vector2 moveInput) {
        //float speed = isLowSpeed ? speedPlayerMove2 : speedPlayerMove1;
        rigidbody.velocity = moveInput * (isLowSpeed ? speedPlayerMove2 : speedPlayerMove1);
    }

    void StopMove() {
        rigidbody.velocity = Vector2.zero;
    }

    void LowSpeed() {
        Debug.Log("low speed");
        isLowSpeed = true;
    }

    void NormalSpeed() {
        isLowSpeed = false;
    }
}
