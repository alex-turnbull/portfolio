using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public static MusicController instance;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(this.gameObject); //prevents MusicPlayer game object from being destroyed so that the waves music is played consistently throughout each scene 
        
    }
}

