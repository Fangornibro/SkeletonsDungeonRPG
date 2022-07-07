using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseButton : MonoBehaviour
{
    private Button button;
    private Image image;
    [HideInInspector]
    public bool isUsable;
    private Color startColor;
    [HideInInspector]
    public Transform Icon;
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        startColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        isUsable = Icon.GetComponent<Icon>().item.GetComponent<CellType>().cellType == CellType.Type.Usable;
        if (isUsable)
        {
            button.enabled = true;
            image.color = startColor;
        }
        else
        {
            button.enabled = false;
            image.color = new Color(0.1226415f, 0.1226415f, 0.1226415f, 0.7843137f);
        }
    }
}
