using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Icon : MonoBehaviour
{
    public Item item, newItem;
    public Cell cell;
    public string name, itemType ,description;
    private Transform player;
    private GameObject inventory;
    [HideInInspector]
    public int maxNumber, curNumber;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
    }
    public void DropOneItem()
    {
        newItem = Instantiate(item, player.position, player.rotation);
        curNumber--;
        transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(curNumber));
        if (curNumber == 0)
        {
            Destroy(gameObject);
        }
    }

    public void DropAllItem()
    {
        newItem = Instantiate(item, player.position, player.rotation);
        newItem.curNumber = curNumber;
        Destroy(gameObject);
    }

    public void Use()
    {
        curNumber--;
        transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(curNumber));
        if (curNumber <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeItemCell(Cell cellToChange)
    {
        if (cellToChange.icon == null)
        {
            if (cellToChange.GetComponent<CellType>().cellType == item.GetComponent<CellType>().cellType || cellToChange.GetComponent<CellType>().cellType == CellType.Type.Everything)
            {
                cellToChange.icon = transform.GetComponent<Icon>().cell.icon;
                foreach (Cell cellinv in inventory.GetComponent<Inventory>().cells)
                {
                    if (cellinv == cell)
                    {
                        cellinv.icon = null;
                        break;
                    }
                }
                cell = cellToChange;
                transform.position = transform.GetComponent<Icon>().cell.transform.position;
            }
        }
        else
        {
            if ((cellToChange.GetComponent<CellType>().cellType == item.GetComponent<CellType>().cellType || cellToChange.GetComponent<CellType>().cellType == CellType.Type.Everything) && (cell.GetComponent<CellType>().cellType == cellToChange.icon.item.GetComponent<CellType>().cellType || cell.GetComponent<CellType>().cellType == CellType.Type.Everything))
            {
                if (cellToChange.icon.name == name)
                {
                    if (cellToChange.icon.maxNumber > 1 && cellToChange.icon.curNumber != cellToChange.icon.maxNumber)
                    {
                        if (curNumber > cellToChange.icon.maxNumber - cellToChange.icon.curNumber)
                        {
                            curNumber -= cellToChange.icon.maxNumber - cellToChange.icon.curNumber;
                            cellToChange.icon.curNumber += cellToChange.icon.maxNumber - cellToChange.icon.curNumber;
                        }
                        else
                        {
                            cellToChange.icon.curNumber += curNumber;
                            Destroy(gameObject);
                        }
                        transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(curNumber));
                        cellToChange.icon.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(cellToChange.icon.curNumber));
                    }
                }
                else
                {
                    cellToChange.icon.transform.position = transform.position;
                    Icon temp = cellToChange.icon;
                    cellToChange.icon = transform.GetComponent<Icon>().cell.icon;
                    foreach (Cell cellinv in inventory.GetComponent<Inventory>().cells)
                    {
                        if (cellinv == cell)
                        {
                            cellinv.icon = temp;
                            cellinv.icon.cell = cellinv;
                            break;
                        }
                    }
                    cell = cellToChange;
                    transform.position = transform.GetComponent<Icon>().cell.transform.position;
                }
            }
        }
    }
}