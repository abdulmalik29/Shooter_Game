using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMenuEnemyAi : MonoBehaviour
{

	public float moveSpeed = 1f;
	public static List<Rigidbody2D> EnemyRBs;

    public GameObject deathEffect;
    private RippleProcessor rp;


    private Rigidbody2D rb;
	void Start()
    {
        rp = Camera.main.GetComponent<RippleProcessor>();
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


    void Die()
    {
        Destroy(gameObject);

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);

        rp.MaxAmount = 10f;
        rp.Ripple(transform.position);

        AudioManager.instance.Play("explosion1");

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Die(); 
        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

}
