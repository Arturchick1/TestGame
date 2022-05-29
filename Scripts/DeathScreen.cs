using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject DthScreen;
    [SerializeField]
    private int indexScene;

    public void EndGame()//включает экран конца игры
    {
        DthScreen.SetActive(true);
    }

    public void RestartGame()// перезапускает уровень
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()// загружает след сцену
    {
        SceneManager.LoadScene(indexScene);
    }
    
    public void Exit()// закрывает приложение
    {
        Application.Quit();
    }


}
