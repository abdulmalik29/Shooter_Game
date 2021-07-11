using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
	public enum EnemyType
	{
		shooter,
		basic,
		circler
	};

	private static List<Rigidbody2D> EnemyRBs;

	public EnemyType eType;
	public float moveSpeed = 5f;

	[Range(0f, 1f)]
	public float turnSpeed = .1f;

	public float repelRange = 2f;
	public float repelAmount = .3f;

	public float startMaxChaseDistance = 20f;
	private float maxChaseDistance;

	[Header("Shooting")]
	public float attackingSpeed = 1f;
	public float shootDistance = 5f;
	public GameObject bulletPrefab;
	public Transform firePoint;
	public float firePerSecond = 1f;
	private float nextTimeToFire = .5f;

	private Rigidbody2D rb;
	private Enemy e;

	//private Vector3 velocity;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		e = GetComponent<Enemy>();

		if (EnemyRBs == null)
		{
			EnemyRBs = new List<Rigidbody2D>();
		}

        moveSpeed *= (Progression.Growth - 1f) * 0.5f + 1f;

        EnemyRBs.Add(rb);
	}

	private void OnDestroy()
	{
		EnemyRBs.Remove(rb);
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		maxChaseDistance = startMaxChaseDistance; /** Progression.Growth;*/

		float distance = Vector2.Distance(rb.position, PlayrMovement.Position);

		if (distance > maxChaseDistance)
		{
			//Destroy(gameObject);
			e.Die(false);
			return;
		}

		Vector2 direction = (PlayrMovement.Position - rb.position).normalized;

		Vector2 newPos;
		
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
		
		if (eType == EnemyType.shooter)
		{

			if (distance > shootDistance)
			{
				newPos = MoveRegular(direction);
			}
			else
			{
				newPos = MoveStrafing(direction, attackingSpeed);
			}

            Shoot();
            newPos -= rb.position;
            rb.AddForce(newPos, ForceMode2D.Force);

		}
        else
        {
			newPos = MoveRegular(direction);
			rb.MovePosition(newPos);
		}
	}

	Vector2 MoveStrafing(Vector2 direction, float speed)
	{
		Vector2 newPos = transform.position + transform.right * Time.fixedDeltaTime * speed;
		return newPos;
	}

	
	Vector2 MoveRegular(Vector2 direction)
	{
		Vector2 repelForce = Vector2.zero;
		foreach (Rigidbody2D enemy in EnemyRBs)
		{
			if (enemy == rb)
				continue;

			if (Vector2.Distance(enemy.position, rb.position) <= repelRange)
			{
				Vector2 repelDir = (rb.position - enemy.position).normalized;
				repelForce += repelDir;
			}
		}

		Vector2 newPos = transform.position + transform.up * Time.fixedDeltaTime * moveSpeed;
		newPos += repelForce * Time.fixedDeltaTime * repelAmount;

		return newPos;
	}

	void Shoot()
	{
		if (Time.time >= nextTimeToFire)
		{
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			Destroy(bullet, 30f);

			nextTimeToFire = Time.time + 1f / firePerSecond;

		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, maxChaseDistance);
	}
}