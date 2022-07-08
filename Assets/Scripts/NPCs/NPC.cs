using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPC
{
    public Transform questPoint;
    public Quest curQuest;
    public TextMeshProUGUI text = GameObject.Find("Text").GetComponent<TextMeshProUGUI>(), person = GameObject.Find("Person").GetComponent<TextMeshProUGUI>();
    public GameObject faceIcon = GameObject.Find("DialogueHudFaceIcon"), button1 = GameObject.Find("DialogueButton1"), button2 = GameObject.Find("DialogueButton2"), button3 = GameObject.Find("DialogueButton3"), button4 = GameObject.Find("DialogueButton4");
    public List<Quest> allUncompletedQuests;
    public bool ifInArea = false;
    public static bool isDialogueOpen = false;
    int ichar = 0;
    public bool epressed = false, stringEnded = false, initialization = true;
    string Text = "";
    float delayBetweenLetters = 0.1f, startdelayBetweenLetters = 0.1f;
    public AudioSource textSound = GameObject.Find("textSound").GetComponent<AudioSource>();
    public DialogueBranch curDialogueBranch;
    public int plusStatement = 0;
    public Inventory invent = GameObject.Find("Inventory").GetComponent<Inventory>();
    public NPC(Transform QuestPoint, List<Quest> AllUncompletedQuests)
    {
        questPoint = QuestPoint;     
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

        questPoint.GetComponent<Animator>().SetInteger("Statement", curQuest.statement);

        if (initialization)
        {
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            curDialogueBranch = curQuest.dialogues[curQuest.statement - 1];
            initialization = false;
        }

        if (ifInArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                invent.isInventOpen = false;
                if (!stringEnded)
                {
                    if (epressed)
                    {
                        startdelayBetweenLetters = 0;
                    }
                }
                epressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!stringEnded)
                {
                    if (epressed)
                    {
                        startdelayBetweenLetters = 0;
                    }
                }
            }
            if (stringEnded)
            {
                DialogueButton.curNPC = this;
                if (curDialogueBranch.choice1text != null)
                {
                    button1.SetActive(true);
                    button1.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice1text;
                }
                if (curDialogueBranch.choice2text != null)
                {
                    button2.SetActive(true);
                    button2.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice2text;
                }
                if (curDialogueBranch.choice3text != null)
                {
                    button3.SetActive(true);
                    button3.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice3text;
                }
                if (curDialogueBranch.choice4text != null)
                {
                    button4.SetActive(true);
                    button4.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice4text;
                }
            }
            if (epressed)
            {
                isDialogueOpen = true;
                Pause.pauseOn = true;
                if (curDialogueBranch == null)
                {
                    text.SetText("");
                    person.SetText("");
                    Text = "";
                    ichar = 0;
                    stringEnded = false;
                    epressed = false;
                    isDialogueOpen = false;
                    Pause.pauseOn = false;
                    initialization = true;
                    if (curQuest.statement == 3 && plusStatement == 1)
                    {
                        allUncompletedQuests.RemoveAt(0);
                    }
                    else
                    {
                        curQuest.statement += plusStatement;
                    }
                }
                else
                {
                    faceIcon.GetComponent<Image>().sprite = curDialogueBranch.icon;
                    person.SetText(curDialogueBranch.person);
                    if (ichar < curDialogueBranch.textToChars().Count)
                    {
                        if (delayBetweenLetters <= 0)
                        {
                            Text += curDialogueBranch.textToChars()[ichar];
                            if (curDialogueBranch.textToChars()[ichar] != " ")
                            {
                                textSound.Play();
                            }
                            delayBetweenLetters = startdelayBetweenLetters;
                            ichar++;
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
                    text.fontSize = curDialogueBranch.fontSize;
                    text.SetText(Text);
                }
            }
        }
        return true;
    }
    public void DialogueSelection(DialogueBranch nextDialogueBranch)
    {
        if (stringEnded)
        {
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            if (nextDialogueBranch == null)
            {
                plusStatement = curDialogueBranch.nextStatement;
            }
            curDialogueBranch = nextDialogueBranch;
            text.SetText("");
            person.SetText("");
            Text = "";
            ichar = 0;
            stringEnded = false;
            epressed = true;
        }
    }

    public void SetStatement(int Statemnet)
    {
        curQuest.statement = Statemnet;
        initialization = true;
    }
}
