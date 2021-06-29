using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
	public ulong  scoreGate;
	public Weapon weapon;
	public float spawnPerSecond = 1f;
	public EnemyType[] enemies;

}
