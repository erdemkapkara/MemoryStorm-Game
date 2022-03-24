using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject ExitPannel;
    private void Start()
    {
        if (Time.timeScale==0)
             Time.timeScale = 1;

        Screen.fullScreen = false;
    }
    public void EndGame()
    {
        ExitPannel.SetActive(true);      

    }
    public void Answer(string answer)
    {
        if (answer == "Yes")
        {
            Application.Quit();

        }else
        {
            ExitPannel.SetActive(false);

        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(GameController.sceneCount);
        //SceneManager.LoadScene(PlayerPrefs.GetInt("KaldigiBolum")); // kaldığı devam etmesini sağlıcam
    }
}
