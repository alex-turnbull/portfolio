using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private static GameHandler _Instance;

    public static GameHandler Instance { get { return _Instance; } }

    public GameObject playerGameObject;

    public AudioSource audioSource;
    public List<AudioClip> entryDialogs;
    private int dialogIterator;

    public List<AudioClip> ambientSounds;

    public ambientHandler ambientHandler;

    public enum gameStates
    {
        navigating,
        selection
    }

    public gameStates gameState;

    public enum levels
    {
        LEVEL_1,
        LEVEL_2,
        LEVEL_3,              
        LEVEL_4,
        LEVEL_5,
        LEVEL_6,
        MAX_LEVEL
    }

    public levels currentLevel;

    public List<GameObject> levelSpawners;

    public int buttonSelected;
    public bool buttonClickedOn = false;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        dialogIterator = 0;
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Instance = this;
        }

        gameState = gameStates.navigating;
        currentLevel = levels.LEVEL_1;
        audioSource.clip = entryDialogs[dialogIterator];
        audioSource.Play();

        ambientHandler.updateAmbient(currentLevel);
    }

    public void buttonClicked(int buttonIdentfier)
    {
        buttonSelected = buttonIdentfier;
        buttonClickedOn = true;
    }

    public void nextLevel()
    {
        if(currentLevel + 1 != levels.MAX_LEVEL)
        {
            currentLevel = currentLevel + 1;
            ambientHandler.updateAmbient(currentLevel);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);            
        }
        dialogIterator++;

        playerGameObject.transform.position = levelSpawners[(int)currentLevel].transform.position;
        playerGameObject.transform.rotation = levelSpawners[(int)currentLevel].transform.rotation;
        audioSource.clip = entryDialogs[dialogIterator];
        audioSource.Play();
    }
}
