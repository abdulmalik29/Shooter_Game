using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public Text waveText;
    public GameObject heart;
    public GameObject heartPos;
    public List<Image> hearts;

    void Start()
    {
        WaveManager.onWaveChanged += WaveSpawner_onWaveChanged;

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
        waveText.text = "Wave: " + Progression.currentWaveNum;
    }

}
