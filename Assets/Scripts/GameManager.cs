using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _pauseScreen;
    public PlayFabManager playFabManager;
    private int _totalScore;


    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        _totalScore = CoinController.instance.getTotalCoinScore();
        playFabManager.SendLeaderBoard(_totalScore);
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //PAUSE MENU BUTTONS 
    public void PauseGame()
    {
        _pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ExitGame()
    {

    }

}
