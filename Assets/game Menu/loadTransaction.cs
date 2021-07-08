using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loadTransaction : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public static loadTransaction instance;

    public void load()
    {
        StartCoroutine(LoadGame(1));
    }

    IEnumerator LoadGame(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
