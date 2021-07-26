using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool debbuging;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        WaveManager.onWaveChanged += destroyEnemies_onWaveChanged;
        Player.onPlayerDeath += Player_onPlayerDeath;
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
                yield return new WaitForSeconds(0.1f);

            }
        }

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(bullet);
        }
    }

    public void Player_onPlayerDeath(object sender, EventArgs e)
    {
        //Debug.Log("game ended");
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        AudioManager.instance.Stop("mainSong");

        yield return new WaitForSecondsRealtime(2.35f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

/*    private void OnDrawGizmos()
    {
    }*/
}
