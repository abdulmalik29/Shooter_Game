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
    //public static event Action onDamageTaken;
    //public static event Action onHeal;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        //WaveManager.onWaveChanged += heal_onWaveChanged;

        currentHearts = maxHearts;
        //Debug.Log("current Hearts: " + currentHearts);
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
            yield return new WaitForSecondsRealtime(.6f);
            isTakingDamage = true;
        }

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

    public void heal(int amount = 1)
    {
        if (currentHearts <= maxHearts)
            currentHearts += amount;
            //Debug.Log("current Hearts: " + currentHearts);
    }

    public void fully_heal()
    {
        currentHearts = maxHearts;
    }
    public void increasMaxHearts(int amount = 1)
    {
        maxHearts += amount;
        //Debug.Log("max Hearts: " + maxHearts);
    }

}
