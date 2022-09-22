using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectionContextMenu : MonoBehaviour
{
    private static GameObject selectionContextMenuHudGO, DropAllButtonGO;
    private static RectTransform rectTransform;
    private void Start()
    {
        selectionContextMenuHudGO = transform.Find("SelectionContextMenuHud").gameObject;
        DropAllButtonGO = selectionContextMenuHudGO.transform.Find("DropAllButton").gameObject;
        rectTransform = selectionContextMenuHudGO.GetComponent<RectTransform>();
        selectionContextMenuHudGO.SetActive(false);
    }
    public static void Show(bool ifMoreThenOne)
    {
        selectionContextMenuHudGO.SetActive(true);
        if (ifMoreThenOne)
        {
            rectTransform.sizeDelta = new Vector2(300, 210);
            DropAllButtonGO.SetActive(true);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(300, 140);
            DropAllButtonGO.SetActive(false);
        }
        selectionContextMenuHudGO.transform.position = Input.mousePosition;
    }
    public static void UnShow()
    {
        DropAllButtonGO.SetActive(true);
        selectionContextMenuHudGO.SetActive(false);
    }
}
