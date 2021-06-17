using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int damage = 1;
    public int health = 10;
    public uint reward = 10;

    public GameObject deathEffect;



    //Player player;
    //Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Bullet.onAOE_Attack += WaveSpawner_onWaveChanged;
        

    }

    private void WaveSpawner_onWaveChanged(object sender, EventArgs e)
    {
        takeDamage(PlayerShooting.currentWeapon.AOE_damage);
    }

    void takeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Progression.Score += reward;
        //Debug.Log("Score: "+ Progression.Score);

        Destroy(this.gameObject);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            Die();
            Player.takeDamage(damage);
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(PlayerShooting.currentWeapon.damage);
        }
    }
}
