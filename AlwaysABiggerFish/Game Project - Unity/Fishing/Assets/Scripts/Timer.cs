using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour {

    public float RoundTime;
    public GameObject marker;

    public Transform startMarker;
    public Transform endMarker;

    private float initialTime;


    void Start()
    {
        initialTime = RoundTime;
    }


    void Update()
    {
        // counts down
        if (RoundTime > 0)
        {
            RoundTime -= Time.deltaTime;
        }

        else if (RoundTime <= 0) // when counter reaches zero call end the level
        {
            SceneManager.LoadScene("win", LoadSceneMode.Single); // load the win screen
        }


        // handle animation of timer
        float alpha = RoundTime / initialTime;  // create a float between 0 and 1 as a ratio of "time passed"
        marker.transform.position = Vector3.Lerp(endMarker.position, startMarker.position, alpha); // lerp this alpha to move the fish smoothly





    }



}
