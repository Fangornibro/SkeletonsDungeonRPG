using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questGiver, name;
    public int statement = 1, counter, emptyCounter = 0;
    public Dialogue takeDialogue, completeDialogue, justDialogue;
    public Quest(string QuestGiver, string Name, Dialogue TakeDialogue, Dialogue CompleteDialogue, Dialogue JustDialogue, int Counter)
    {
        questGiver = QuestGiver;
        name = Name;
        takeDialogue = TakeDialogue;
        completeDialogue = CompleteDialogue;
        counter = Counter;
        justDialogue = JustDialogue;
    }
}
