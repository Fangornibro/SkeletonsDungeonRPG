using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public List<string> Person;
    public List<string> Text;
    public int FullList, curID = 0;
    public Dialogue(List<string> person, List<string> text)
    {
        Person = person;
        Text = text;
        FullList = Text.Count;
    }

    public void NextPhrase()
    {
        curID++;
    }
    public bool dialogueEnded()
    {
        return Text.Count == curID;
    }

    public string curPerson()
    {
        return Person[curID];
    }

    public string curText()
    {
        return Text[curID];
    }
}
