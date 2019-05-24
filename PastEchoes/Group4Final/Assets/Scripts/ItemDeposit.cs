using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeposit : MonoBehaviour
{
    public GameObject itemToDeposit;

    public string compareToStore { get; set; }

    void Start()
    {
        compareToStore = itemToDeposit.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
