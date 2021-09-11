using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 1.5f;

    PauseMenu gamePaused;

    public void LoadNext(string nextScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }

    public void LoadLevel(string nextLevel)
    {
        Time.timeScale = 1;
        StartCoroutine(SceneTransition(nextLevel));
    }

    public void LoadStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void LoadSame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGame()
    {
        // SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // BOSSES

    /*
    public void SolBoss()
    {
        SceneManager.LoadScene("SolBoss");
    }
    */

    IEnumerator SceneTransition(string sceneName)
    {
        transition.SetTrigger("In");

        yield return new WaitForSeconds(transitionTime);

        Time.timeScale = 1;

        SceneManager.LoadScene(sceneName);

    }

}
