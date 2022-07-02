using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> InventoryUI = new List<GameObject>();
    public Transform menu;
    public List<Cell> cells;
    [HideInInspector]
    public bool isInventOpen = false;

    void Update()
    {
        for (int i = 0; i < InventoryUI.Count; i++)
        {
            if (InventoryUI[i].gameObject == null)
            {
                InventoryUI.RemoveAt(i);
            }
        } 
        //Inventory open/close
        if (Input.GetKeyDown(KeyCode.I) && !menu.GetComponent<Menu>().isMenuOpen && !NPC.isDialogueOpen || isInventOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            isInventOpen = !isInventOpen;
        }
        //Activation/deactivation of UI when inventory opened
        if (isInventOpen)
        {
            if (InventoryUI.Count != 0)
            {
                foreach (GameObject UI in InventoryUI)
                {
                    UI.SetActive(true);
                }
            }

            
        }
        else
        {
            if (InventoryUI.Count != 0)
            {
                foreach (GameObject UI in InventoryUI)
                {
                    UI.SetActive(false);
                }
            }
        }
    }
}