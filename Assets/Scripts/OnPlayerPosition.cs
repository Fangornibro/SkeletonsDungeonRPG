using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerPosition : MonoBehaviour
{
    public GameObject player;
    private float Shaking;
    private float timeBtwShaking = 0.05f;

    void Update()
    {

        if (Player.HP <= 30)
        {
            if (timeBtwShaking <= 0)
            {
                Shaking = Random.value * 3 / (Player.HP);
                timeBtwShaking = 0.05f;
            }
            else
            {
                timeBtwShaking -= Time.deltaTime;
            }
        }
        else
        {
            Shaking = 0;
        }
        //Camera on Player
        transform.position = new Vector3(player.transform.position.x + Shaking, player.transform.position.y, transform.position.z);
    }
}
