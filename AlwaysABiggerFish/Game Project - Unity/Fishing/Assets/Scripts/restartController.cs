using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restartController : MonoBehaviour {

    public Toggle  p1, p2;
    
    public void p1Ready()
    {
    //    p1 = true;
    }

    public void p2Ready()
    {
      //  p2 = true;
    }

    // Use this for initialization
    void Start ()
    {

        //p1 = false;
        //p2 = false;
            

    }
	
	// Update is called once per frame
	void Update ()

    {

        // gives p1 an input button which will interact with thier toggle and flip flop it
        if (Input.GetKey(KeyCode.LeftControl) == true)   
        {
          //  if (p1.isOn == false)
                p1.isOn = true;

           // if (p1.isOn == true)
           //     p1.isOn = false;
        }

        // gives p2 an input button which will interact with thier toggle and flip flop it
        if (Input.GetKeyDown(KeyCode.RightControl) == true)
        {
          //  if (p2.isOn == false)
                p2.isOn = true;

          //  if (p2.isOn == true)
           //     p2.isOn = false;
        }




        //when both players are ready the game begins
        if (p1.isOn == true & p2.isOn == true)
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        }
        
	}
}
