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
        }
        else
        {
            doorColider.isTrigger = true;
            animator.SetBool("IsLocked", false);
            if (ifInArea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isLocked = true;
                }
            }
        }
    }
}
