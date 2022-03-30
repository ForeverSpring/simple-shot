using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int life = 10;
    public void SetLife(int life) {
        this.life = life;
    }

    public void GetInjured(int injuredLife) {
        if (injuredLife >= life) {
            Dead();
            return;
        }
        else
            life -= injuredLife;
    }

    public void Dead() {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            other.gameObject.SetActive(false);
            GetInjured(1);
        }
    }

}
