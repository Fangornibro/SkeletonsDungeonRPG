using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public RectTransform icon;
    public string name, description;

    public void take()
    {
        foreach (Cell cell in Inventory.cells)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cell.item == null)
                {
                    cell.item = this;
                    transform.position = new Vector2(-100, 0);
                }
            }
        }
    }
}
