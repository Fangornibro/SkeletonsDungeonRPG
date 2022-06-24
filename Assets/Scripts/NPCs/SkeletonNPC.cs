using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkeletonNPC : MonoBehaviour
{
    private Transform QuestPoint, QuestArea;
    public static Dialogue takeDialogue1, completeDialogue1, justDialogue1;
    public Quest quest1;
    public static Quest curQuest;
    public TextMeshProUGUI Text, Person;
    public static NPC Skeleton;
    private bool QuestExist = true;
    public Collider2D player;
    public Transform invent, menu;

    List<Quest> AllUncompletedQuests;
    private void Start()
    {
        QuestPoint = transform.Find("QuestPoint");
        QuestArea = transform.Find("SkeletonNPCQuestArea");
        //First quest
        takeDialogue1 = new Dialogue(new List<string> { "Skeleton", "Player", "Skeleton" }, new List<string> { "Hey you, can you help me?", "Yeah.", "Bring me some bones." });
        completeDialogue1 = new Dialogue(new List<string> { "Skeleton", "Player" }, new List<string> { "Nice job.", "Ty." });
        justDialogue1 = new Dialogue(new List<string> { "" }, new List<string> { "The gates are closed." });
        quest1 = new Quest("Skeleton", "Get key", takeDialogue1, completeDialogue1, justDialogue1, 1);
        //List of quests
        AllUncompletedQuests = new List<Quest> { quest1 };
        //NPC class
        Skeleton = new NPC(QuestPoint, Text, Person, AllUncompletedQuests);
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
