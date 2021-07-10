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
    public Text healthText;

    [Header("Level up effect")]
    public GameObject levelUpEffect;

    private RippleProcessor rp;


    void Start()
    {
        WaveManager.onWaveChanged += WaveSpawner_onWaveChanged;

        rp = Camera.main.GetComponent<RippleProcessor>();

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + Progression.Score.ToString();
        waveText.text = "Wave: " + WaveManager.currentWaveNum;
        healthText.text = Player.currentHearts.ToString();
    }

    private void WaveSpawner_onWaveChanged(object sender, EventArgs e)
    {
        
        GameObject effect = Instantiate(levelUpEffect, PlayrMovement.Position, Quaternion.identity);
        Destroy(effect, 3f);
        rp.MaxAmount = 100;
        rp.Ripple(PlayrMovement.Position);
        
    }

}
