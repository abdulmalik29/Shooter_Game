using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Bullet : MonoBehaviour
{
    public bool isEnemyBullet = false;
    public int damage = 1;
    public float speed = 10f;

    public bool debbuging = true;
    [Header("Hit Effect")]
    public GameObject hitEffect;
    public float hitEffectDuration = 4f;

    [Header("AOE Damage")]
    public bool hasAOE_damage;
    public int AOE_maxTargets = 0;
    public float AOE_range;

    public enum MyEnum
    {
        myEnum1,
        myEnum2,
        myEnum3
    };

    public static event EventHandler onAOE_Attack;
    private Rigidbody2D rb;
    public MyEnum s;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        rb.rotation = PlayrMovement.playerAngle+90;

        if (isEnemyBullet)
            rb.AddTorque(5f, ForceMode2D.Impulse);

        s = MyEnum.myEnum1;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isEnemyBullet)
        {
            if (collision.gameObject.tag != "Enemy")
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, hitEffectDuration);
                Destroy(gameObject);

                Player.takeDamage(damage);
            }
        }
        else 
        { 
            if (collision.gameObject.tag != "Player")
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, hitEffectDuration);
                Destroy(gameObject);

                if (hasAOE_damage)
                {
                    // get any colliders in range and on the layermask
                    Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, AOE_range);


                    // this is a list that we will populate with enemies in range
                    List<GameObject> enemiesToHit = new List<GameObject>();

                    // populate the "enemiesToHit" list, will only add up to "maxTargets" number of objects
                    for (int i = 0; i < AOE_maxTargets; i++)
                    {
                        // make sure "i" doesn't go past the number of enemies in range
                        if (i < enemiesInRange.Length)
                        {
                            enemiesToHit.Add(enemiesInRange[i].gameObject);
                        }
                        else
                        {
                            // stop the loop if there are no more hits
                            break;
                        }
                    }

                    foreach (GameObject enemy in enemiesToHit)
                    {
                        if (!GameObject.ReferenceEquals(enemy, collision.gameObject))
                        {
                            // do damage 
                            if (onAOE_Attack != null)
                                onAOE_Attack(this, EventArgs.Empty);
                        }

                        //showAOE_Atttack(enemy.transform);

                    }
                }
            }
        }

    }

    void showAOE_Atttack(Transform target)
    {
        //LineController newLine = Instantiate(AOE_prefap);
        //newLine.AssainTarget(transform.position, target);
    }

    private void OnDrawGizmos()
    {
        if (debbuging)
        {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AOE_range);
        }
    }
}
