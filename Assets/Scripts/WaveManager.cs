using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{

	public static event EventHandler onWaveChanged;

	public Boolean Debugging;
	public int DebuggingWaveNumber = -1;

	private Boolean isSpawnerOn;
	private Boolean isCoroutineExecuting = false;

	[Header(" ")] 
	public float startSpawnRadius = 10f;
	private float spawnRadius;

	public Wave[] waves;

	public static int currentWaveNum;
	public static Wave currentWave;

	private float nextSpawnTime = 1f;

    private void Awake()
    {
		if (DebuggingWaveNumber == -1)
        {
			currentWaveNum = 0;
        }
        else
        {
			currentWaveNum = DebuggingWaveNumber;
		}

		isSpawnerOn = false;

	}

    private void Start()
    {
		StartCoroutine(startSpawner());
    }

    void Update()
	{

		/*		if (Progression.IsGrowing)
					return;*/
		if (currentWaveNum < waves.Length)
		{
			currentWave = waves[currentWaveNum];
		}
		
		spawnRadius = startSpawnRadius * Progression.Growth;

		if (Progression.Score  < currentWave.scoreGate)
        {
			if (Time.time >= nextSpawnTime && !Debugging)
			{
                if (isSpawnerOn)
				{
					SpawnWave();
					nextSpawnTime = Time.time + 1f / currentWave.spawnPerSecond;
				}
            }
			return;
		}
        else
        {
			StartCoroutine(WaitThenChangeWave());
        }

	}

	void SpawnWave()
	{
		foreach (EnemyType eType in currentWave.enemies)
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


	
	
	IEnumerator WaitThenChangeWave()
	{
		if (isCoroutineExecuting)
			yield break;

		isCoroutineExecuting = true;

		yield return new WaitForSecondsRealtime(.9f);
		
		if (onWaveChanged != null)
			onWaveChanged(this, EventArgs.Empty);

		float oldSpeed = PlayrMovement.movementSpeed;
		PlayrMovement.movementSpeed = 3;

		yield return new WaitForSecondsRealtime(3.85f);
		currentWaveNum++;

		PlayrMovement.movementSpeed = oldSpeed;

		isCoroutineExecuting = false;

	}

	IEnumerator startSpawner()
	{
		yield return new WaitForSeconds(1);
		isSpawnerOn = true;

	}

}

