using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public uint reward = 10;
    public int damage = 10;
    public int health = 10;

    public GameObject deathEffect;

    //Player player;
    //Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void Die()
    {
        Progression.Score += reward;
        Debug.Log("Score: "+ Progression.Score);

        Destroy(this.gameObject);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            Die();
            Player.TakeDamage();

        }

        else if (collision.gameObject.tag == "Bullet")
        {
            Die();
        }
    }
}
