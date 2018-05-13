using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_open_close : MonoBehaviour {

    public bool door_isOpen;
    public GameObject doorSwatch;
    // Use this for initialization
    void Start()
    {
        door_isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void door_changeState()
    {
        if (!door_isOpen)
        {
            doorSwatch.GetComponent<Animator>().Play("doorSelectionOpen");
            door_isOpen = true;
        }
        else
        {
            doorSwatch.GetComponent<Animator>().Play("doorSelectionClose");
            door_isOpen = false;
        }
    }
}
