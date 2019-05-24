using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{

    private bool inRangeOfItem;
    private GameObject itemInRange;

    private itemHandler currentItemHandleScript;

    private bool inRangeOfPodium;
    ItemDeposit depositPodScript;

    public List<string> inventory;

    public Canvas playerCanvas;
    private GameObject helpText;
    public GameObject eventTextObj;

    void Start()
    {
        playerCanvas.gameObject.SetActive(false);
        helpText = playerCanvas.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    { 
        if(GameHandler.Instance.gameState == GameHandler.gameStates.navigating)
        {
            helpText.GetComponent<Text>().text = "Press E to interact";

            if (inRangeOfItem && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pickup Item");

                currentItemHandleScript.itemSelection();

                inventory.Add(itemInRange.name);

                inRangeOfItem = false;
                itemInRange = null;                
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Debug.Log(inventory);
            }

            if (inRangeOfPodium && Input.GetKeyDown(KeyCode.E))
            {
                bool hadItem = false;

                foreach (var item in inventory)
                {
                    if (item == depositPodScript.compareToStore)
                    {
                        hadItem = true;
                        inventory.Remove(item);
                        Debug.Log("Placed item on podium");
                        //eventTextObj.GetComponent<Text>().text = "Memory complete";
                        GameHandler.Instance.nextLevel();
                        break;
                    }
                }
                if (hadItem == false)
                {
                    Debug.Log("You don't have the item");
                }

            }

            if(inRangeOfItem || inRangeOfPodium)
            {
                playerCanvas.gameObject.SetActive(true);
            }
            else
            {
                eventTextObj.GetComponent<Text>().text =  " ";
                playerCanvas.gameObject.SetActive(false);
            }
        }

        if(GameHandler.Instance.gameState == GameHandler.gameStates.selection)
        {
            helpText.GetComponent<Text>().text = "Left Click here to Shake the item or Right Click to drop and select the correct item";

            playerCanvas.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Shaking Object;
                currentItemHandleScript.audioSource.clip = currentItemHandleScript.shakeSounds[Random.Range(0, currentItemHandleScript.shakeSounds.Length)];
                currentItemHandleScript.audioSource.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //Dropping Object;
                currentItemHandleScript.audioSource.clip = currentItemHandleScript.dropSounds[Random.Range(0, currentItemHandleScript.dropSounds.Length)];
                currentItemHandleScript.audioSource.Play();
            }

            if (GameHandler.Instance.buttonClickedOn == true)
            {
                currentItemHandleScript.itemSelected(GameHandler.Instance.buttonSelected);
                //eventTextObj.GetComponent<Text>().text = "Level complete";
                GameHandler.Instance.buttonClickedOn = false;
            } 
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Debug.Log("Entered Item Space");
            Debug.Log("Item is: " + other.gameObject.name);
            inRangeOfItem = true;
            itemInRange = other.gameObject;
            currentItemHandleScript = itemInRange.GetComponent<itemHandler>();
        }

        if (other.gameObject.CompareTag("Deposit"))
        {
            Debug.Log("Entered Podium Space");
            depositPodScript = other.gameObject.GetComponent<ItemDeposit>();
            inRangeOfPodium = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Debug.Log("Left Item Space");
            inRangeOfItem = false;
            itemInRange = null;
            currentItemHandleScript = null;
        }

        if (other.gameObject.CompareTag("Deposit"))
        {
            Debug.Log("Left Podium Space");
            depositPodScript = null;
            inRangeOfPodium = false;
        }
    }
}
