using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WaveManager.onWaveChanged += destroyEnemies_onWaveChanged;
    }


    // Update is called once per frame
    void Update()
    {
        
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
            enemy.GetComponent<Enemy>().Die(false);
        }
    }
}
