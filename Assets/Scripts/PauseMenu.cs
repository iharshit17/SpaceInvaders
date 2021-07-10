using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            { Resume(); }
            else
            {
                Pause();
            }
        }
        
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
