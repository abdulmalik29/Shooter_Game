using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamagePoints : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyAfter;
    public float northDistance;
    void Start()
    {
        Destroy(gameObject, destroyAfter);
        transform.localPosition += new Vector3(0, northDistance, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
