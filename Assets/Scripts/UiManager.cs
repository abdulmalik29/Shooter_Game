using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [Header("UI Text")]
    public Text scoreText;
    public Text waveText;

    [Header("Health system")]
    public GameObject heart;
    public GameObject heartPos;
    public List<Image> hearts;

    [Header("Level up effect")]
    public GameObject levelUpEffect;

    private RippleProcessor rp;


    void Start()
    {
        WaveManager.onWaveChanged += WaveSpawner_onWaveChanged;

        rp = Camera.main.GetComponent<RippleProcessor>();


        for (int i = 0; i < Player.currentHearts; i++)
        {
            GameObject h = Instantiate(heart, heartPos.transform);
            hearts.Add(h.GetComponent<Image>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + Progression.Score.ToString();
    }

    private void WaveSpawner_onWaveChanged(object sender, EventArgs e)
    {
        waveText.text = "Wave: " + WaveManager.currentWaveNum;

        GameObject effect = Instantiate(levelUpEffect, PlayrMovement.Position, Quaternion.identity);
        Destroy(effect, 3f);
        rp.MaxAmount = 100;
        rp.Ripple(PlayrMovement.Position);
        
    }

}
