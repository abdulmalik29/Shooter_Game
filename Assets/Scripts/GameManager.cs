using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public float levelUpExplosionRange;
    public bool debbuging;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        WaveManager.onWaveChanged += destroyEnemies_onWaveChanged;
        Player.onPlayerDeath += Player_onPlayerDeath;
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

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(bullet);
        }
    }
    private void OnDrawGizmos()
    {
        if (debbuging)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(position, levelUpExplosionRange);
        }
    }

    public void Player_onPlayerDeath(object sender, EventArgs e)
    {
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
