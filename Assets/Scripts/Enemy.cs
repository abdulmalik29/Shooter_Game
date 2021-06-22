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
    public GameObject damageFloatingText;

    private Color color = new Color(255f/255f, 103f/255f, 0f/255f); // orange color
    private RippleProcessor rp;



    //Player player;
    //Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //WaveManager.onWaveChanged += die_onWaveChanged;
        Bullet.onAOE_Attack += WaveSpawner_onWaveChanged;
        rp = Camera.main.GetComponent<RippleProcessor>();

    }

    private void die_onWaveChanged(object sender, EventArgs e)
    {
        Die(false);
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
            Die();
            color = Color.red;
        }

        GameObject popUp = Instantiate(damageFloatingText, transform.position, Quaternion.identity);
        popUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = color;
        popUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmg.ToString();
    }

    void Die(bool addScoreWhenKilled = true)
    {
        if (addScoreWhenKilled)
        {
            Progression.Score += reward;
        }
        //Debug.Log("Score: "+ Progression.Score);

        Destroy(this.gameObject);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);

        rp.MaxAmount = waveStrenth;
        rp.Ripple(PlayrMovement.Position);

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
