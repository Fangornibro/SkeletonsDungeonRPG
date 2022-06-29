using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public List<string> Person;
    public List<string> Text;
    public List<List<string>> Button;
    public int curID = 0;
    public Dialogue(List<string> person, List<string> text, List<List<string>> button)
    {
        Person = person;
        Text = text;
        Button = button;
    }

    public void NextPhrase()
    {
        curID++;
    }
    public bool dialogueEnded()
    {
        return Text.Count == curID;
    }

    public List<string> curText()
    {
        List<string> Textletters = new List<string>();
        foreach (char s in Text[curID])
        {
            Textletters.Add(s.ToString());
        }
        return Textletters;
    }

    public string curPerson()
    {
        return Person[curID];
    }
}
