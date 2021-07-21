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
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        WaveManager.onWaveChanged += destroyEnemies_onWaveChanged;
        Player.onPlayerDeath += Player_onPlayerDeath;
    }
    void Start()
    {
        //WaveManager.onWaveChanged += destroyEnemies_onWaveChanged;
        //Player.onPlayerDeath += Player_onPlayerDeath;
    }


    // Update is called once per frame
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

    public void Player_onPlayerDeath(object sender, EventArgs e)
    {
        Debug.Log("game ended");
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmos()
    {
        if (debbuging)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(PlayrMovement.Position, levelUpExplosionRange);
        }
    }
}
