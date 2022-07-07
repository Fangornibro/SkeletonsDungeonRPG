using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput, moveVelocity;
    public Animator animatorBody, animatorHead, animatorLegs;
    float RightScalexBody, LeftScalexBody, RightScalexHead, LeftScalexHead, RightScalexLegs, LeftScalexLegs;
    public GameObject bullet;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public static float HP = 100;
    public static bool canShoot = true;
    public AudioSource stepsSound;

    bool IsDamaged = false;
    float timeBetweenDamage = 0.5f;
    SpriteRenderer spriteRendererHead;
    SpriteRenderer spriteRendererBody;
    SpriteRenderer spriteRendererLegs;
    void Start()
    {
        spriteRendererHead = transform.Find("PlayerHead").GetComponent<SpriteRenderer>();
        spriteRendererBody = transform.Find("PlayerBody").GetComponent<SpriteRenderer>();
        spriteRendererLegs = transform.Find("PlayerLegs").GetComponent<SpriteRenderer>();
        //Set rigidbody
        rb = GetComponent<Rigidbody2D>();
        //Body right turn
        RightScalexBody = transform.Find("PlayerBody").localScale.x;
        //Body left turn
        LeftScalexBody = transform.Find("PlayerBody").localScale.x * -1;
        //Head right turn
        RightScalexHead = transform.Find("PlayerHead").localScale.x;
        //Head left turn
        LeftScalexHead = transform.Find("PlayerHead").localScale.x * -1;
        //Legs right turn
        RightScalexLegs = transform.Find("PlayerLegs").localScale.x;
        //Legs left turn
        LeftScalexLegs = transform.Find("PlayerLegs").localScale.x * -1;
    }

    void Update()
    {
        if (!Pause.pauseOn)
        {
            //Move velocity calculation
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            moveVelocity = moveInput * speed;
            //Running animation
            if (moveInput != Vector2.zero)
            {
                animatorLegs.SetBool("IsRunning", true);
                if (!stepsSound.isPlaying)
                {
                    stepsSound.Play();
                }
            }
            else
            {
                stepsSound.Stop();
                animatorLegs.SetBool("IsRunning", false);
            }
            //Player's body & legs turn (right or left)
            if (moveInput.x > 0)
            {
                transform.Find("PlayerBody").localScale = new Vector2(RightScalexBody, transform.Find("PlayerBody").localScale.y);
                transform.Find("PlayerLegs").localScale = new Vector2(RightScalexLegs, transform.Find("PlayerLegs").localScale.y);
            }
            else if (moveInput.x < 0)
            {
                transform.Find("PlayerBody").localScale = new Vector2(LeftScalexBody, transform.Find("PlayerBody").localScale.y);
                transform.Find("PlayerLegs").localScale = new Vector2(LeftScalexLegs, transform.Find("PlayerLegs").localScale.y);
            }
            //Player's head turn (right or left)
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                transform.Find("PlayerHead").localScale = new Vector2(RightScalexHead, transform.Find("PlayerHead").localScale.y);
            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            {
                transform.Find("PlayerHead").localScale = new Vector2(LeftScalexHead, transform.Find("PlayerHead").localScale.y);
            }
            //Player's head turn (top or bottom)
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > transform.position.y)
            {
                animatorHead.SetBool("IsTop", true);
            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y)
            {
                animatorHead.SetBool("IsTop", false);
            }
            //Shot angle
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //Attacking
            if (ItemEventSystem.canPlayerShoot && NonShotableArea.canPlayerShoot && ItemDropButton.canPlayerShoot)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }
            if (canShoot)
            {
                if (timeBtwShots <= 0)
                {
                    animatorBody.SetBool("IsAttacking", false);
                    if (Input.GetMouseButton(0))
                    {
                        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, rot - 90));
                        timeBtwShots = startTimeBtwShots;
                    }
                }
                else
                {
                    animatorBody.SetBool("IsAttacking", true);
                    timeBtwShots -= Time.deltaTime;
                }
            }
            //Player death
            if (HP <= 0)
            {
                Destroy(gameObject);
            }

            //Player invincible 
            if (IsDamaged)
            {
                timeBetweenDamage -= Time.deltaTime;
                if (timeBetweenDamage <= 0)
                {
                    IsDamaged = false;
                    spriteRendererHead.color = Color.white;
                    spriteRendererBody.color = Color.white;
                    spriteRendererLegs.color = Color.white;
                    timeBetweenDamage = 0.5f;
                }
            }
        }
        else
        {
            animatorLegs.SetBool("IsRunning", false);
        }
    }

    private void FixedUpdate()
    {
        if (!Pause.pauseOn)
        {
            //Player movement
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }

    public void takeDamage(int damage)
    {
        if (!IsDamaged)
        {
            IsDamaged = true;
            HP -= damage;
            spriteRendererHead.color = new Color(1, 1, 1, 0.5f);
            spriteRendererBody.color = new Color(1, 1, 1, 0.5f);
            spriteRendererLegs.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
