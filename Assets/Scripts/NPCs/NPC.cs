using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPC
{
    public Transform player, questPoint;
    public Quest curQuest;
    public TextMeshProUGUI text, person;
    public List<Quest> allUncompletedQuests;
    public bool ifInArea = false;
    public static bool isDialogueOpen = false;
    public NPC(Transform QuestPoint, TextMeshProUGUI Text, TextMeshProUGUI Person, List<Quest> AllUncompletedQuests)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        questPoint = QuestPoint;     
        text = Text;
        person = Person;
        allUncompletedQuests = AllUncompletedQuests;
    }

    public bool NPCQuest()
    {
        if (allUncompletedQuests.Count != 0)
        {
            curQuest = allUncompletedQuests[0];
        }
        else
        {
            questPoint.GetComponent<Animator>().SetInteger("Statement", 4);
            return false;
        }
       

        if (curQuest.statement == 1)
        {
            questPoint.GetComponent<Animator>().SetInteger("Statement", 1);

            if (ifInArea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (curQuest.takeDialogue.dialogueEnded())
                    {
                        text.SetText("");
                        person.SetText("");
                        curQuest.statement = 2;
                        isDialogueOpen = false;
                        Pause.pauseOn = false;
                    }
                    else
                    {
                        person.SetText(curQuest.takeDialogue.curPerson());
                        text.SetText(curQuest.takeDialogue.curText());
                        curQuest.takeDialogue.NextPhrase();
                        isDialogueOpen = true;
                        Pause.pauseOn = true;
                    }
                }
            }
        }
        else if (curQuest.statement == 2)
        {
            questPoint.GetComponent<Animator>().SetInteger("Statement", 2);
            if (ifInArea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (curQuest.justDialogue.dialogueEnded())
                    {
                        text.SetText("");
                        person.SetText("");
                        curQuest.justDialogue.curID = 0;
                        isDialogueOpen = false;
                        Pause.pauseOn = false;
                    }
                    else
                    {
                        person.SetText(curQuest.justDialogue.curPerson());
                        text.SetText(curQuest.justDialogue.curText());
                        curQuest.justDialogue.NextPhrase();
                        isDialogueOpen = true;
                        Pause.pauseOn = true;
                    }
                }
            }

            if (curQuest.emptyCounter == curQuest.counter)
            {
                curQuest.statement = 3;
            }
        }
        else if (curQuest.statement == 3)
        {
            questPoint.GetComponent<Animator>().SetInteger("Statement", 3);

            if (ifInArea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (curQuest.completeDialogue.dialogueEnded())
                    {
                        text.SetText("");
                        person.SetText("");
                        allUncompletedQuests.RemoveAt(0);
                        isDialogueOpen = false;
                        Pause.pauseOn = false;
                    }
                    else
                    {
                        person.SetText(curQuest.completeDialogue.curPerson());
                        text.SetText(curQuest.completeDialogue.curText());
                        curQuest.completeDialogue.NextPhrase();
                        isDialogueOpen = true;
                        Pause.pauseOn = true;
                    }
                }
            }
        }
        return true;
    }
}
