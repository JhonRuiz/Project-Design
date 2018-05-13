using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavOpenRearLeftDoor : MonoBehaviour {

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
            doorSwatch.GetComponent<Animator>().Play("Rav4RearDoorOpen");
            door_isOpen = true;
        }
        else
        {
            doorSwatch.GetComponent<Animator>().Play("Rav4RearDoorClose");
            door_isOpen = false;
        }
    }
}
