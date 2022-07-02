using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HPBar : MonoBehaviour
{
    public Transform playerLight;
    private void Start()
    {
        playerLight = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerLight").transform;
    }
    void Update()
    {
        //Set size of HP bar to Player's HP 
        RectTransform rectTransform = GetComponent<RectTransform>();    
        rectTransform.sizeDelta = new Vector2(102* Player.HP/100, 10.44f);

        playerLight.GetComponent<Light2D>().color = new Color(1f, Player.HP / 100f, Player.HP / 100f);
    }
}
