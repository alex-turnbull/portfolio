using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	public AudioClip buttonPress;
	public AudioSource audio;
	public GameObject pauseMenuUI;
	public GameObject unpauseUI;
	public Toggle  p1, p2;
	public Toggle unpause1, unpause2;
    public GameObject MusicPlayer;
    private AudioSource audioSource;

    /*DontDestroyOnLoad(this.gameObject) prevents the LevelManager game object from being destroyed between scenes
		public void LoadLevel() { //applied to UI buttons so that they send a debug message to console and the "main" scene is loaded when clicked
        Debug.Log("Level load requested for : ");
        int c = SceneManager.GetActiveScene().buildIndex;
        if (c < SceneManager.sceneCountInBuildSettings)
	        SceneManager.LoadScene("main");
			DontDestroyOnLoad(this.gameObject); 
	}*/

    public void MainMenu() {
		SceneManager.LoadScene("Start");
		DontDestroyOnLoad(this.gameObject);
	}

	public void Instructions() { //applied to "how to play" button so that a debug message is sent to the console and the "Instructions" scene is loaded when clicked 
		Debug.Log("Loading instructions screens");
        	SceneManager.LoadScene("Instructions");
        	DontDestroyOnLoad(this.gameObject);
	}

	public void ReadyUp() { //applied to UI buttons so that the "Ready Up" scene is loaded when clicked
        Debug.Log("Level load requested for : ");
        Time.timeScale = 1f;
        int c = SceneManager.GetActiveScene().buildIndex;
        if (c < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene("Ready_Up");
            DontDestroyOnLoad(this.gameObject);


	}

	public void Quit(string name) { //applied to quit button so that a debug message is sent to the console and the application is closed when clicked 
		Debug.Log("Quitting game...");
		Application.Quit(); 
	}

	public void Resume(){
		pauseMenuUI.SetActive(false);
		unpauseUI.SetActive(true);
        //Time.timeScale = 1f;
        
    }


	public void Pause(){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
        audioSource.Pause();
    }

    private void Start()
    {
        MusicPlayer = GameObject.Find("MusicPlayer");
        audioSource = MusicPlayer.GetComponent<AudioSource>();
    }

    void Update()
    {
        try
        {
            if (unpause1.isOn == true & unpause2.isOn == true)
            {
                Debug.Log("start play");
                unpauseUI.SetActive(false);
                Time.timeScale = 1f;
                Debug.Log("start play");
                unpause1.isOn = false;
                unpause2.isOn = false;
                audioSource.Play();

                //Resume();
            }
        }
        catch (System.NullReferenceException)
        {

        }
        // gives p1 an input button which will interact with thier toggle and flip flop it
        if (Input.GetKey(KeyCode.LeftControl) == true)
        {
            //  if (p1.isOn == false)
            p1.isOn = true;

            // if (p1.isOn == true)
            //     p1.isOn = false;
        }

        // gives p2 an input button which will interact with thier toggle and flip flop it
        if (Input.GetKeyDown(KeyCode.RightControl) == true)
        {
            //  if (p2.isOn == false)
            p2.isOn = true;

            //  if (p2.isOn == true)
            //     p2.isOn = false;
        }




        //when both players are ready the game begins
        try
        {
            if (p1.isOn == true & p2.isOn == true)
            {
                SceneManager.LoadScene("main", LoadSceneMode.Single);
                DontDestroyOnLoad(this.gameObject);
                pauseMenuUI.SetActive(false);
                //unpauseUI.SetActive(false);
            }
        }
        catch (System.NullReferenceException)
        {
        }



        if (Input.GetKeyDown(KeyCode.RightControl) == true)
        {
            unpause1.isOn = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            unpause2.isOn = true;
        }
    }


}


/*public void playClip() { //applied to UI buttons so that they play the sound "buttonClick" when clicked
    audio = GetComponent<AudioSource>();
    audio.PlayOneShot(buttonPress);
}*/




