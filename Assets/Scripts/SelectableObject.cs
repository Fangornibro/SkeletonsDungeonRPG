using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour

{
    public Collider2D[] allSelectableItems;
    public static float selectRange;
    public LayerMask selectedItemLayerMask;
    private Collider2D selectedItem, lastSelectedItem;
    public Material selectedMaterial, defaultMaterial;

    void Update()
    {
        if (lastSelectedItem != null)
        {
            lastSelectedItem.GetComponentInParent<SpriteRenderer>().material = defaultMaterial;
            if (lastSelectedItem.name == "GateSelectionArea")
            {
                lastSelectedItem.GetComponentInParent<Gate>().ifInArea = false;
            }
            if (lastSelectedItem.name == "SkeletonKidNPCSelectionArea")
            {
                SkeletonKidNPC.SkeletonKid.ifInArea = false;
            }
            if (lastSelectedItem.name == "DoubleLadderFloor1" || lastSelectedItem.name == "DoubleLadderFloor2" || lastSelectedItem.name == "DoubleLadderFloor3" || lastSelectedItem.name == "DoubleLadderFloor4" || lastSelectedItem.name == "DoubleLadderFloor5" || lastSelectedItem.name == "DoubleLadderFloor6")
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
            if (selectedItem.name == "GateSelectionArea")
            {
                selectedItem.GetComponentInParent<Gate>().ifInArea = true;
            }
            if (selectedItem.name == "SkeletonKidNPCSelectionArea")
            {
                SkeletonKidNPC.SkeletonKid.ifInArea = true;
            }
            if (selectedItem.name == "DoubleLadderFloor1" || selectedItem.name == "DoubleLadderFloor2" || selectedItem.name == "DoubleLadderFloor3" || selectedItem.name == "DoubleLadderFloor4" || selectedItem.name == "DoubleLadderFloor5" || selectedItem.name == "DoubleLadderFloor6")
            {
                selectedItem.GetComponentInParent<Ladder>().ifInArea = true;
            }
            if (selectedItem.name == "Key")
            {
                selectedItem.GetComponentInParent<Item>().take();
            }
        }
        selectedItem = null;

    }
}
