using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerController : MonoBehaviour {

    public GameObject line;
    public float yMin, yMax, speed;
    private bool lineDown, lineMoving;


    // Use this for initialization
    void Start()
    {
        lineDown = false;
        lineMoving = false;
      
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1") && lineMoving == false && lineDown == false)
        {
            lineMoving = true;
            downFrame();
        }

            if (Input.GetButton("Fire1") && lineMoving == false && lineDown == true)
        {
            lineMoving = true;
            upFrame();
        }


        if (lineMoving == (true) && lineDown == false)
        {
            downFrame();
        }

        if (lineMoving == (true) && lineDown == true)
        {
            upFrame();
        }
    }

    void downFrame()
    {
        if (line.transform.position.y > yMin)
        {
            line.transform.position += new Vector3(0.0f, -0.1f * speed * Time.deltaTime, 0.0f);
        }
        if (line.transform.position.y <= yMin)
        {
            lineDown = true;
            lineMoving = false;
        }
    }

    void upFrame()
    {
        if (line.transform.position.y < yMax)
        {
            line.transform.position += new Vector3(0.0f, 0.1f * speed * Time.deltaTime, 0.0f);
        }
        if (line.transform.position.y >= yMax)
        {
            lineDown = false;
            lineMoving = false;
        }
    }

}
