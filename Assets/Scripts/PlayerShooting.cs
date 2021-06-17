using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
	public Weapon startCurrentWeapon;
	public static Weapon currentWeapon;

    private float nextTimeOfFire = 0f;

    private void Start()
    {
		currentWeapon = startCurrentWeapon;
	}

    // Update is called once per frame
    void Update()
	{
		//globalCurrentWeapon = currentWeapon;

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
