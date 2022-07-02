using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootableObject : MonoBehaviour
{
    public enum Type { SkeletonBody }
    public Type type;

    public static InteractableItem LootableObjectInt;
    public DialogueBranch SkeletonBodyDialoguebranch1, SkeletonBodyDialoguebranch2;
    public TextMeshProUGUI Text, Object;
    public GameObject dialogueHudFaceIcon, button1, button2, button3, button4;
    public Sprite skeletonBodyIcon, playerIcon;
    [HideInInspector]
    public bool ifInArea = false;
    private void Start()
    {
        if (type == Type.SkeletonBody)
        {
            SkeletonBodyDialoguebranch2 = new DialogueBranch("Skeleton body", "hi...............", skeletonBodyIcon, "...", null, null, null, null, null, null, null, 1, 16);
            SkeletonBodyDialoguebranch1 = new DialogueBranch("Me", "He's dead... right?", playerIcon, "Loot", "Leave", null, null, SkeletonBodyDialoguebranch2, null, null, null, 0);
            LootableObjectInt = new InteractableItem(SkeletonBodyDialoguebranch1);
        }
    }

    private void Update()
    {
        if (ifInArea)
        {
            if (type == Type.SkeletonBody)
            {
                if (LootableObjectInt.Interaction())
                {

                }
            }
        }
    }
}
