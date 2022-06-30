using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NPC
{
    public Transform player, questPoint;
    public Quest curQuest;
    public TextMeshProUGUI text, person;
    public GameObject button1, button2, button3, button4;
    public List<Quest> allUncompletedQuests;
    public bool ifInArea = false;
    public static bool isDialogueOpen = false;
    int ichar = 0;
    bool epressed = false, stringEnded = false, initialization = true;
    string Text = "";
    float delayBetweenLetters = 0.1f, startdelayBetweenLetters = 0.1f;
    public AudioSource textSound = GameObject.Find("textSound").GetComponent<AudioSource>();
    public DialogueBranch curDialogueBranch;
    public int plusStatement = 0;
    public NPC(Transform QuestPoint, TextMeshProUGUI Text, TextMeshProUGUI Person, GameObject Button1, GameObject Button2, GameObject Button3, GameObject Button4, List<Quest> AllUncompletedQuests)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        questPoint = QuestPoint;     
        text = Text;
        person = Person;
        allUncompletedQuests = AllUncompletedQuests;
        button1 = Button1;
        button2 = Button2;
        button3 = Button3;
        button4 = Button4;
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
            curDialogueBranch = curQuest.dialogues[curQuest.statement - 1];
            initialization = false;
        }

        if (ifInArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
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

                if (curDialogueBranch.choice1text != null)
                {
                    button1.SetActive(true);
                    button1.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice1text;
                    button1.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curDialogueBranch.choice1dialoguebranch); });
                }
                if (curDialogueBranch.choice2text != null)
                {
                    button2.SetActive(true);
                    button2.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice2text;
                    button2.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curDialogueBranch.choice2dialoguebranch); });
                }
                if (curDialogueBranch.choice3text != null)
                {
                    button3.SetActive(true);
                    button3.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice3text;
                    button3.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curDialogueBranch.choice3dialoguebranch); });
                }
                if (curDialogueBranch.choice4text != null)
                {
                    button4.SetActive(true);
                    button4.GetComponentInChildren<TextMeshProUGUI>().text = curDialogueBranch.choice4text;
                    button4.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curDialogueBranch.choice4dialoguebranch); });
                }
            }
            if (epressed)
            {
                isDialogueOpen = true;
                Pause.pauseOn = true;
                if (curDialogueBranch == null)
                {
                    button1.GetComponent<Button>().onClick.RemoveAllListeners();
                    button2.GetComponent<Button>().onClick.RemoveAllListeners();
                    button3.GetComponent<Button>().onClick.RemoveAllListeners();
                    button4.GetComponent<Button>().onClick.RemoveAllListeners();
                    text.SetText("");
                    person.SetText("");
                    Text = "";
                    ichar = 0;
                    stringEnded = false;
                    epressed = false;
                    isDialogueOpen = false;
                    Pause.pauseOn = false;
                    initialization = true;
                    if (curQuest.statement == 3)
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
