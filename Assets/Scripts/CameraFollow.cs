using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject target;
    public float smoothing = 0.1f;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        target = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if(transform.position != target.transform.position)
            {
                Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }
}
