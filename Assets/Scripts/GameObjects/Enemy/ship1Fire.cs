using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship1Fire : MonoBehaviour
{
    [SerializeField] public bool fire;
    [SerializeField] public float fireRate = 1, fireSpeed = 1;
    void Start()
    {
        fire = false;
        StartCoroutine(nameof(FireIE));
    }

    IEnumerator FireIE() {
        while (true) {
            if (fire) {
                GameObject bulluet = DanmuFactory.Instance.Getfireball_red_tail_big();
                bulluet.transform.position = this.transform.position;
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
