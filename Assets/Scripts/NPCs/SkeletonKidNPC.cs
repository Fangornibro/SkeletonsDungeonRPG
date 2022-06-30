using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkeletonKidNPC : MonoBehaviour
{
    private Transform QuestPoint;
    public Quest quest1;
    public TextMeshProUGUI Text, Person;
    public static NPC SkeletonKid;
    private bool QuestExist = true;

    public GameObject button1, button2, button3, button4;

    List<Quest> AllUncompletedQuests;
    private void Start()
    {
        QuestPoint = transform.Find("QuestPoint");
        //First quest
        //takeDialogue1 = new Dialogue(new List<string> { "Skeleton kid", "Skeleton kid" }, new List<string> { "Hi, i lost my toy. Can you help me?", "Ty" }, new List<List<string>> { new List<string> { "Yes", "No", null, null }, new List<string> { "NP", null, null, null } });
        //completeDialogue1 = new Dialogue(new List<string> { "Skeleton kid"}, new List<string> { "Thanks, now die!" }, new List<List<string>> { new List<string> { "Wtf", null, null, null } });
        //justDialogue1 = new Dialogue(new List<string> { "Skeleton kid" }, new List<string> { "Help!"}, new List<List<string>> { new List<string> { "Ok", null, null, null } });
        //quest1 = new Quest("Skeleton kid", "Help find my toy", takeDialogue1, completeDialogue1, justDialogue1, 1);
        //List of quests
        //AllUncompletedQuests = new List<Quest> { quest1 };
        //NPC class
        //SkeletonKid = new NPC(QuestPoint, Text, Person, button1, button2, button3, button4, AllUncompletedQuests);
    }
    void Update()
    {
        //NPC quest system
        //if (QuestExist)
        //{
        //    QuestExist = SkeletonKid.NPCQuest();
        //}
    }
}
