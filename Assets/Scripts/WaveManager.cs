using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{

	public static event EventHandler onWaveChanged;

	public float startSpawnRadius = 10f;
	private float spawnRadius;

	public Wave[] waves;

	private float nextSpawnTime = 1f;

    void Start()
    {
		

	}


    void Update()
	{

		/*		if (Progression.IsGrowing)
					return;*/
		if (Progression.currentWaveNum < waves.Length)
		{
			Progression.currentWave = waves[Progression.currentWaveNum];
		}
		
		spawnRadius = startSpawnRadius; /** Progression.Growth;*/

		if (Progression.Score  < Progression.currentWave.scoreGate)
        {
			if (Time.time >= nextSpawnTime)
			{
				SpawnWave();
				nextSpawnTime = Time.time + 1f / Progression.currentWave.spawnPerSecond;
			}
			return;
		}

		Progression.currentWaveNum++;
		if (onWaveChanged != null)
			onWaveChanged(this, EventArgs.Empty);


	}

	void SpawnWave()
	{
		foreach (EnemyType eType in Progression.currentWave.enemies)
		{
			if (Random.value <= eType.spawnChance)
			{
				SpawnEnemy(eType.enemyPrefab);
			}
		}
	}

	void SpawnEnemy(GameObject enemyPrefab)
	{
		Vector2 spawnPos = PlayrMovement.Position;
		spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

		Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
	}

}

