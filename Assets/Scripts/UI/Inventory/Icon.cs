using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    public Item item;
    public Cell cell;
    public string name, description;
    private Transform player;
    private GameObject inventory;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
    }
    public void DropItem()
    {
        Instantiate(item, player.position, player.rotation);
        Destroy(item.gameObject);
        foreach (Cell cellinv in inventory.GetComponent<Inventory>().cells)
        {
            if (cellinv == cell)
            {
                Destroy(cellinv.icon.gameObject);
            }
        }
    }

    public void ChangeItemCell(Cell cellToChange)
    {
        cellToChange.icon = transform.GetComponent<Icon>().cell.icon;
        foreach (Cell cellinv in inventory.GetComponent<Inventory>().cells)
        {
            if (cellinv == cell)
            {
                cellinv.icon = null;
            }
        }
        cell = cellToChange;
    }
}