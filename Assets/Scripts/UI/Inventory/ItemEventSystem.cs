using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEventSystem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject iconPrefab;
    private GameObject inventory;
    private RectTransform inventoryHud, iconRectTransform;
    public static bool canPlayerShoot = true;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventoryHud = inventory.transform.Find("InventoryHud").gameObject.GetComponent<RectTransform>();
        iconRectTransform = transform.GetComponent<RectTransform>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            iconPrefab.GetComponent<Icon>().DropItem();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Left)
        {
            transform.GetComponent<Image>().color = new Color(0.3584906f, 0.3584906f, 0.3584906f, 1);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Left)
        {
            transform.GetComponent<Image>().color = new Color(0.6603774f, 0.6603774f, 0.6603774f, 1);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = new Color(0.6603774f, 0.6603774f, 0.6603774f, 1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        canPlayerShoot = false;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = eventData.pointerCurrentRaycast.screenPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canPlayerShoot = true;
        if (iconRectTransform.localPosition.y > inventoryHud.localPosition.y + inventoryHud.rect.height/2 || iconRectTransform.localPosition.y < inventoryHud.localPosition.y - inventoryHud.rect.height / 2 || iconRectTransform.localPosition.x > inventoryHud.localPosition.x + inventoryHud.rect.width / 2 || iconRectTransform.localPosition.x < inventoryHud.localPosition.x - inventoryHud.rect.width / 2)
        {
            transform.GetComponent<Icon>().DropItem();
        }
        foreach (Cell cellinv in inventory.GetComponent<Inventory>().cells)
        {
            if (cellinv.GetComponent<Collider2D>().Distance(GetComponent<Collider2D>()).distance < 40 && cellinv.icon == null)
            {
                transform.GetComponent<Icon>().ChangeItemCell(cellinv);
                transform.position = transform.GetComponent<Icon>().cell.transform.position;
            }
            else
            {
                transform.position = transform.GetComponent<Icon>().cell.transform.position;
            }
        }
    }

}
