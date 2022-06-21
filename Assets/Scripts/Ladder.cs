using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool ifInArea = false;
    private Transform player, tpPoint;

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
            }
        }
    }
}
