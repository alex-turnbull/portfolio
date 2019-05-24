using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {

    public GameObject ballObject;
    private SphereCollider ballCollider;
    [SerializeField]
    private float force;

    private Vector3 initialPos;
    private Quaternion initialRot;

    //public Quaternion rot;

    private bool hasBall;

    private shaderHandler shaderHandleScript;

    public GameObject player;

    void Start () {
        initialPos = ballObject.transform.localPosition;
        initialRot = ballObject.transform.localRotation;
        ballCollider = ballObject.GetComponent<SphereCollider>();

        shaderHandleScript = ballObject.GetComponent<shaderHandler>();
    }
	
	void OnMouseDown () {
	}

    void Update()
    {
        if(gameObject.transform.childCount != 0)
        {
            hasBall = true;
            ballObject.layer = 9;
        }
        else
        {
            hasBall = false;
        }

        if(Vector3.Distance(ballObject.transform.position, gameObject.transform.position) > 40.0f)
        {
            resetBall(ballObject);
        }

        if (Input.GetButtonDown("Jump"))
        {
            print(GameHandler.Instance.currentLevel);
        }

        if (GameHandler.Instance.gameState == GameHandler.gameStates.navigating)
        {
            if (Input.GetMouseButtonDown(0) && hasBall)
            {
                throwBall();
            }

            if (Input.GetMouseButtonDown(1))
            {
                resetBall(ballObject);
            }
        }       

        Debug.DrawRay(ballObject.transform.position, ballObject.transform.forward * force, Color.red);
    }

    public void throwBall()
    {        
        ballObject.transform.parent = null;
        Rigidbody ballRB = ballObject.GetComponent<Rigidbody>();
        ballRB.useGravity = true;
        ballRB.AddForce(ballObject.transform.forward * force, ForceMode.Impulse);
        Invoke("DelayCollide", 0.05f);
    }

    public void resetBall(GameObject ball)
    {
        ball.transform.parent = this.gameObject.transform;
        ball.transform.localPosition = initialPos;
        ball.transform.localRotation = initialRot;
        Rigidbody ballRB2 = ball.GetComponent<Rigidbody>();
        ballRB2.useGravity = false;
        ballRB2.velocity = Vector3.zero;
        ballRB2.angularVelocity = Vector3.zero;

        shaderHandleScript.canRipple = true;
        shaderHandleScript.count = 0;
    }

    void DelayCollide()
    {
        ballObject.layer = 8;
    }
}
