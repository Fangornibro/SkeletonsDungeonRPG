using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerPosition : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        //Camera on Player
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
