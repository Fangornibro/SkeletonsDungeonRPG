using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed, lifetime, distance;
    public int damage;
    public LayerMask playerMask, whatIsSolid;
    private Transform target;
    private CircleCollider2D PlayerHomingArea;
    private Collider2D EnemyBulletCollider;
    private bool isHoming = true;
    private void Start()
    {
        //Set the target
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //Set the Homing area
        PlayerHomingArea = GameObject.FindGameObjectWithTag("Player").transform.Find("HomingArea").GetComponent<CircleCollider2D>();
        //Set the Bullet collider
        EnemyBulletCollider = transform.GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        if (!Pause.pauseOn)
        {
            //Remove the homing if the bullet flies close to the Player
            if (PlayerHomingArea.Distance(EnemyBulletCollider).distance <= 0)
            {
                isHoming = false;
            }
            //Set the bullet homing on Player
            if (isHoming)
            {
                Vector3 difference = target.transform.position - transform.position;
                float rot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot - 90);
            }

            //Ability to hit the player
            RaycastHit2D hitinfo1 = Physics2D.Raycast(transform.position, transform.up, distance, playerMask);
            if (hitinfo1.collider != null)
            {
                target.GetComponent<Player>().takeDamage(10);
                Destroy(gameObject);
            }

            //Ability to hit Walls
            RaycastHit2D hitinfo2 = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
            if (hitinfo2.collider != null)
            {
                Destroy(gameObject);
            }

            //Bullet movement
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            //Set lifetime
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
