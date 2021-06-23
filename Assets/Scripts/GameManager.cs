using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WaveManager.onWaveChanged += killEnemies_onWaveChanged;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void killEnemies_onWaveChanged(object sender, EventArgs e)
    {
        StartCoroutine(WaitAndKillEnemies());
    }

    IEnumerator WaitAndKillEnemies()
    {
        Debug.Log("Before Waitng ");
        yield return new WaitForSeconds(1f);
        Debug.Log("Waitng ");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().Die(false);
        }
    }
}
