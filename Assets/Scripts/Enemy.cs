using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int damage = 1;
    public int health = 10;
    public uint reward = 10;

    private float waveStrenth = 20;
    public GameObject deathEffect;

    private RippleProcessor rp;

    void Start()
    {
        
        Bullet.onAOE_Attack += WaveSpawner_onWaveChanged;
        rp = Camera.main.GetComponent<RippleProcessor>();

    }

    private void WaveSpawner_onWaveChanged(object sender, EventArgs e)
    {
        takeDamage(PlayerShooting.currentWeapon.AOE_damage);
    }

    void takeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die(true);
        }

    }

    public void Die(bool addScoreWhenKilled = true)
    {
        if (addScoreWhenKilled)
        {
            Progression.Score += reward;
        }
        //Debug.Log("Score: "+ Progression.Score);
        if (gameObject != null)
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 4f);
            rp.MaxAmount = waveStrenth;
            rp.Ripple(transform.position);
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            Die();
            Player.takeDamage(damage);
        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            takeDamage(PlayerShooting.currentWeapon.damage);
        }
    }
}
