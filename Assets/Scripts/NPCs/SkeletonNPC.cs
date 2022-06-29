using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkeletonNPC : MonoBehaviour
{
    private Transform QuestPoint, QuestArea;
    public static Dialogue takeDialogue1, completeDialogue1, justDialogue1;
    public DialogueBranch branch1, branch2, branch3, branch4, branch5;
    public Quest quest1;
    public static Quest curQuest;
    public TextMeshProUGUI Text, Person;
    public static NPC Skeleton;
    private bool QuestExist = true;
    public Collider2D player;
    public Transform invent, menu;

    public GameObject button1, button2, button3, button4;

    List<Quest> AllUncompletedQuests;
    private void Start()
    {
        QuestPoint = transform.Find("QuestPoint");
        QuestArea = transform.Find("SkeletonNPCQuestArea");
        //First quest
        branch5 = new DialogueBranch("Nice, hurry up!", "Ok.", null, null, null, null, null, null, null);
        branch4 = new DialogueBranch("Ooh, that's bad.", "Bye", null, null, null, null, null, null, null);
        branch3 = new DialogueBranch("Ooh, that's bad.", "Bye", null, null, null, null, null, null, null);
        branch2 = new DialogueBranch("Oh, thanks. Can you find the key to my prison cell?", "I can handle it.", "Sorry. I can`t", null, null, branch4, branch5, null, null);
        branch1 = new DialogueBranch("Hey you, can you help me?", "Yeah", "No", null, null, branch2, branch3, null, null);

        takeDialogue1 = new Dialogue(new List<string> { "???", "???" }, new List<string> { "Hey you, can you help me?", "Bring me some bones." }, new List<List<string>> { new List<string> { "Yes", "No", null, null }, new List<string> { "Ok", "No", null, null } });
        completeDialogue1 = new Dialogue(new List<string> { "???"}, new List<string> { "Nice job."}, new List<List<string>> { new List<string> { "Ty", null, null, null } });
        justDialogue1 = new Dialogue(new List<string> { "" }, new List<string> { "The gates are closed." }, new List<List<string>> { new List<string> { "Ok", null, null, null } });
        quest1 = new Quest("Skeleton", "Get key", takeDialogue1, completeDialogue1, justDialogue1, 1);
        //List of quests
        AllUncompletedQuests = new List<Quest> { quest1 };
        //NPC class
        Skeleton = new NPC(QuestPoint, Text, Person, button1, button2, button3, button4, AllUncompletedQuests);
    }
    void Update()
    {
        if (!menu.GetComponent<Menu>().isMenuOpen && !invent.GetComponent<Inventory>().isInventOpen)
        {
            if (QuestArea.GetComponent<Collider2D>().Distance(player).distance <= 0)
            {
                Skeleton.ifInArea = true;
            }
            else
            {
                Skeleton.ifInArea = false;
            }
        }
        else
        {
            Skeleton.ifInArea = false;
        }
        //NPC quest system
        if (QuestExist)
        {
            QuestExist = Skeleton.NPCQuest();
        }


        foreach (Cell cell in Inventory.cells)
        {
            if (cell.item != null)
            {
                if (cell.item.name == "Key")
                {
                    if (Skeleton.curQuest.name == "Get key" && Skeleton.curQuest.statement == 2)
                    {
                        Skeleton.curQuest.emptyCounter = 1;
                    }
                    break;
                }
            }
            Skeleton.curQuest.emptyCounter = 0;
        }
    }
}
