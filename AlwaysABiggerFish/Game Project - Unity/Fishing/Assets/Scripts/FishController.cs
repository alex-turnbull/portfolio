using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
//using UnityEditor.Android;

public class FishController : MonoBehaviour {

    public float speed; // direction and speed are split for ease of balence, direction should always be 1 or -1
    private int direction;

    private Fish Info;
    public int PlayerID;

    private GameObject controller;
    private TwoPlayerController PlayerCont;

    private GameObject buttonControl;
    private powerupButtonController buttonController;

    private Animator animator;
    private float BlendValue;

    public GameObject FishParticle;
    public GameObject JellyParticle;
    public GameObject JellyPowerUpParticle;
    public GameObject SpeedPowerUpParticle;

    private AudioSource audioHandler;
    private GameObject SFXController;
    private AudioSource SFXHandler;

    public AudioClip jellyZapAudio, otterSqueakAudio;





    private bool touching;

    public void SetupFish(int dir, Fish info, int PlayerRef)
    {
        //assign variables for the fish

        Info = info; //information from the fish prefab (score)
        direction = dir; //-1 or 1 (to travel left/right) - based on player 
        PlayerID = PlayerRef;

        BlendValue = Info.blendValue;

    }

    // Use this for initialization
    void Start() {

        SFXController = GameObject.Find("SFXController");
        SFXHandler = SFXController.GetComponent<AudioSource>();

        audioHandler = GetComponent<AudioSource>();



        //SprRen = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //get other game objects and their components for reference
        controller = GameObject.Find("PlayerController");
        PlayerCont = controller.GetComponent<TwoPlayerController>();
        buttonControl = GameObject.Find("PowerupController");
        buttonController = buttonControl.GetComponent<powerupButtonController>();
    }

    // Update is called once per frame
    void Update() {

        animator.SetFloat("Blend", BlendValue);

        if (PlayerID == 1) //if the fish belongs to player one, transform along the bottom strip (right to left)
        {
            Vector3 tickMove = new Vector3((speed * direction * Time.deltaTime * TwoPlayerController.fishBonusSpeedP1), 0.0f, 0.0f); // added fishBonusSpeed and removed magic number '1' AM
            transform.position += tickMove;

        }

        if (PlayerID == 2) //if the fish belongs to player one, transform along the top strip (left to right)
        {
            Vector3 tickMove = new Vector3((speed * direction * Time.deltaTime * TwoPlayerController.fishBonusSpeedP2), 0.0f, 0.0f); // added fishBonusSpeed and removed magic number '1' AM
            transform.position += tickMove;

        }



        if (touching == true) //basic check, need to incorperate lineMoving from player controller  GetComponent<TwoPlayerController>().lineMoving == true)
        {
            CatchFish(Info); //if fish is colliding with rod, use as parameter into a function to check type of fish      
        }

    }

    //if fish collides with the rod, set the boolean to say it's in contact to true - to work for checking fish catching
    public void OnTriggerEnter2D(Collider2D collision)
    {
        touching = true;
    }

    //if fish leaves the rod, then set the bool to false - meaning it can't be caught anymore
    void OnTriggerExit2D(Collider2D collision)
    {
        touching = false;
    }

    void CatchFish(Fish fish) //when fish collide with the line check how to handle based on fish type
    {


        //jellyfish handler - if statement checks that the line isn't moving
        if (PlayerCont.lineMoving == false)
        {

            if (fish.Name == "JellyFish") //if the fish is a jellyfish, add a negative score and retract line to the other players side.
            {
                audioHandler.clip = jellyZapAudio;
                audioHandler.Play();

                PlayerCont.addScore(PlayerID, fish.ScoreValue);
                PlayerCont.lineMoving = true; //code to change and switch lines to the other players side
                touching = false; // set the fish touching to false so that catch code isn't run constantly while fish is in the rod

                GameObject feedback = Instantiate(JellyParticle, gameObject.transform); //instantiate red zap particles at fish position
                feedback.transform.parent = null;   //stops the particles being set to inactive along with the fish/following the fish's transform

                PlayerCont.ZapFeedback(PlayerID);  //call function in twoplayercontroller to start feedback for the relevant player
                Handheld.Vibrate();                 //haptic feedback  -COMMENTED OUT AS BREAKS PC BUILD (works fine for mobile builds)
            }
        }

        //nested loops for the seperate catachable fish - this major if statement checks if a player taps the screen on their side whilst it is their turn.
        if ((PlayerCont.P1ButtonDown == true && PlayerCont.lineDown == true) || (PlayerCont.P2ButtonDown == true && PlayerCont.lineDown == false) && PlayerCont.lineMoving == false)
        {


            //check if it's the jelly powerup
            if (fish.Name == "jellyPickup")
            {

                //depending on who caught the fish, make their power-up button active
                if (PlayerID == 1)
                {
                    powerUpJelly.p1Ready = true;
                    buttonController.p1Jelly.interactable = true;
                }
                else
                {
                    powerUpJelly.p2Ready = true;
                    buttonController.p2Jelly.interactable = true;
                }

                GameObject feedback = Instantiate(JellyPowerUpParticle, gameObject.transform); //instantiate pink jelly particles at fish position
                feedback.transform.parent = null;   //stops the particles being set to inactive along with the fish/following the fish's transform

            
                //remove the fish and set it's touching to false to stop it constantly run the catch code
                gameObject.SetActive(false);
                touching = false;
            }

            //check for the other power-up and follow the same process
            else if (fish.Name == "speedPickup")
            {
                if (PlayerID == 1)
                {
                    powerUpSpeed.p1Ready = true;
                    buttonController.p1Speed.interactable = true;
                }
                else
                {
                    powerUpSpeed.p2Ready = true;
                    buttonController.p2Speed.interactable = true;
                }

                GameObject feedback = Instantiate(SpeedPowerUpParticle, gameObject.transform); //instantiate yellow lightning particles at fish position
                feedback.transform.parent = null;   //stops the particles being set to inactive along with the fish/following the fish's transform


                gameObject.SetActive(false);
                touching = false;
            }
            else //if it's a regular scoring fish
            {
                GameObject feedback = Instantiate(FishParticle, gameObject.transform); //instantiate green success particles at fish position
                feedback.transform.parent = null;   //stops the particles being set to inactive along with the fish/following the fish's transform



                gameObject.SetActive(false); //set fish inactive if input if pressed while fish is colliding
                PlayerCont.addScore(PlayerID, Info.ScoreValue); //add the score of the fish to the player who caught it




            }

            SFXHandler.Play();
         
        }



    }



}




