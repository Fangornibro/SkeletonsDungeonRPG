using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public bool isLocked;
    public bool quest;
    private Collider2D doorColider;
    private Animator animator;
    private Dialogue GateIsClosed;
    public TextMeshProUGUI text;
    public bool ifInArea = false;
    public bool ifEnemyInArea = false;
    private void Start()
    {
        doorColider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        GateIsClosed = new Dialogue(new List<string> { "" }, new List<string> { "The gates are closed." });
    }
    void Update()
    {
        if (isLocked)
        {
            gameObject.layer = LayerMask.NameToLayer("Solid");
            doorColider.isTrigger = false;
            animator.SetBool("IsLocked", true);
            if (ifInArea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!quest)
                    {
                        isLocked = false;
                    }
                }
            }
            if (!quest)
            {
                if (ifEnemyInArea)
                {
                    isLocked = false;
                }
            }
        }
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
