using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Progression : MonoBehaviour
{
    public static ulong Score;
    public static float Growth;

    public static Progression instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Score = 0;
        Growth = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        WaveManager.onWaveChanged += OnWaveChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWaveChanged(object sender, EventArgs e)
    {
        //float baseScale = Growth;
        //float factor = 1.1f;

        //Growth *= factor;
        Growth = 1.2f;

        Debug.Log("Growth" + Growth);
    }

}
