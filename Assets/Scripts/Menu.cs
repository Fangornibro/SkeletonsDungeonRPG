using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Transform invent;
    public bool isMenuOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !invent.GetComponent<Inventory>().isInventOpen && !NPC.isDialogueOpen)
        {
            Pause.pauseOn = !Pause.pauseOn;
            isMenuOpen = !isMenuOpen;
        }
    }
}