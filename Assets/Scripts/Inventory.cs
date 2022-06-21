using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> InventoryUI = new List<GameObject>();
    [HideInInspector]
    public Cell cell11, cell12, cell13, cell21, cell22, cell23;
    public static List<Cell> cells;
    private bool isInventOpen = false;
    void Start()
    {
        cell11 = new Cell(-100, 60, null);
        cell12 = new Cell(0, 60, null);
        cell13 = new Cell(100, 60, null);
        cell21 = new Cell(-100, -30, null);
        cell22 = new Cell(0, -30, null);
        cell23 = new Cell(100, -30, null);
        cells = new List<Cell> { cell23, cell22, cell21, cell13, cell12, cell11 };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventOpen = !isInventOpen;
        }
        if (isInventOpen)
        {
            foreach (GameObject UI in InventoryUI)
            {
                UI.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject UI in InventoryUI)
            {
                UI.SetActive(false);
            }
        }

        foreach (Cell cell in cells)
        {
            if (cell.item != null)
            {
                cell.item.icon.localPosition = new Vector2(cell.x, cell.y);
            }
        }
    }
}
    