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
    int i =0;
    bool epressed = false, stringEnded = false;
    string Text = "";
    float delayBetweenLetters = 0.1f, startdelayBetweenLetters = 0.1f;
    public AudioSource textSound = GameObject.Find("textSound").GetComponent<AudioSource>();
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
                    if (stringEnded)
                    {
                        curQuest.takeDialogue.NextPhrase();
                        text.SetText("");
                        person.SetText("");
                        Text = "";
                        i = 0;
                        stringEnded = false;
                    }
                    else
                    {
                        if (epressed)
                        {
                            startdelayBetweenLetters = 0;
                        }
                    }
                    epressed = true;
                }
                if (epressed)
                {
                    isDialogueOpen = true;
                    Pause.pauseOn = true;
                    if (curQuest.takeDialogue.dialogueEnded())
                    {
                        text.SetText("");
                        person.SetText("");
                        Text = "";
                        i = 0;
                        stringEnded = false;
                        epressed = false;
                        isDialogueOpen = false;
                        Pause.pauseOn = false;
                        curQuest.statement = 2;
                    }
                    else
                    {
                        person.SetText(curQuest.takeDialogue.curPerson());
                        if (i < curQuest.takeDialogue.curText().Count)
                        {
                            if (delayBetweenLetters <= 0)
                            {
                                Text += curQuest.takeDialogue.curText()[i];
                                if (curQuest.takeDialogue.curText()[i] != " ")
                                {
                                    textSound.Play();
                                }
                                delayBetweenLetters = startdelayBetweenLetters;
                                i++;
                            }
                            else
                            {
                                delayBetweenLetters -= Time.deltaTime;
                            }
                        }
                        else
                        {
                            startdelayBetweenLetters = 0.1f;
                            epressed = false;
                            stringEnded = true;
                        }
                        text.SetText(Text);
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
                    if (stringEnded)
                    {
                        curQuest.justDialogue.NextPhrase();
                        text.SetText("");
                        person.SetText("");
                        Text = "";
                        i = 0;
                        stringEnded = false;
                    }
                    else
                    {
                        if (epressed)
                        {
                            startdelayBetweenLetters = 0;
                        }
                    }
                    epressed = true;
                }
                if (epressed)
                {
                    isDialogueOpen = true;
                    Pause.pauseOn = true;
                    if (curQuest.justDialogue.dialogueEnded())
                    {
                        text.SetText("");
                        person.SetText("");
                        Text = "";
                        i = 0;
                        stringEnded = false;
                        epressed = false;
                        curQuest.justDialogue.curID = 0;
                        isDialogueOpen = false;
                        Pause.pauseOn = false;
                    }
                    else
                    {
                        person.SetText(curQuest.justDialogue.curPerson());
                        if (i < curQuest.justDialogue.curText().Count)
                        {
                            if (delayBetweenLetters <= 0)
                            {
                                Text += curQuest.justDialogue.curText()[i];
                                if (curQuest.justDialogue.curText()[i] != " ")
                                {
                                    textSound.Play();
                                }
                                delayBetweenLetters = startdelayBetweenLetters;
                                i++;
                            }
                            else
                            {
                                delayBetweenLetters -= Time.deltaTime;
                            }
                        }
                        else
                        {
                            startdelayBetweenLetters = 0.1f;
                            epressed = false;
                            stringEnded = true;
                        }
                        text.SetText(Text);
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
                    if (stringEnded)
                    {
                        curQuest.completeDialogue.NextPhrase();
                        text.SetText("");
                        person.SetText("");
                        Text = "";
                        i = 0;
                        stringEnded = false;
                    }
                    else
                    {
                        if (epressed)
                        {
                            startdelayBetweenLetters = 0;
                        }
                    }
                    epressed = true;
                }
                if (epressed)
                {
                    isDialogueOpen = true;
                    Pause.pauseOn = true;
                    if (curQuest.completeDialogue.dialogueEnded())
                    {
                        text.SetText("");
                        person.SetText("");
                        Text = "";
                        i = 0;
                        stringEnded = false;
                        epressed = false;
                        allUncompletedQuests.RemoveAt(0);
                        isDialogueOpen = false;
                        Pause.pauseOn = false;
                    }
                    else
                    {
                        person.SetText(curQuest.completeDialogue.curPerson());
                        if (i < curQuest.completeDialogue.curText().Count)
                        {
                            if (delayBetweenLetters <= 0)
                            {
                                Text += curQuest.completeDialogue.curText()[i];
                                if (curQuest.completeDialogue.curText()[i] != " ")
                                {
                                    textSound.Play();
                                }
                                delayBetweenLetters = startdelayBetweenLetters;
                                i++;
                            }
                            else
                            {
                                delayBetweenLetters -= Time.deltaTime;
                            }
                        }
                        else
                        {
                            startdelayBetweenLetters = 0.1f;
                            epressed = false;
                            stringEnded = true;
                        }
                        text.SetText(Text);
                    }
                }
            }
        }
        return true;
    }
}
