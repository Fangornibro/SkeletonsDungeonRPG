using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, lifetime, distance;
    public int damage;
    public LayerMask whatIsSolid;
    public ParticleSystem particleSystem;
    void Update()
    {
        if (!Pause.pauseOn)
        {
            //Ability to hit solid objects
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
            if (hitinfo.collider != null)
            {
                if (hitinfo.collider.CompareTag("Enemy"))
                {
                    hitinfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                }
                Instantiate(particleSystem, transform.position, Quaternion.Euler(0, 0, 0));
                particleSystem.Play();
                Destroy(gameObject);
            }
            //Bullet movement
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            //Lifetime
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Instantiate(particleSystem, transform.position, Quaternion.Euler(0, 0, 0));
                particleSystem.Play();
                Destroy(gameObject);
            }
        }
    }
}
