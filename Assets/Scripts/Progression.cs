using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Progression : MonoBehaviour
{
    public static ulong Score;

    public static int currentWaveNum;
    public static Wave currentWave;

    public static Progression instance;

    //public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Score = 0;
        currentWaveNum = 0;

        Debug.Log("Score " + Score);
        Debug.Log(" current Wave " + currentWaveNum);
        //scoreText.text = Score.ToString();

        //Time.timeScale = 1f;
        //Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
