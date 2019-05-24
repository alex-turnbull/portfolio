using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTextController : MonoBehaviour {

    public Text p1P1Score, p1P2Score, p2P1Score, p2P2Score;
    private int P1Score, P2Score;

    public Image p1Bubble, p2Bubble;

    public Sprite winBubble, loseBubble, drawBubble;

    void hasWon()
    {
        
    }
    
	void Start ()
    {
        //gets scores from other script
        P1Score = TwoPlayerController.P1Score;
        P2Score = TwoPlayerController.P2Score;
    }

    void Update()

    {
        //sets both players score displays
        p1P1Score.text = P1Score.ToString();
        p1P2Score.text = P2Score.ToString();
        
        p2P1Score.text = P1Score.ToString();
        p2P2Score.text = P2Score.ToString();



        //sets the win / lose screen's for both players or a draw
        if (P1Score > P2Score)
        {
            p1Bubble.sprite = winBubble;
            p2Bubble.sprite = loseBubble;
        }

        if (P2Score > P1Score)
        {
            p1Bubble.sprite = loseBubble;
            p2Bubble.sprite = winBubble;
        }

        if (P2Score == P1Score)
        {
            p1Bubble.sprite = drawBubble;
            p2Bubble.sprite = drawBubble;
        }



    }
}
