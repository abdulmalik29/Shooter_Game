using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int maxHearts = 5;
    public static int currentHearts;

    public GameObject deathEffect;

    public static event EventHandler onPlayerDeath;

    //public static event Action onDamageTaken;
    //public static event Action onHeal;


    void Start()
    {
        WaveManager.onWaveChanged += heal_onWaveChanged;

        currentHearts = maxHearts;
        //Debug.Log("current Hearts: " + currentHearts);
    }

    private void Update()
    {
        if (currentHearts < 0)
        {
            Die();
        }
    }

    public static void takeDamage(int Damage = 1)
    {
        currentHearts -= Damage;
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        //effect.transform.localScale = transform.localScale;
        Destroy(gameObject);
        Destroy(effect, 10f);

        if (onPlayerDeath != null)
            onPlayerDeath(this, EventArgs.Empty);
    }

    public static void heal(int amount = 1)
    {
        if (currentHearts <= maxHearts)
            currentHearts += amount;
            //Debug.Log("current Hearts: " + currentHearts);
    }

    public static void increasHearts(int amount = 1)
    {
        maxHearts += amount;
        //Debug.Log("max Hearts: " + maxHearts);
    }


    void heal_onWaveChanged(object sender, EventArgs e)
    {
        currentHearts = maxHearts;
    }

}
