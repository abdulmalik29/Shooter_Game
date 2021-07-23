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

    private void OnWaveChanged(object sender, EventArgs e)
    {

        Growth = 1.1f;
        //Debug.Log("Growth" + Growth);
    }

}
