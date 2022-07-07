using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InteractableItem
{
    public Quest curQuest;
    public TextMeshProUGUI text = GameObject.Find("Text").GetComponent<TextMeshProUGUI>(), person = GameObject.Find("Person").GetComponent<TextMeshProUGUI>();
    public GameObject faceIcon = GameObject.Find("DialogueHudFaceIcon"), button1 = GameObject.Find("DialogueButton1"), button2 = GameObject.Find("DialogueButton2"), button3 = GameObject.Find("DialogueButton3"), button4 = GameObject.Find("DialogueButton4");

    public static bool isDialogueOpen = false;
    int ichar = 0;
    public bool epressed = false, stringEnded = false, initialization = true;
    string Text = "";
    float delayBetweenLetters = 0.1f, startdelayBetweenLetters = 0.1f;
    public AudioSource textSound = GameObject.Find("textSound").GetComponent<AudioSource>();
    public DialogueBranch dialogue, curDialogueBranch;
    public int Statement = 0;
    public Inventory invent = GameObject.Find("Inventory").GetComponent<Inventory>();
    public InteractableItem(DialogueBranch Dialogue)
    {
        dialogue = Dialogue;
    }

    public bool Interaction()
    {
        if (initialization)
        {
            curDialogueBranch = dialogue;
            initialization = false;
        }
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
            DialogueButton.curInteractableItem = this;
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
                if (Statement == 1)
                {
                    return true;
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
        return false;
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
                Statement = curDialogueBranch.nextStatement;
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
}

