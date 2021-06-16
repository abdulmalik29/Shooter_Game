using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int maxHearts = 5;
    public static int currentHearts;

    //public static event Action onDamageTaken;
    //public static event Action onHeal;


    void Start()
    {
        currentHearts = maxHearts;
        Debug.Log("current Hearts: " + currentHearts);
    }

    public static void takeDamage(int Damage = 1)
    {
        if (currentHearts > 0)
        {
            currentHearts -= Damage;
            Debug.Log("current Hearts: " + currentHearts);
        }
    }

    public static void heal(int amount = 1)
    {
        if (currentHearts <= maxHearts)
            currentHearts += amount;
            Debug.Log("current Hearts: " + currentHearts);
    }

    public static void increasHearts(int amount = 1)
    {
        maxHearts += amount;
        Debug.Log("max Hearts: " + maxHearts);
    }

}
