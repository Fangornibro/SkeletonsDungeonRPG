using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    GameObject player;
    BoxCollider2D damageCollider;
    CircleCollider2D trigerCollider;
    Animator animator;
    float timeBtwDamage = 0f;
    bool isOn = false;
    void Start()
    {
        trigerCollider = GetComponent<CircleCollider2D>();
        damageCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!Pause.pauseOn)
        {
            if (trigerCollider.Distance(player.transform.Find("ForTraps").GetComponent<Collider2D>()).distance <= 0)
            {
                if (timeBtwDamage <= 0)
                {
                    timeBtwDamage = 3f;
                    isOn = false;
                }
                else
                {
                    isOn = true;
                }
            }
            if (isOn)
            {
                if (timeBtwDamage <= 2.6f)
                {
                    animator.SetBool("IsTrapOn", false);
                }
                else
                {
                    animator.SetBool("IsTrapOn", true);
                    if (damageCollider.Distance(player.transform.Find("ForTraps").GetComponent<Collider2D>()).distance <= 0)
                    {
                        player.GetComponent<Player>().takeDamage(20);
                    }
                }
                timeBtwDamage -= Time.deltaTime;
            }
        }
    }
}
