using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winButtonContoller : MonoBehaviour {

    public Button menuButton, restartButton;

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}
