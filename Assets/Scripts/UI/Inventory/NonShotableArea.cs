using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NonShotableArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool canPlayerShoot = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        canPlayerShoot = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canPlayerShoot = true;
    }
}
