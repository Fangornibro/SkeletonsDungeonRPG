using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibility : MonoBehaviour
{
    [HideInInspector]
    public Collider2D[] allSelectableItems;
    public static float selectRange = 2;
    public LayerMask selectedItemLayerMask;
    public Collider2D selectedItem, lastSelectedItem;

    void Update()
    {
        if (lastSelectedItem != null)
        {
            if (lastSelectedItem.tag == "Wall")
            {
                lastSelectedItem.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
        allSelectableItems = Physics2D.OverlapCircleAll(transform.position, selectRange, selectedItemLayerMask);


        foreach (Collider2D item in allSelectableItems)
        {
            if (selectedItem == null)
            {
                selectedItem = item;
                lastSelectedItem = item;
            }
            if (item.Distance(transform.GetComponent<Collider2D>()).distance < selectedItem.Distance(transform.GetComponent<Collider2D>()).distance)
            {

                selectedItem = item;
                lastSelectedItem = item;
            }
        }
        if (selectedItem != null)
        {
            if (selectedItem.tag == "Wall")
            {
                if (transform.position.y > selectedItem.transform.position.y)
                {
                    selectedItem.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.75f);
                }
            }
        }
        selectedItem = null;

    }
}
