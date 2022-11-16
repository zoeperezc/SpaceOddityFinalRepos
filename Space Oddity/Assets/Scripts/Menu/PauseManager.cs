using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject Btnpause;
    [SerializeField] private GameObject PauseMenu;


    public void PauseBtn()
    {
        Time.timeScale = 0f;
        Btnpause.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void ResumeBtn()
    {
        Time.timeScale = 1f;
        Btnpause.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void RestartBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelsBtn(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitBtn()
    {
        Debug.Log("ClosingApp");
        Application.Quit();
    }
}
