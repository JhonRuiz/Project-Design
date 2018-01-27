﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwatchScript : MonoBehaviour {

    public bool swatch_isOpen;
    public GameObject colorSwatch;
	// Use this for initialization
	void Start () {
        swatch_isOpen = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void swatch_changeState()
    {
        if (!swatch_isOpen)
        {
            colorSwatch.GetComponent<Animator>().Play("colorSwatchOpens");
            swatch_isOpen = true;
        }else {
            colorSwatch.GetComponent<Animator>().Play("colorSwatchCloses");
            swatch_isOpen = false;
        }
    }
}
