using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
	public Weapon currentWeapon;

	private float nextTimeOfFire = 0f;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			if (Time.time >= nextTimeOfFire)
			{
				currentWeapon.Shoot(firePoint);
				nextTimeOfFire = Time.time + 1f / currentWeapon.fireRate;
			}
		}
	}

/*	void Shoot1()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }*/

}
