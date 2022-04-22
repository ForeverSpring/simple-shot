using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadVFX : MonoBehaviour
{
    [SerializeField] public Animator animator;
    private void OnEnable() {
        animator.Play("Start");
        Invoke("Finish", 2f);
    }
    void Finish() {
        this.gameObject.SetActive(false);
    }
}
