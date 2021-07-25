using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayer : MonoBehaviour
{

    public static Vector2 Position;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float firePerSecond = 1f;
    private float nextTimeToFire = .5f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject enemy = FindClosestEnemy();

        if (enemy == null)
        {
            return;
        }


        Vector3 v = new Vector3(rb.position.x, rb.position.y, enemy.transform.position.z);
        Vector2 lookDirection = enemy.transform.position - v;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        
        Position = rb.position;

        if (Time.time >= nextTimeToFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            AudioManager.instance.Play("gunShot4");
            PlayrMovement.playerAngle = angle;

            Destroy(bullet, 30f);

            nextTimeToFire = Time.time + 1f / firePerSecond;

        }
    }


    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;

    }
}
