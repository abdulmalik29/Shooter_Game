using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public bool isEnemyBullet = false;

    public float speed = 10f;


    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        if (isEnemyBullet)
        {
            rb.AddTorque(5f, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isEnemyBullet)
        {
            if (collision.gameObject.tag != "Enemy")
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 4f);
                Destroy(gameObject);
            }
        }
        else 
        { 
            if (collision.gameObject.tag != "Player")
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 4f);
                Destroy(gameObject);
            }
        }

    }
}
