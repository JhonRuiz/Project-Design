using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavOpenRearRightDoor : MonoBehaviour {

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
            doorSwatch.GetComponent<Animator>().Play("Rav4RearRightDoorOpen");
            door_isOpen = true;
        }
        else
        {
            doorSwatch.GetComponent<Animator>().Play("Rav4RearRightDoorClose");
            door_isOpen = false;
        }
    }
}
