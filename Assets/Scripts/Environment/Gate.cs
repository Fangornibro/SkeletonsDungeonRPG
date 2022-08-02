using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public enum GateQuest { none, Skeleton, StartGate }
    public GateQuest quest;


    private Collider2D doorColider;
    private Animator animator;
    [HideInInspector]
    public bool isLocked;
    [HideInInspector]
    public bool ifInArea = false;
    [HideInInspector]
    public bool ifEnemyInArea = false;

    //Start gate
    public static InteractableItem StartGate;
    public DialogueBranch StartGateDialoguebranch1;
    public Sprite playetIcon;
    private void Start()
    {
        doorColider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        if (quest == GateQuest.StartGate)
        {
            StartGateDialoguebranch1 = new DialogueBranch("Me", "Looks like the lock is rusted. How long have I been here?", playetIcon, "Open the gate", null, null, null, null, null, null, null, 1);
            StartGate = new InteractableItem(StartGateDialoguebranch1);
        }
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
                if (quest == GateQuest.none)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isLocked = false;
                    }
                }
                else if (quest == GateQuest.StartGate)
                {
                    if (StartGate.Interaction())
                    {
                        quest = GateQuest.none;
                        isLocked = false;
                    }
                }
            }
            if (quest == GateQuest.none)
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
