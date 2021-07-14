using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
	public ulong  scoreGate;
	public Weapon weapon;
	public float spawnPerSecond = 1f;
	public EnemyType[] enemies;


    public Wave(ulong _scoreGate, Weapon _weapon, float _spawnPerSecond, EnemyType[] _enemies)
    {
        scoreGate = _scoreGate;
        weapon = _weapon;
        spawnPerSecond = _spawnPerSecond;
        enemies = _enemies;
    }
/*
    public Wave()
    {

    }*/
}
