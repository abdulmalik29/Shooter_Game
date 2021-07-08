using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void StartGame()
    {
        Debug.Log("%%%%%%%");
        //loadTransaction.instance.load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
