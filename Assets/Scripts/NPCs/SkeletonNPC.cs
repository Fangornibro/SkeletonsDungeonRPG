using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkeletonNPC : MonoBehaviour
{
    private Transform QuestPoint;
    public DialogueBranch quest1takeDialoguebranch1, quest1takeDialoguebranch2, quest1takeDialoguebranch3, quest1takeDialoguebranch4, quest1takeDialoguebranch5, quest1JustDialoguebranch1, quest1CompleteDialoguebranch1, quest1CompleteDialoguebranch2;
    public Quest quest1;
    public Sprite icon;
    public static NPC Skeleton;
    private bool QuestExist = true;
    private GameObject inventory;
    List<Quest> AllUncompletedQuests;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");

        QuestPoint = transform.Find("QuestPoint");
        //First quest
        quest1takeDialoguebranch5 = new DialogueBranch("Skeleton", "Ooh, that's bad.", icon, "Bye", null, null, null, null, null, null, null, 0);
        quest1takeDialoguebranch4 = new DialogueBranch("Skeleton", "Nice, hurry up!", icon, "Ok", null, null, null, null, null, null, null, 1);
        quest1takeDialoguebranch3 = new DialogueBranch("Skeleton", "Ooh, that's bad.", icon, "Bye", null, null, null, null, null, null, null, 0);
        quest1takeDialoguebranch2 = new DialogueBranch("Skeleton", "Oh, thanks. Can you find the key to my prison cell?", icon, "I can handle it.", "Sorry. I can`t", null, null, quest1takeDialoguebranch4, quest1takeDialoguebranch5, null, null, 0);
        quest1takeDialoguebranch1 = new DialogueBranch("Skeleton", "Hey you, can you help me?", icon, "Yeah", "No", null, null, quest1takeDialoguebranch2, quest1takeDialoguebranch3, null, null, 0);

        quest1JustDialoguebranch1 = new DialogueBranch("Skeleton", "Did you find the key?", icon, "No", null, null, null, null, null, null, null, 0);

        quest1CompleteDialoguebranch2 = new DialogueBranch("Skeleton", "Quick, give it here.", icon, "(Give the key to the skeleton)", null, null, null, null, null, null, null, 1);
        quest1CompleteDialoguebranch1 = new DialogueBranch("Skeleton", "Did you find the key?", icon, "Yes", "No", null, null, quest1CompleteDialoguebranch2, null, null, null, 0);

        quest1 = new Quest("Skeleton", "Get key", quest1takeDialoguebranch1, quest1JustDialoguebranch1, quest1CompleteDialoguebranch1);
        //List of quests
        AllUncompletedQuests = new List<Quest> { quest1 };
        //NPC class
        Skeleton = new NPC(QuestPoint, AllUncompletedQuests);
    }
    void Update()
    {
        //NPC quest system
        if (QuestExist)
        {
            QuestExist = Skeleton.NPCQuest();
        }

        if (Skeleton.curQuest.name == "Get key")
        {
            foreach (Cell cell in inventory.GetComponent<Inventory>().cells)
            {
                if (cell.icon != null)
                {
                    if (cell.icon.name == "Key" && Skeleton.curQuest.statement == 2)
                    {
                        Skeleton.SetStatement(3);
                    }
                }
            }
        }
    }
}
