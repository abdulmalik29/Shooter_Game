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
	private ArrayList wavesList = new ArrayList();

	public static int currentWaveNum;
	public static Wave currentWave;

	private float nextSpawnTime = 1f;

    private void Awake()
    {

        for (int i = 0; i < waves.Length; i++)
        {
			wavesList.Add(waves[i]);
		}

		//Debug.Log(wavesList.Count);

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

		if (currentWaveNum < wavesList.Count)
		{
			currentWave = (Wave) wavesList[currentWaveNum];
		}

        if (wavesList.Count - currentWaveNum == 1)
        {
            Debug.Log("There is only one wavew left");

            ulong newScoreGate = (currentWave.scoreGate * 2) + 50;
            float newSpawnPerSecond = (currentWave.spawnPerSecond) * 0.9f;

            Wave newwave = new Wave(newScoreGate, currentWave.weapon, newSpawnPerSecond, currentWave.enemies);
            wavesList.Add(newwave);
        }

        spawnRadius = startSpawnRadius /* * Progression.Growth*/;

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


	void CreateNextWave()
    {

    }

	IEnumerator WaitThenChangeWave()
	{
		if (isCoroutineExecuting)
			yield break;

		isCoroutineExecuting = true;

		yield return new WaitForSecondsRealtime(.6f);
		
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

