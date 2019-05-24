using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambientHandler : MonoBehaviour
{
    public List<AudioClip> amibentSounds;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAmbient(GameHandler.levels levelEnum)
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = amibentSounds[(int)levelEnum];
        audioSource.Play();
    }
}
