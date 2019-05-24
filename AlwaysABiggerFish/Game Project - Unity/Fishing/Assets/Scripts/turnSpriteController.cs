using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnSpriteController : MonoBehaviour {

    public SpriteRenderer p1Sprite, p2Sprite;
    public Sprite activeSprite, inactiveSprite;

    public GameObject gamecontroller;
 
    // Use this for initialization
    void Start ()
    {
        p1Sprite.sprite = activeSprite;
        p2Sprite.sprite = inactiveSprite;

    }
	
	// Update is called once per frame
	void Update ()
    {
        TwoPlayerController controller = gamecontroller.GetComponent<TwoPlayerController>();

        
        if (controller.IsP1Active() == true)
        {
            p1Sprite.sprite = activeSprite;
            p2Sprite.sprite = inactiveSprite;
        }

        else
        {
            p1Sprite.sprite = inactiveSprite;
            p2Sprite.sprite = activeSprite;
        }
	}
}
