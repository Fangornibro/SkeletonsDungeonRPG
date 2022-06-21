using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkeletonKidNPC : MonoBehaviour
{
    private Transform QuestPoint;
    public static Dialogue takeDialogue1, completeDialogue1, justDialogue1;
    public Quest quest1;
    public static Quest curQuest;
    public TextMeshProUGUI Text, Person;
    public static NPC SkeletonKid;
    private bool QuestExist = true;

    List<Quest> AllUncompletedQuests;
    private void Start()
    {
        QuestPoint = transform.Find("QuestPoint");
        //First quest
        takeDialogue1 = new Dialogue(new List<string> { "Skeleton kid", "Player", "Skeleton kid" }, new List<string> { "Hi, i lost my toy. Can you help me?", "Yeah." });
        completeDialogue1 = new Dialogue(new List<string> { "Skeleton kid", "Player" }, new List<string> { "Thanks, now die!", "..." });
        justDialogue1 = new Dialogue(new List<string> { "Skeleton kid", "Player" }, new List<string> { "Thanks, now die!", "..." });
        quest1 = new Quest("Skeleton kid", "Help find my toy", takeDialogue1, completeDialogue1, justDialogue1, 1);
        //List of quests
        AllUncompletedQuests = new List<Quest> { quest1 };
        //NPC class
        SkeletonKid = new NPC(QuestPoint, Text, Person, AllUncompletedQuests);
    }
    void Update()
    {
        //NPC quest system
        if (QuestExist)
        {
            QuestExist = SkeletonKid.NPCQuest();
        }
    }
}
