using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// There is one of these controllers on each fish strip.
/// holds its own object pool for fish (fishPool)
/// handles its own deactivation of disused fish, keeps them inside its list.

[System.Serializable]
public class Fish
    // defines a fish as having a name, a sprite and a reference
{
    public string Name;
    public Sprite FishSprite;
    public float blendValue;
    public int ScoreValue;
}

public class FishStripController : MonoBehaviour {

    public float SpawnRate;     // set in unity, delay in seconds between spawns
    private float SpawnTick;    // used internally to time the fish
    public GameObject fish;     // set in unity, reference to fish object to spawn  
    public Transform SpawnPoint;// set in unity, reference to location at which fish will spawn
    public int poolLimit;       // set in unity, max size of fishPool
    List<GameObject> fishPool;  // obect pool for fish for this player
    public int playerRef;
    public int Direction;

    public int bigFish;
    public int medFish;
    public int smlFish;
    public int jelFish;


    public Fish[] SpawnData;  // array for data population of fish


    public void ShuffleFish(List<GameObject> fishPool) // fisher-yates shuffle function takes a list of gameobjects fishPool as an input and shuffles it
    {

        for (int i = 0; i < fishPool.Count; i++)
        {
            GameObject temp = fishPool[i];
            int randomIndex = Random.Range(i, fishPool.Count);
            fishPool[i] = fishPool[randomIndex];
            fishPool[randomIndex] = temp;
        }

    }


    void Start()
    {

        SpawnTick = SpawnRate;

        fishPool = new List<GameObject>(); // instantiates the list

        for (int i = 0; i < poolLimit; i++) // instantiates <poolLimit> number of blank fish into the list and sets them as inactive
        {
            // int randIndex = Random.Range(0, SpawnData.Length);

            GameObject obj = (GameObject)Instantiate(fish);
            obj.SetActive(false);
            fishPool.Add(obj);
        }

    }


    void decideSpawn()
    {
        int rngesus = Random.Range(0, 101);
        int diff;

        if (playerRef == 1)
        // this controler handles two different players do must be duplicated as this is balence.
        {
            diff = (TwoPlayerController.P1Score - TwoPlayerController.P2Score);
            rngesus += powerUpJelly.p1JellyFactor;

                                                                //  big    med     sml     jellypwr spdpwr  jellyfish
            if (diff <= -10)              SpawnFishRandomly(rngesus, 20,    25,     30,     10,     10,     5);
            if (diff > -10 && diff <= -5) SpawnFishRandomly(rngesus, 20,    25,     30,     10,     10,     5);

            if (diff >- 5 && diff < 5)    SpawnFishRandomly(rngesus, 20,    25,     30,     10,     10,     5);

            if (diff >= 5 && diff < 10)   SpawnFishRandomly(rngesus, 10,    25,     35,     5,      5,      20);
            if (diff >= 10)               SpawnFishRandomly(rngesus,  5,    25,     40,     0,      0,      30);

        }




        if (playerRef == 2)
        // this controler handles two different players do must be duplicated as this is balence.
        {
            diff = (TwoPlayerController.P2Score - TwoPlayerController.P1Score);
            rngesus += powerUpJelly.p2JellyFactor;

            //  big    med     sml     jellypwr spdpwr  jellyfish
            if (diff <= -10) SpawnFishRandomly(rngesus, 20, 25, 30, 10, 10, 5);
            if (diff > -10 && diff <= -5) SpawnFishRandomly(rngesus, 20, 25, 30, 10, 10, 5);

            if (diff > -5 && diff < 5) SpawnFishRandomly(rngesus, 20, 25, 30, 10, 10, 5);

            if (diff >= 5 && diff < 10) SpawnFishRandomly(rngesus, 10, 25, 35, 5, 5, 20);
            if (diff >= 10) SpawnFishRandomly(rngesus, 5, 25, 40, 0, 0, 30);

        }
    }

    

      void SpawnFishRandomly(int rngesus, int bigspawn, int medspawn, int smlspawn, int jellypowerspawn, int speedpowerspawn, int jellyspawn)
    {

        if (0 < rngesus && rngesus <= bigspawn) spawnFish(0);
        if (bigspawn < rngesus && rngesus <= (bigspawn + medspawn)) spawnFish(1);
        if ((bigspawn + medspawn) < rngesus && rngesus <= (bigspawn + medspawn + smlspawn)) spawnFish(2);
        if ((bigspawn + medspawn + smlspawn) < rngesus && rngesus <= (bigspawn + medspawn + smlspawn + jellypowerspawn)) spawnFish(5);
        if ((bigspawn + medspawn + smlspawn + jellypowerspawn) < rngesus && rngesus <= (bigspawn + medspawn + smlspawn + jellypowerspawn + speedpowerspawn)) spawnFish(4);
        if ((bigspawn + medspawn + smlspawn + jellypowerspawn + speedpowerspawn) < rngesus) spawnFish(3);

    }


    void tempfunc(int rngesus) 
    {

 
    }

    void spawnFish(int fish)
    
    // function for activating fish as they are needed

    {
        //ShuffleFish(fishPool);

        for (int i = 0; i < fishPool.Count; i++) //loops through the pool
        {
            if(!fishPool[i].activeInHierarchy)  // checks the fish isn't already in use
            {
                fishPool[i].transform.position = SpawnPoint.position;
                fishPool[i].transform.rotation = SpawnPoint.rotation;
                FishController fishControl = fishPool[i].GetComponent<FishController>(); // grabs the fishcontroler for that fish
                fishControl.SetupFish(Direction, SpawnData[fish], playerRef);   //assigns the fish its data
                fishPool[i].SetActive(true);    //moves it to the correct location and rotation then activates it
                break;                          //only does this for the first fish it finds
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // counts down
        if (SpawnTick > 0)
        {
            SpawnTick -= Time.deltaTime;
        }

        else if (SpawnTick <= 0) //timer reaches 0
        {
            decideSpawn(); // decide which fish to spawn
            if (playerRef == 1) SpawnTick = (SpawnRate * powerUpSpeed.P1spawnRatio); //reset timer
            if (playerRef == 2) SpawnTick = (SpawnRate * powerUpSpeed.P2spawnRatio);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
        // when a fish hits the despawner, set it as inactive
    {
        if (other.gameObject.CompareTag("fish")) // bug catcher, just to make sure it can't set random other game objects inactive
        {
            other.gameObject.SetActive(false);
        }
    }
        
}
