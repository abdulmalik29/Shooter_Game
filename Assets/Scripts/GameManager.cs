using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float levelUpExplosionRange;
    public bool debbuging;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        WaveManager.onWaveChanged += destroyEnemies_onWaveChanged;
    }


    // Update is called once per frame
    void Update()
    {
        position = PlayrMovement.Position;
    }
    private void destroyEnemies_onWaveChanged(object sender, EventArgs e)
    {
        StartCoroutine(WaitThenDestroy());
    }

    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().Die(false);
            }
        }

        //Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(position, levelUpExplosionRange);

        //foreach (Collider2D enemy in enemiesInRange)
        //{
        //    if (enemy.gameObject != null)
        //    {
        //        enemy.gameObject.GetComponent<Enemy>().Die(false);
        //    }
        //}
    }
    private void OnDrawGizmos()
    {
        if (debbuging)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(position, levelUpExplosionRange);
        }
    }
}
