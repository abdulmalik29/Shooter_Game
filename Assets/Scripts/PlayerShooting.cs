using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public bool debbuging = false;
	public Transform firePoint;
	public Weapon startCurrentWeapon;
	public static Weapon currentWeapon;

    private float nextTimeOfFire = 0f;

    private void Start()
    {
		if (debbuging)
        {
			currentWeapon = startCurrentWeapon;
        }
	}

    // Update is called once per frame
    void Update()
	{
		if (!debbuging)
			currentWeapon = WaveManager.currentWave.weapon;

		if (Input.GetButton("Fire1"))
		{
			if (Time.time >= nextTimeOfFire)
			{
				currentWeapon.Shoot(firePoint);
				nextTimeOfFire = Time.time + 1f / currentWeapon.fireRate;
			}
		}
	}


}
