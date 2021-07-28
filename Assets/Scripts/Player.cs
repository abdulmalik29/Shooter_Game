using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHearts = 5;
    public int currentHearts;

    public GameObject deathEffect;

    public static event EventHandler onPlayerDeath;
    Boolean isTakingDamage = true;

    public static Player instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        AudioManager.instance.Stop("mainSong");
        AudioManager.instance.Play("mainSong");

        currentHearts = 3;
    }

    private void Update()
    {
        if (currentHearts <= 0)
        {
            Die();
        }
    }

    public void takeDamage(int Damage = 1)
    {
        if (isTakingDamage)
        {
            currentHearts -= Damage;
            StartCoroutine(stopTakingDamage());
        }
    }

    IEnumerator stopTakingDamage()
    {
        if (isTakingDamage)
        {
            isTakingDamage = false;
            yield return new WaitForSecondsRealtime(.4f);
            isTakingDamage = true;
        }

    }

    void Die()
    {
        //PlayFabManager.instance.UpdateLeaderboard(Progression.Score);
        AudioManager.instance.Play("playerDeathSound");
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(effect, 10f);
        if (onPlayerDeath != null)
            onPlayerDeath(this, EventArgs.Empty);
    }

    public void heal(int amount = 1)
    {
        if (currentHearts < maxHearts)
            currentHearts += amount;
    }

    public void fully_heal()
    {
        currentHearts = maxHearts;
    }
    public void increasMaxHearts(int amount = 1)
    {
        maxHearts += amount;
    }

}
