using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject iconPrefab;
    private GameObject inventory;
    [HideInInspector]
    public GameObject icon;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
    }
    public void take()
    {
        foreach (Cell cell in inventory.GetComponent<Inventory>().cells)
        {
            if (cell.icon == null)
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
