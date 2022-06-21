using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    void Update()
    {
        //Set size of HP bar to Player's HP 
        RectTransform rectTransform = GetComponent<RectTransform>();    
        rectTransform.sizeDelta = new Vector2(102* Player.HP/100, 10.44f);
    }
}
