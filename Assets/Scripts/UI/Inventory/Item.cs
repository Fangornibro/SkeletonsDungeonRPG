using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject iconPrefab;
    private GameObject inventory;
    [HideInInspector]
    public GameObject icon;
    public string name;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
    }
    public void take()
    {
        foreach (Cell cell in inventory.GetComponent<Inventory>().cells)
        {
            if (GetComponent<CellType>().cellType == CellType.Type.Usable)
            {
                if (cell.icon != null)
                {
                    if (cell.icon.name == name && cell.icon.curNumber < cell.icon.maxNumber)
                    {
                        cell.icon.curNumber++;
                        cell.icon.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(cell.icon.curNumber));
                        Destroy(gameObject);
                        return;
                    }
                }
            }
        }
        foreach (Cell cell in inventory.GetComponent<Inventory>().cells)
        {
            if (cell.icon == null && (cell.GetComponent<CellType>().cellType == GetComponent<CellType>().cellType || cell.GetComponent<CellType>().cellType == CellType.Type.Everything))
            {
                icon = Instantiate(iconPrefab);
                icon.transform.SetParent(inventory.transform);
                icon.GetComponent<RectTransform>().localPosition = new Vector2(cell.x, cell.y);
                icon.GetComponent<Icon>().item = this;
                icon.GetComponent<Icon>().cell = cell;
                inventory.GetComponent<Inventory>().InventoryUI.Add(icon);
                cell.icon = icon.GetComponent<Icon>();
                transform.position = new Vector2(-100, 0);
                break;
            }
        }
    }
}
