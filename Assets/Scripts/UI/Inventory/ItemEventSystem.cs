using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEventSystem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject iconPrefab;
    private GameObject inventory, DropButtonGO, UseButtonGO, DropAllButtonGO;
    private RectTransform inventoryHud, inventoryHudArmor, inventoryHudBottom, iconRectTransform;
    public static bool canPlayerShoot = true;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventoryHud = inventory.transform.Find("InventoryHud").gameObject.GetComponent<RectTransform>();
        inventoryHudArmor = inventory.transform.Find("InventoryHudArmor").gameObject.GetComponent<RectTransform>();
        inventoryHudBottom = GameObject.Find("InventoryHudBottom").gameObject.GetComponent<RectTransform>();
        iconRectTransform = transform.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SelectionContextMenu.UnShow();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory.GetComponent<Inventory>().isInventOpen)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (GetComponent<Icon>().curNumber > 1)
                {
                    SelectionContextMenu.Show(true);
                    DropAllButtonGO = GameObject.Find("DropAllButton");
                    DropAllButtonGO.GetComponent<ItemDropAllButton>().Icon = transform;
                }
                else
                {
                    SelectionContextMenu.Show(false);
                }
                DropButtonGO = GameObject.Find("DropButton");
                DropButtonGO.GetComponent<ItemDropButton>().Icon = transform;
                UseButtonGO = GameObject.Find("UseButton");
                UseButtonGO.GetComponent<ItemUseButton>().Icon = transform;

            }
        }
        else
        {
            if (eventData.button == PointerEventData.InputButton.Left && GetComponent<Icon>().cell.GetComponent<CellType>().cellType == CellType.Type.Usable)
            {
                GetComponent<Icon>().Use();
            }
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
        if (inventory.GetComponent<Inventory>().isInventOpen)
        {
            transform.GetComponent<Image>().color = Color.white;
            ContextMenu.Show(transform.GetComponent<Icon>().name, transform.GetComponent<Icon>().itemType, transform.GetComponent<Icon>().description, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = new Color(0.6603774f, 0.6603774f, 0.6603774f, 1);
        ContextMenu.UnShow();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventory.GetComponent<Inventory>().isInventOpen)
        {
            ContextMenu.UnShow();
            canPlayerShoot = false;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = eventData.pointerCurrentRaycast.screenPosition;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Icon>().transform.SetParent(GameObject.Find("Canvas").transform);
        if (inventory.GetComponent<Inventory>().isInventOpen)
        {
            ContextMenu.UnShow();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventory.GetComponent<Inventory>().isInventOpen)
        {
            ContextMenu.UnShow();
            canPlayerShoot = true;
            if ((iconRectTransform.position.y > inventoryHud.position.y + inventoryHud.rect.height / 2 || iconRectTransform.position.y < inventoryHud.position.y - inventoryHud.rect.height / 2 || iconRectTransform.position.x > inventoryHud.position.x + inventoryHud.rect.width / 2 || iconRectTransform.position.x < inventoryHud.position.x - inventoryHud.rect.width / 2) &&
                (iconRectTransform.position.y > inventoryHudArmor.position.y + inventoryHudArmor.rect.height / 2 || iconRectTransform.position.y < inventoryHudArmor.position.y - inventoryHudArmor.rect.height / 2 || iconRectTransform.position.x > inventoryHudArmor.position.x + inventoryHudArmor.rect.width / 2 || iconRectTransform.position.x < inventoryHudArmor.position.x - inventoryHudArmor.rect.width / 2) &&
                (iconRectTransform.position.y > inventoryHudBottom.position.y + inventoryHudBottom.rect.height / 2 || iconRectTransform.position.y < inventoryHudBottom.position.y - inventoryHudBottom.rect.height / 2 || iconRectTransform.position.x > inventoryHudBottom.position.x + inventoryHudBottom.rect.width / 2 || iconRectTransform.position.x < inventoryHudBottom.position.x - inventoryHudBottom.rect.width / 2))
            {
                if (transform.GetComponent<Icon>().item.GetComponent<CellType>().cellType != CellType.Type.Quest)
                {
                    transform.GetComponent<Icon>().DropAllItem();
                }
                else
                {
                    transform.position = transform.GetComponent<Icon>().cell.transform.position;
                }
            }
            else
            {
                foreach (Cell cellinv in inventory.GetComponent<Inventory>().cells)
                {
                    if (cellinv.GetComponent<Collider2D>().Distance(GetComponent<Collider2D>()).distance < cellinv.GetComponent<RectTransform>().sizeDelta.x/4)
                    {
                        transform.GetComponent<Icon>().ChangeItemCell(cellinv);
                        ContextMenu.Show(transform.GetComponent<Icon>().name, transform.GetComponent<Icon>().itemType, transform.GetComponent<Icon>().description, transform.position);
                    }
                    else
                    {
                        transform.position = transform.GetComponent<Icon>().cell.transform.position;
                    }
                }
            }
        }
    }
}
