using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public enum Quest { none, Skeleton }
    public Quest quest;


    private Collider2D doorColider;
    private Animator animator;
    [HideInInspector]
    public bool isLocked;
    [HideInInspector]
    public bool ifInArea = false;
    [HideInInspector]
    public bool ifEnemyInArea = false;
    private void Start()
    {
        doorColider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Gate opening
        if (isLocked)
        {
            gameObject.layer = LayerMask.NameToLayer("Solid");
            doorColider.isTrigger = false;
            animator.SetBool("IsLocked", true);
            if (ifInArea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (quest == Quest.none)
                    {
                        isLocked = false;
                    }
                }
            }
            if (quest == Quest.none)
            {
                if (ifEnemyInArea)
                {
                    isLocked = false;
                }
            }
        }
        //Gate closing
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            doorColider.isTrigger = true;
            animator.SetBool("IsLocked", false);
            if (ifInArea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ifEnemyInArea = false;
                    isLocked = true;
                }
            }
        }
    }
}
