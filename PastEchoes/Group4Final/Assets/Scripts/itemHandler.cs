using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemHandler : MonoBehaviour
{
    public GameObject ballObject;
    public float checkRadius = 1.5f;

    public string[] potentialItems = new string[3];
    private string actualItem;

    private ParticleSystem particleSys;

    public GameObject selectionCanvas;
    public GameObject leftTextObj;
    public GameObject centerTextObj;
    public GameObject rightTextObj;

    public AudioClip[] shakeSounds = new AudioClip[2];
    public AudioClip[] dropSounds = new AudioClip[2];

    [HideInInspector]
    public AudioSource audioSource;
    public List<AudioClip> dialog;

    private Text leftText;
    private Text centerText;
    private Text rightText;

    bool canPickup;

    // Start is called before the first frame update
    void Start()
    {
        canPickup = true;

        audioSource = gameObject.GetComponent<AudioSource>();

        ballObject = GameObject.Find("Ball");
        actualItem = gameObject.name;

        particleSys = gameObject.GetComponent<ParticleSystem>();
        particleSys.Stop();

        //selectionCanvas = GameObject.Find("canvasSelection");
        //leftTextObj = selectionCanvas.transform.GetChild(0).gameObject;
        //centerTextObj = selectionCanvas.transform.GetChild(1).gameObject;
        //rightTextObj = selectionCanvas.transform.GetChild(2).gameObject;

        leftText = leftTextObj.GetComponent<Text>();
        centerText = centerTextObj.GetComponent<Text>();
        rightText = rightTextObj.GetComponent<Text>();

        selectionCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(ballObject.transform.position, gameObject.transform.position) < checkRadius)
        {
            particleSys.Play();
        }   
    }

    public void itemSelection()
    {
        if (canPickup)
        {
            GameHandler.Instance.gameState = GameHandler.gameStates.selection;

            print("ye");
            leftText.text = potentialItems[0];
            centerText.text = potentialItems[1];
            rightText.text = potentialItems[2];

            selectionCanvas.SetActive(true);
        }
    }

    public void itemSelected(int buttonIdentifer)
    {
        canPickup = false;
        print(potentialItems[buttonIdentifer]);
        GameHandler.Instance.gameState = GameHandler.gameStates.navigating;
        selectionCanvas.SetActive(false);
        //Destroy(gameObject);
        audioSource.clip = dialog[buttonIdentifer];
        audioSource.Play();
        //handle the selected option and points or whatever it is
    }
}
