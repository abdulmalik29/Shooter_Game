using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static int maxHearts = 3;
    public static int currentHearts;


    public static event Action onDamageTaken;
    public static event Action onHeal;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHearts = maxHearts;
    }

    public static void TakeDamage()
    {
        if (currentHearts <= 0)
        {
            return;
        }
        currentHearts--;

        Debug.Log("current Hearts: " + currentHearts);

        if (onDamageTaken != null)
        {
            onDamageTaken();
        }
    }

    public static void Heal()
    {
        if (currentHearts >= maxHearts && currentHearts <= 0)
        {
            return;
        }

        currentHearts++;

        if (onHeal != null)
        {
            onHeal();
        }
    }

}
