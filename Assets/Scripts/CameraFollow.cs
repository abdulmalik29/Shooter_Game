using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float smoothing = 0.1f;

    private void FixedUpdate()
    {
        if(transform.position != target.transform.position)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}
