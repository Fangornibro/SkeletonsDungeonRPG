using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject floorToShow, floorToHide;
    [HideInInspector]
    public bool ifInArea = false;
    private Transform player;
    private Transform tpPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tpPoint = transform.Find("tpPoint").transform;
    }
    void Update()
    {
        if (ifInArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.position = tpPoint.position;
                floorToHide.SetActive(false);
                floorToShow.SetActive(true);
            }
        }
    }
}
