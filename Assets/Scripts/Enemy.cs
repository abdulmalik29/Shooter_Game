using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int damage = 1;
    public int health = 10;
    public uint reward = 10;

    [Header("")]
    public bool splitOnDeath = false;
    public GameObject childrenPrefab;
    public float spawnRadius = 1.5f;

    [Header("")]
    public GameObject deathEffect;
    private float waveStrenth = 20;

    private RippleProcessor rp;

    void Start()
    {
        
        Bullet.onAOE_Attack += Bullet_onAOE_Attack;
        rp = Camera.main.GetComponent<RippleProcessor>();

    }

    private void Bullet_onAOE_Attack(object sender, EventArgs e)
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

        if (splitOnDeath && addScoreWhenKilled)
        {
            int numberOfChildren = Random.Range(2, 5);
            for (int i = 0; i < numberOfChildren; i++)
            {
                Vector2 spawnPos = transform.position;
                spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
                Instantiate(childrenPrefab, spawnPos, Quaternion.identity);
            }
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

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

    }
}
