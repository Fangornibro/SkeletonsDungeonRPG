using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questGiver, name;
    public int statement = 1;
    public List<DialogueBranch> dialogues;
    public Quest(string QuestGiver, string Name, DialogueBranch TakeDialogue, DialogueBranch JustDialogue, DialogueBranch CompleteDialogue)
    {
        questGiver = QuestGiver;
        name = Name;
        dialogues = new List<DialogueBranch> { TakeDialogue, JustDialogue, CompleteDialogue };
    }
}
