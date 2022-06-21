using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public Animator animatorTop, animatorBottom;
    bool IsDamaged;
    float animationTime = 0.2f;
    public Collider2D Player;
    private CircleCollider2D ShotArea, MoveArea;
    private float timeBtwShots;
    public float startTimeBtwShots, speed;
    public GameObject bullet;
    float RightScalexTop, LeftScalexTop, RightScalexBottom, LeftScalexBottom;
    private SpriteRenderer HPBar;

    private void Start()
    {
        RightScalexTop = transform.Find("EnemyTop").transform.localScale.x;
        LeftScalexTop = transform.Find("EnemyTop").transform.localScale.x * -1;
        RightScalexBottom = transform.Find("EnemyBottom").transform.localScale.x;
        LeftScalexBottom = transform.Find("EnemyBottom").transform.localScale.x * -1;
        ShotArea = transform.GetChild(2).GetComponent<CircleCollider2D>();
        MoveArea = transform.GetChild(3).GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(Player, GetComponent<Collider2D>());
        HPBar = transform.Find("EnemyHPBarFront").GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        HPBar.size = new Vector2(health / 100f * 1.312f, 0.125f);
        //Enemy death
        if (health == 0)
        {
            Destroy(gameObject);
        }
        //Enemy damaged
        if (IsDamaged)
        {
            animationTime -= Time.deltaTime;
            if (animationTime <= 0)
            {
                IsDamaged = false;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
                animationTime = 0.2f;
            }
        }

        //Enemy attack
        if (Player.Distance(ShotArea).distance <= 0)
        {
            Vector3 difference = Player.transform.position - transform.position;
            float rot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (timeBtwShots <= 0)
            {
                animatorTop.SetBool("IsAttacking", false);
                Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, rot - 90));
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                animatorTop.SetBool("IsAttacking", true);
                timeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            animatorTop.SetBool("IsAttacking", false);
        }
    }
    
    public void TakeDamage(int damage)
    {
        IsDamaged = true;
        health -= damage;
        transform.Find("EnemyTop").GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
        transform.Find("EnemyBottom").GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
    }

    private void FixedUpdate()
    {
        //Enemy movement
        if (MoveArea.Distance(Player).distance <= 0)
        {
            if (ShotArea.Distance(Player).distance >= -2)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                animatorBottom.SetBool("IsRunning", true);
                animatorTop.SetBool("IsRunning", true);
            }
            else
            {
                animatorBottom.SetBool("IsRunning", false);
                animatorTop.SetBool("IsRunning", false);
            }
            if (Player.transform.position.x > transform.position.x)
            {
                transform.Find("EnemyTop").transform.localScale = new Vector2(RightScalexTop, transform.Find("EnemyTop").transform.localScale.y);
                transform.Find("EnemyBottom").transform.localScale = new Vector2(RightScalexBottom, transform.Find("EnemyTop").transform.localScale.y);
            }
            else
            {
                transform.Find("EnemyTop").transform.localScale = new Vector2(LeftScalexTop, transform.Find("EnemyTop").transform.localScale.y);
                transform.Find("EnemyBottom").transform.localScale = new Vector2(LeftScalexBottom, transform.Find("EnemyTop").transform.localScale.y);
            }
        }
        else
        {
            animatorBottom.SetBool("IsRunning", false);
        }

    }
}
