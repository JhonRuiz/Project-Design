using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionAppearBtn : MonoBehaviour {

    public bool swatch_isOpen;
    public GameObject carSwatch;
    // Use this for initialization
    void Start()
    {
        swatch_isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void swatch_changeState()
    {
        if (!swatch_isOpen)
        {
            carSwatch.GetComponent<Animator>().Play("carSelectionOpen");
            swatch_isOpen = true;
        }
        else
        {
            carSwatch.GetComponent<Animator>().Play("carSelectionClose");
            swatch_isOpen = false;
        }
    }
}
