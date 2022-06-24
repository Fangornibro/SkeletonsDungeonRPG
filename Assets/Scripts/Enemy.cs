using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health;
    public Animator animatorTop, animatorBottom;
    bool IsDamaged;
    float animationTime = 0.2f;
    public Collider2D Player;
    private Collider2D ShotArea, MoveArea;
    private float timeBtwShots;
    public float startTimeBtwShots, speed;
    public GameObject bullet;
    float RightScalexTop, LeftScalexTop, RightScalexBottom, LeftScalexBottom;
    private SpriteRenderer HPBar;
    private NavMeshAgent navMeshAgent;

    public Collider2D[] allSelectableItems;
    public LayerMask selectedItemLayerMask;

    public enum EnemyClass { Melee, Range }

    public EnemyClass enemyClass;

    private void Start()
    {
        RightScalexTop = transform.Find("EnemyTop").transform.localScale.x;
        LeftScalexTop = transform.Find("EnemyTop").transform.localScale.x * -1;
        RightScalexBottom = transform.Find("EnemyBottom").transform.localScale.x;
        LeftScalexBottom = transform.Find("EnemyBottom").transform.localScale.x * -1;
        ShotArea = transform.Find("ShotArea").GetComponent<Collider2D>();
        MoveArea = transform.Find("MoveArea").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(Player, GetComponent<Collider2D>());
        HPBar = transform.Find("EnemyHPBarFront").GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
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
            navMeshAgent.SetDestination(Player.transform.position);
            animationTime -= Time.deltaTime;
            if (animationTime <= 0)
            {
                IsDamaged = false;
                transform.Find("EnemyTop").GetComponent<SpriteRenderer>().color = Color.white;
                transform.Find("EnemyBottom").GetComponent<SpriteRenderer>().color = Color.white;
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

        //Gate oppening
        allSelectableItems = Physics2D.OverlapCircleAll(transform.position, 1, selectedItemLayerMask);

        foreach (Collider2D item in allSelectableItems)
        {
            if (item.GetComponentInParent<Gate>() != null)
            {
                item.GetComponentInParent<Gate>().ifEnemyInArea = true;
            }
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
        if (MoveArea.Distance(Player).distance <= 0)
        {
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
        if (enemyClass == EnemyClass.Melee)
        {
            if (MoveArea.Distance(Player).distance <= 0)
            {
                navMeshAgent.SetDestination(Player.transform.position);
                animatorBottom.SetBool("IsRunning", true);
                animatorTop.SetBool("IsRunning", true);
            }
            else
            {
                animatorBottom.SetBool("IsRunning", false);
            }
        }
        else if (enemyClass == EnemyClass.Range)
        {
            if (MoveArea.Distance(Player).distance <= 0)
            {
                if (ShotArea.Distance(Player).distance >= -2)
                {
                    navMeshAgent.SetDestination(Player.transform.position);
                    animatorBottom.SetBool("IsRunning", true);
                    animatorTop.SetBool("IsRunning", true);
                }
                else
                {
                    animatorBottom.SetBool("IsRunning", false);
                    animatorTop.SetBool("IsRunning", false);
                }
            }
            else
            {
                animatorBottom.SetBool("IsRunning", false);
            }
        } 
    }
}
