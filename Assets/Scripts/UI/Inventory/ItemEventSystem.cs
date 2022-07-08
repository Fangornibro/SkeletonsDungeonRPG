using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEventSystem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject iconPrefab;
    private GameObject inventory, DropButtonGO, UseButtonGO;
    private RectTransform inventoryHud, inventoryHudArmor, inventoryHudBottom, iconRectTransform;
    public static bool canPlayerShoot = true;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventoryHud = inventory.transform.Find("InventoryHud").gameObject.GetComponent<RectTransform>();
        inventoryHudArmor = inventory.transform.Find("InventoryHudArmor").gameObject.GetComponent<RectTransform>();
        inventoryHudBottom = inventory.transform.Find("VisibleInventory").Find("InventoryHudBottom").gameObject.GetComponent<RectTransform>();
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
                SelectionContextMenu.Show();
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
            if ((iconRectTransform.localPosition.y > inventoryHud.localPosition.y + inventoryHud.rect.height / 2 || iconRectTransform.localPosition.y < inventoryHud.localPosition.y - inventoryHud.rect.height / 2 || iconRectTransform.localPosition.x > inventoryHud.localPosition.x + inventoryHud.rect.width / 2 || iconRectTransform.localPosition.x < inventoryHud.localPosition.x - inventoryHud.rect.width / 2) &&
                (iconRectTransform.localPosition.y > inventoryHudArmor.localPosition.y + inventoryHudArmor.rect.height / 2 || iconRectTransform.localPosition.y < inventoryHudArmor.localPosition.y - inventoryHudArmor.rect.height / 2 || iconRectTransform.localPosition.x > inventoryHudArmor.localPosition.x + inventoryHudArmor.rect.width / 2 || iconRectTransform.localPosition.x < inventoryHudArmor.localPosition.x - inventoryHudArmor.rect.width / 2) &&
                (iconRectTransform.localPosition.y > inventoryHudBottom.localPosition.y + inventoryHudBottom.rect.height / 2 || iconRectTransform.localPosition.y < inventoryHudBottom.localPosition.y - inventoryHudBottom.rect.height / 2 || iconRectTransform.localPosition.x > inventoryHudBottom.localPosition.x + inventoryHudBottom.rect.width / 2 || iconRectTransform.localPosition.x < inventoryHudBottom.localPosition.x - inventoryHudBottom.rect.width / 2))
            {
                transform.GetComponent<Icon>().DropItem();
            }
            else
            {
                foreach (Cell cellinv in inventory.GetComponent<Inventory>().cells)
                {
                    if (cellinv.GetComponent<Collider2D>().Distance(GetComponent<Collider2D>()).distance < 40 && (cellinv.GetComponent<CellType>().cellType == GetComponent<Icon>().item.GetComponent<CellType>().cellType || cellinv.GetComponent<CellType>().cellType == CellType.Type.Everything))
                    {
                        if (cellinv.icon != null)
                        {
                            cellinv.icon.transform.position = transform.position;
                        }
                        transform.GetComponent<Icon>().ChangeItemCell(cellinv);
                        transform.position = transform.GetComponent<Icon>().cell.transform.position;
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
