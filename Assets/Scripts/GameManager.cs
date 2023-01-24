using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    
    
  public static bool GameIsPaused = false;
 public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused){
            Resume();
        }else{
            Pause();
        }
        }
    }
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
     public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void Restart()
    {
    SceneManager.LoadScene("Home");
   

    }
    public void MainMenuHome()
    {
    SceneManager.LoadScene("home");

    }
    public void StartScene()
    {
    SceneManager.LoadScene("main");

    }
}
