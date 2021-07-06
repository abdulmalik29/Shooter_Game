using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMenuEnemyAi : MonoBehaviour
{

	public float moveSpeed = 1f;
	public static List<Rigidbody2D> EnemyRBs;


	private Rigidbody2D rb;
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();

		if (EnemyRBs == null)
		{
			EnemyRBs = new List<Rigidbody2D>();
		}
		EnemyRBs.Add(rb);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.position = Vector2.MoveTowards(transform.position, AiPlayer.Position, moveSpeed * Time.deltaTime);
	}

}
