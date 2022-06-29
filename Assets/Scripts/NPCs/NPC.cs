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
    int i =0;
    bool epressed = false, stringEnded = false;
    string Text = "";
    float delayBetweenLetters = 0.1f, startdelayBetweenLetters = 0.1f;
    public AudioSource textSound = GameObject.Find("textSound").GetComponent<AudioSource>();
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
        button4 = Button1;
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
                    if(!stringEnded)
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
                    
                    if (curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][0] != null)
                    {
                        button1.SetActive(true);
                        button1.GetComponentInChildren<TextMeshProUGUI>().text = curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][0];
                        button1.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][0], curQuest.takeDialogue); });
                    }
                    if (curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][1] != null)
                    {
                        button2.SetActive(true);
                        button2.GetComponentInChildren<TextMeshProUGUI>().text = curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][1];
                        button2.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][1], curQuest.takeDialogue); });
                    }
                    if (curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][2] != null)
                    {
                        button3.SetActive(true);
                        button3.GetComponentInChildren<TextMeshProUGUI>().text = curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][2];
                        button3.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][2], curQuest.takeDialogue); });
                    }
                    if (curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][3] != null)
                    {
                        button4.SetActive(true);
                        button4.GetComponentInChildren<TextMeshProUGUI>().text = curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][3];
                        button4.GetComponent<Button>().onClick.AddListener(delegate { DialogueSelection(curQuest.takeDialogue.Button[curQuest.takeDialogue.curID][3], curQuest.takeDialogue); });
                    }
                }
                if (epressed)
                {
                    isDialogueOpen = true;
                    Pause.pauseOn = true;
                    if (curQuest.takeDialogue.dialogueEnded())
                    {
                        button1.GetComponent<Button>().onClick.RemoveAllListeners();
                        button2.GetComponent<Button>().onClick.RemoveAllListeners();
                        button3.GetComponent<Button>().onClick.RemoveAllListeners();
                        button4.GetComponent<Button>().onClick.RemoveAllListeners();
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
    public void DialogueSelection(string textFromButton, Dialogue curDialogue)
    {
        if (stringEnded)
        {
            if (curDialogue.Button[curQuest.takeDialogue.curID][0] != null)
            {
                button1.SetActive(false);
            }
            if (curDialogue.Button[curQuest.takeDialogue.curID][1] != null)
            {
                button2.SetActive(false);
            }
            if (curDialogue.Button[curQuest.takeDialogue.curID][2] != null)
            {
                button3.SetActive(false);
            }
            if (curDialogue.Button[curQuest.takeDialogue.curID][3] != null)
            {
                button4.SetActive(false);
            }
            curDialogue.NextPhrase();
            text.SetText("");
            person.SetText("");
            Text = "";
            i = 0;
            stringEnded = false;
            epressed = true;
        }
    }
}
