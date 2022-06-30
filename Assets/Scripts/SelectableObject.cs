using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour

{
    [HideInInspector]
    public Collider2D[] allSelectableItems;
    public static float selectRange;
    public LayerMask selectedItemLayerMask;
    public static Collider2D selectedItem, lastSelectedItem;
    public Material selectedMaterial, defaultMaterial;
    public Transform invent, menu;

    void Update()
    {
        if (!menu.GetComponent<Menu>().isMenuOpen && !invent.GetComponent<Inventory>().isInventOpen)
        {
            if (lastSelectedItem != null)
            {
                lastSelectedItem.GetComponentInParent<SpriteRenderer>().material = defaultMaterial;
                if (lastSelectedItem.GetComponentInParent<Gate>() != null)
                {
                    lastSelectedItem.GetComponentInParent<Gate>().ifInArea = false;
                    if (lastSelectedItem.GetComponentInParent<Gate>().quest == Gate.Quest.Skeleton)
                    {
                        SkeletonNPC.Skeleton.ifInArea = false;
                    }
                }
                if (lastSelectedItem.GetComponentInParent<SkeletonKidNPC>() != null)
                {
                    SkeletonKidNPC.SkeletonKid.ifInArea = false;
                }
                if (lastSelectedItem.GetComponentInParent<Ladder>() != null)
                {
                    lastSelectedItem.GetComponentInParent<Ladder>().ifInArea = false;
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
                selectedItem.GetComponentInParent<SpriteRenderer>().material = selectedMaterial;
                if (selectedItem.GetComponentInParent<Gate>() != null)
                {
                    selectedItem.GetComponentInParent<Gate>().ifInArea = true;
                    if (selectedItem.GetComponentInParent<Gate>().quest == Gate.Quest.Skeleton)
                    {
                        SkeletonNPC.Skeleton.ifInArea = true;
                    }
                }
                if (selectedItem.GetComponentInParent<SkeletonKidNPC>() != null)
                {
                    SkeletonKidNPC.SkeletonKid.ifInArea = true;
                }
                if (selectedItem.GetComponentInParent<Ladder>() != null)
                {
                    selectedItem.GetComponentInParent<Ladder>().ifInArea = true;
                }
                if (selectedItem.GetComponentInParent<Item>() != null)
                {
                    selectedItem.GetComponentInParent<Item>().take();
                }
            }
            selectedItem = null;
        }
        else
        {
            if (lastSelectedItem != null)
            {
                if (lastSelectedItem.GetComponentInParent<Gate>() != null)
                {
                    lastSelectedItem.GetComponentInParent<Gate>().ifInArea = false;
                    if (lastSelectedItem.GetComponentInParent<Gate>().quest == Gate.Quest.Skeleton)
                    {
                        SkeletonNPC.Skeleton.ifInArea = false;
                    }
                }
                if (lastSelectedItem.GetComponentInParent<SkeletonKidNPC>() != null)
                {
                    SkeletonKidNPC.SkeletonKid.ifInArea = false;
                }
                if (lastSelectedItem.GetComponentInParent<Ladder>() != null)
                {
                    lastSelectedItem.GetComponentInParent<Ladder>().ifInArea = false;
                }
            }
        }
    }
}
