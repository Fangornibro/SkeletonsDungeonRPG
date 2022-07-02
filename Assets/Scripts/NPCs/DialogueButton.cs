using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour, IPointerClickHandler
{
    public static NPC curNPC;
    public static InteractableItem curInteractableItem;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (curNPC != null)
            {
                if (transform.name == "DialogueButton1")
                {
                    curNPC.DialogueSelection(curNPC.curDialogueBranch.choice1dialoguebranch);
                }
                if (transform.name == "DialogueButton2")
                {
                    curNPC.DialogueSelection(curNPC.curDialogueBranch.choice2dialoguebranch);
                }
                if (transform.name == "DialogueButton3")
                {
                    curNPC.DialogueSelection(curNPC.curDialogueBranch.choice3dialoguebranch);
                }
                if (transform.name == "DialogueButton4")
                {
                    curNPC.DialogueSelection(curNPC.curDialogueBranch.choice4dialoguebranch);
                }
            }
            curNPC = null;
            if (curInteractableItem != null)
            {
                if (transform.name == "DialogueButton1")
                {
                    curInteractableItem.DialogueSelection(curInteractableItem.curDialogueBranch.choice1dialoguebranch);
                }
                if (transform.name == "DialogueButton2")
                {
                    curInteractableItem.DialogueSelection(curInteractableItem.curDialogueBranch.choice2dialoguebranch);
                }
                if (transform.name == "DialogueButton3")
                {
                    curInteractableItem.DialogueSelection(curInteractableItem.curDialogueBranch.choice3dialoguebranch);
                }
                if (transform.name == "DialogueButton4")
                {
                    curInteractableItem.DialogueSelection(curInteractableItem.curDialogueBranch.choice4dialoguebranch);
                }
            }
            curInteractableItem = null;
        }
    }
}
