using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public GameObject heart;
    public GameObject heartPos;
    public List<Image> hearts;

    void Start()
    {
        Player.onDamageTaken += UpdateHearts;
        Player.onHeal += addHearts;

        for (int i = 0; i < Player.maxHearts; i++)
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

    void UpdateHearts()
    {
        int heartFill = Player.currentHearts;

        foreach (Image i in hearts)
        {
            i.fillAmount = heartFill;
            heartFill--;
        }
    }

    void addHearts()
    {
        foreach (Image i in hearts)
        {
            Destroy(i.gameObject);
        }
        hearts.Clear();
        for (int i = 0; i < Player.maxHearts; i++)
        {
            GameObject h = Instantiate(heart, heartPos.transform);
            hearts.Add(h.GetComponent<Image>());
        }
    }

}
