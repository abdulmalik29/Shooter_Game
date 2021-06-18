using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void AssainTarget(Vector3 startPoint, Transform newTarget)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(1, target.position);
    }
}
