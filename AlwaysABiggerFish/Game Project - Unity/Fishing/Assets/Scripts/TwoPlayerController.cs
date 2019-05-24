using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoPlayerController : MonoBehaviour {

    public GameObject P1Line, P2Line; // references to each players line objects
    public float P1_yMin, P1_yMax, speed; // set in unity, tweakable limits for movement of lines.
    public bool lineDown = true, lineMoving, inputClicked; // used internally to animate lines.
    public static int P1Score, P2Score;
    public static float fishBonusSpeedP1, fishBonusSpeedP2; //change the speed of the fish when speed powerup is active

    private AudioSource audioHandler;

    private bool p1gLate, p2gLate;

    public Button P1Button;
    public Button P2Button;

    //reference image objects for the tally UI for both players
    [Header("P1 Tally")]
    public Image P1tally1, P1tally2, P1tally3, P1tally4, P1tally5, P1tally6;

    [Header("P2 Tally")]
    public Image P2tally1, P2tally2, P2tally3, P2tally4, P2tally5, P2tally6;

    //create lists to store tally sprites in
    public List<Image> P1tallyList = new List<Image>();
    public List<Image> P2tallyList = new List<Image>();

    //reference tally sprites
    public Sprite ui_tally_00, ui_tally_01, ui_tally_02, ui_tally_03, ui_tally_04, ui_tally_05;

    public bool P1ButtonDown, P2ButtonDown;

    private GameObject buttonControl;
    private powerupButtonController buttonController;

    //used for changing animations on the otters
    public GameObject fisher1;
    public GameObject fisher2;


    // Use this for initialization
    void Start()
    {
        audioHandler = GetComponent<AudioSource>();

        //initialise variables for both players
        P1Score = 0; 
        P2Score = 0;
        fishBonusSpeedP1 = 1;
        fishBonusSpeedP2 = 1;

        lineDown = true; // makes sure that the line is in its starting position
        lineMoving = false;

        Button P1Input = P1Button.GetComponent<Button>();
        Button P2Input = P2Button.GetComponent<Button>();

        //on start, add the tally objects into each plauyers list
        P1tallyList.Add(P1tally1);
        P1tallyList.Add(P1tally2);
        P1tallyList.Add(P1tally3);
        P1tallyList.Add(P1tally4);
        P1tallyList.Add(P1tally5);
        P1tallyList.Add(P1tally6);

        P2tallyList.Add(P2tally1);
        P2tallyList.Add(P2tally2);
        P2tallyList.Add(P2tally3);
        P2tallyList.Add(P2tally4);
        P2tallyList.Add(P2tally5);
        P2tallyList.Add(P2tally6);

        //at the start - for each of the players tally, set of the objects to have a blank sprite
        foreach (Image tally in P1tallyList) {
            tally.sprite = ui_tally_00;
        }

        foreach (Image tally in P2tallyList) {
            tally.sprite = ui_tally_00;
        }

        //reference the buttons from the PowerUpsController so they can be changed as needed.
        buttonControl = GameObject.Find("PowerupController");
        buttonController = buttonControl.GetComponent<powerupButtonController>();
    }

    private void Update() 
    {
        //test buttons for windows build / unity editor testing
        //player inputs on control keys
        if (Input.GetKey(KeyCode.LeftControl)) ButtonPressed(P1Button);
        if (Input.GetKey(KeyCode.RightControl)) ButtonPressed(P2Button);

        //turn on powerups for each player
        if (Input.GetKey(KeyCode.Q)) buttonController.p1Jelly.interactable = true;
        if (Input.GetKey(KeyCode.W)) buttonController.p1Speed.interactable = true;
        if (Input.GetKey(KeyCode.O)) buttonController.p2Jelly.interactable = true;
        if (Input.GetKey(KeyCode.P)) buttonController.p2Speed.interactable = true;

    }
    // Update is called once per frame
    void LateUpdate()
    {   
        
        if (lineMoving == (true) && lineDown == false)  // if the line is already moving and its last complete position was up
        {
            downFrame();                                // animate a down frame
            
        }

        if (lineMoving == (true) && lineDown == true)   // if the line is already moving and its last complete position was down
        {   
            upFrame();                                  // animate an up frame
            
        }

        if (p1gLate) { P1Go(); p1gLate = false; }
        if (p2gLate) { P2Go(); p2gLate = false; }

    }

    public bool IsP1Active()
    {
        return lineDown;
        
    }

    // define functions for animating frames of line movement
    public void downFrame()
    {
        if (P1Line.transform.position.y > P1_yMin)  // check the lines arent beyond the limits (only check once using P1's line positions as they will always move in perfect syncronisation and prevent logic conflicts
        {
            P1Line.transform.position += new Vector3(0.0f, -0.1f * speed * Time.deltaTime, 0.0f);   
            P2Line.transform.position += new Vector3(0.0f, -0.1f * speed * Time.deltaTime, 0.0f);
        }
        if (P1Line.transform.position.y <= P1_yMin) // when the lines meet the limits flip the line positon and stop the line moving
        {
            lineDown = true;
            lineMoving = false;
        }

    }

    public void upFrame()
    {
        if (P1Line.transform.position.y < P1_yMax) // as above
        {
            P1Line.transform.position += new Vector3(0.0f, 0.1f * speed * Time.deltaTime, 0.0f);
            P2Line.transform.position += new Vector3(0.0f, 0.1f * speed * Time.deltaTime, 0.0f);
        }
        if (P1Line.transform.position.y >= P1_yMax) // as above
        {
            lineDown = false;
            lineMoving = false;
            //P1ButtonDown = false;
            //P2ButtonDown = false;
        }
    }

    public void addScore(int PlayerRef, int scoreValue)
    {
        if (PlayerRef == 1)
        {
            P1Score += scoreValue;
            //scoreP1.text = P1Score.ToString();
            if(P1Score < 0) P1Score = 0;
            else if (P1Score > 30) P1Score = 30;
            CalculateScore(P1Score, 1);

        }
        if (PlayerRef == 2)
        {
            P2Score += scoreValue;
            //scoreP2.text = P2Score.ToString();
            if (P2Score < 0) P2Score = 0;
            else if (P2Score > 30) P2Score = 30;
            CalculateScore(P2Score, 2);
        }

        //print("Player1: " + P1Score.ToString());
        //print("Player2: " + P2Score.ToString());
    }

    public void ButtonPressed(Button PushedButton)
    {
        audioHandler.Play();
        //check what player pressed their button and transistion of the lines as appropriate
        //^ functions named P1Go and P2Go below
        if (PushedButton.name == "P1Input")
        {
            P1ButtonDown = true;
            p1gLate = true;
            StartCoroutine(timerToReset());
        }
        else if (PushedButton.name == "P2Input")
        {
            P2ButtonDown = true;
            p2gLate = true;

            StartCoroutine(timerToReset());
        }
    }

    //after a tiny delay, reset the player buttons to stop spam
    public IEnumerator timerToReset() {
        yield return new WaitForSeconds(0.1f);
        resetButtons();
    }

    void resetButtons() 
    {
        P1ButtonDown = false;
        P2ButtonDown = false;
    }

    public void P2Go()
    {
        if (lineMoving == false && lineDown == false) // the player taps, the line isn't already moving and its current position is up
        {
            lineMoving = true;  // set the line as moving
            downFrame();  // animate a frame of downward movement
        }
          
    }

    public void P1Go()
    {
        if (lineMoving == false && lineDown == true) // the player taps, the line isn't already moving and its current position is down
        {
            lineMoving = true;  //set the line as moving
            upFrame();          // animate a frame of upward movement
        }
    }

    public void CalculateScore(int score, int playerRef)
    {
        //calculate the division and modulo of the score
        int div = score / 5;
        int mod = score % 5;


        if (playerRef == 1)
        {
            //assign all "tally sprites" to be blank/ a checkmark for now
            foreach (Image tally in P1tallyList)
            {
                tally.sprite = ui_tally_00;
            }

            //using the division, calculate and switch the number of sprites that need to be the full 5 bar tally sprite
            for (int i = 0; i < div; i++)
            {
                P1tallyList[i].sprite = ui_tally_05;
            }
            //change the first sprite after the last full tally to show the modulus
            for (int i = 0; i < 6; i++)
            {
                if (P1tallyList[i].sprite != ui_tally_05)
                {
                    if (mod == 0) P1tallyList[i].sprite = ui_tally_00;
                    if (mod == 1) P1tallyList[i].sprite = ui_tally_01;
                    if (mod == 2) P1tallyList[i].sprite = ui_tally_02;
                    if (mod == 3) P1tallyList[i].sprite = ui_tally_03;
                    if (mod == 4) P1tallyList[i].sprite = ui_tally_04;
                    break;
                }

            }
        }
        //same again for the other player
        else
        {
            foreach (Image tally in P2tallyList)
            {
                tally.sprite = ui_tally_00;
            }

            for (int i = 0; i < div; i++)
            {
                P2tallyList[i].sprite = ui_tally_05;
            }
            for (int i = 0; i < 6; i++)
            {
                if (P2tallyList[i].sprite != ui_tally_05)
                {
                    if (mod == 0) P2tallyList[i].sprite = ui_tally_00;
                    if (mod == 1) P2tallyList[i].sprite = ui_tally_01;
                    if (mod == 2) P2tallyList[i].sprite = ui_tally_02;
                    if (mod == 3) P2tallyList[i].sprite = ui_tally_03;
                    if (mod == 4) P2tallyList[i].sprite = ui_tally_04;
                    break;
                }

            }
        }
    }

    public void ZapFeedback(int playerRef)   //feedback for jellyfish zap. it didn't want me to call the coroutine from the fishcontroller script
    {
        StartCoroutine(ZapDuration(playerRef));
    }

    public IEnumerator ZapDuration(int playerRef)       //coroutine for zap feedback
    {
        if (playerRef == 1)         //set relevant player to zap animation
        {
            fisher1.GetComponent<Animator>().SetBool("Zapped", true);
        }
        else
        {
            fisher2.GetComponent<Animator>().SetBool("Zapped", true);
        }

        yield return new WaitForSeconds(0.5f);

        if (playerRef == 1)         //set back to idle
        {
            fisher1.GetComponent<Animator>().SetBool("Zapped", false);
        }
        else
        {
            fisher2.GetComponent<Animator>().SetBool("Zapped", false);
        }
    }
}
