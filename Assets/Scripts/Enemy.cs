using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health;
    public Animator animatorTop, animatorBottom;
    public float startTimeBtwShots, speed;
    public GameObject bullet;
    public Transform shotPoint;
    public enum EnemyClass { Melee, Range }
    public EnemyClass enemyClass;


    private float timeBtwShots;
    private bool isDamaged;
    private float animationTime = 0.2f;
    public Collider2D player;
    private Collider2D shotArea, moveArea;
    private float rightScalexTop, leftScalexTop, rightScalexBottom, leftScalexBottom;
    private SpriteRenderer hpBar;
    private NavMeshAgent navMeshAgent;
    private Vector2 curTarget;
    [HideInInspector]
    public Collider2D[] allSelectableItems;
    [HideInInspector]
    public LayerMask selectedItemLayerMask;

    private void Start()
    {
        rightScalexTop = transform.Find("EnemyTop").transform.localScale.x;
        leftScalexTop = transform.Find("EnemyTop").transform.localScale.x * -1;
        rightScalexBottom = transform.Find("EnemyBottom").transform.localScale.x;
        leftScalexBottom = transform.Find("EnemyBottom").transform.localScale.x * -1;
        shotArea = transform.Find("ShotArea").GetComponent<Collider2D>();
        moveArea = transform.Find("MoveArea").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(player, GetComponent<Collider2D>());
        hpBar = transform.Find("EnemyHPBarFront").GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        curTarget = transform.position;

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    private void Update()
    {
        if (!Pause.pauseOn)
        {
            hpBar.size = new Vector2(health / 100f * 1.312f, 0.125f);
            //Enemy death
            if (health == 0)
            {
                Destroy(gameObject);
            }
            //Enemy damaged
            if (isDamaged)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    transform.Find("EnemyTop").transform.localScale = new Vector2(rightScalexTop, transform.Find("EnemyTop").transform.localScale.y);
                    transform.Find("EnemyBottom").transform.localScale = new Vector2(rightScalexBottom, transform.Find("EnemyTop").transform.localScale.y);
                }
                else
                {
                    transform.Find("EnemyTop").transform.localScale = new Vector2(leftScalexTop, transform.Find("EnemyTop").transform.localScale.y);
                    transform.Find("EnemyBottom").transform.localScale = new Vector2(leftScalexBottom, transform.Find("EnemyTop").transform.localScale.y);
                }
                curTarget = player.transform.position;
                animationTime -= Time.deltaTime;
                if (animationTime <= 0)
                {
                    isDamaged = false;
                    transform.Find("EnemyTop").GetComponent<SpriteRenderer>().color = Color.white;
                    transform.Find("EnemyBottom").GetComponent<SpriteRenderer>().color = Color.white;
                    animationTime = 0.2f;
                }
            }

            //Enemy attack
            if (player.Distance(shotArea).distance <= 0)
            {
                Vector3 difference = player.transform.position - transform.position;
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
    }
    
    public void TakeDamage(int damage)
    {
        isDamaged = true;
        health -= damage;
        transform.Find("EnemyTop").GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
        transform.Find("EnemyBottom").GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
    }

    private void FixedUpdate()
    {
        if (!Pause.pauseOn)
        {
            //Enemy rotation(left right side)
            navMeshAgent.SetDestination(curTarget);
            if (moveArea.Distance(player).distance <= 0)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    transform.Find("EnemyTop").transform.localScale = new Vector2(rightScalexTop, transform.Find("EnemyTop").transform.localScale.y);
                    transform.Find("EnemyBottom").transform.localScale = new Vector2(rightScalexBottom, transform.Find("EnemyTop").transform.localScale.y);
                }
                else
                {
                    transform.Find("EnemyTop").transform.localScale = new Vector2(leftScalexTop, transform.Find("EnemyTop").transform.localScale.y);
                    transform.Find("EnemyBottom").transform.localScale = new Vector2(leftScalexBottom, transform.Find("EnemyTop").transform.localScale.y);
                }
            }

            //Melee enemy movement
            if (enemyClass == EnemyClass.Melee)
            {
                if (moveArea.Distance(player).distance <= 0)
                {
                    curTarget = player.transform.position;
                    animatorBottom.SetBool("IsRunning", true);
                    animatorTop.SetBool("IsRunning", true);
                }
                else
                {
                    animatorBottom.SetBool("IsRunning", false);
                }
            }
            //Range enemy movement
            else if (enemyClass == EnemyClass.Range)
            {
                if (moveArea.Distance(player).distance <= 0)
                {
                    if (shotArea.Distance(player).distance >= -2)
                    {
                        curTarget = player.transform.position;
                        animatorBottom.SetBool("IsRunning", true);
                        animatorTop.SetBool("IsRunning", true);
                    }
                    else
                    {
                        curTarget = transform.position;
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
        else
        {
            animatorBottom.SetBool("IsRunning", false);
            navMeshAgent.SetDestination(transform.position);
        }
    }
}
