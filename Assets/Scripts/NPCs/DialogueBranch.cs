using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBranch
{
    public string text, person;

    public string choice1text;
    public string choice2text;
    public string choice3text;
    public string choice4text;
    public DialogueBranch choice1dialoguebranch;
    public DialogueBranch choice2dialoguebranch;
    public DialogueBranch choice3dialoguebranch;
    public DialogueBranch choice4dialoguebranch;
    public int nextStatement;

    public DialogueBranch(string Person, string Text, string Choice1text, string Choice2text, string Choice3text, string Choice4text, DialogueBranch Choice1dialoguebranch, DialogueBranch Choice2dialoguebranch, DialogueBranch Choice3dialoguebranch, DialogueBranch Choice4dialoguebranch, int NextStatement)
    {
        person = Person;
        text = Text;
        choice1text = Choice1text;  
        choice2text = Choice2text;
        choice3text = Choice3text;
        choice4text = Choice4text;
        choice1dialoguebranch = Choice1dialoguebranch;
        choice2dialoguebranch = Choice2dialoguebranch;
        choice3dialoguebranch = Choice3dialoguebranch;
        choice4dialoguebranch = Choice4dialoguebranch;
        nextStatement = NextStatement;
    }

    public List<string> textToChars()
    {
        List<string> Textletters = new List<string>();
        foreach (char s in text)
        {
            Textletters.Add(s.ToString());
        }
        return Textletters;
    }
}
