using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> floors = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if (i != 0)
            {
                floors[i].SetActive(false);
            }
        }
    }
}
