using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
	public GameObject bulletPrefab;
	public float fireRate;
	public int damage = 20;
	public String soundName = "";
	//public bool shootsRaycasts = false;

	[Header("AOE Damage")]
	public int AOE_damage = 0;

	public void Shoot(Transform firePoint)
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

		//if (soundName != "")
		//{
			//AudioManager.instance.Play(soundName);
		FindObjectOfType<AudioManager>().Play(soundName);
		//}

		Destroy(bullet, 10f);
    }
	IEnumerator setActive(GameObject bullet)
	{
		yield return new WaitForSeconds(.1f);
		bullet.SetActive(true);
    }
}