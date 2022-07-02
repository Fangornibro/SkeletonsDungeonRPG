using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector]
    public float x, y;
    public Icon icon;
    private void Start()
    {
        x = GetComponent<RectTransform>().localPosition.x ;
        y = GetComponent<RectTransform>().localPosition.y;
        icon = null;
    }
}
