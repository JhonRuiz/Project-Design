using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAR : MonoBehaviour {

    public GameObject a6;
    public GameObject cube;
    public GameObject gt86;



    // Use this for initialization
    void Start () {

        

        a6.SetActive(false);
        cube.SetActive(false);
        gt86.SetActive(false);

        Debug.Log(Globals.curentCar);

        if (Globals.curentCar == 1)
        {
            a6.SetActive(true);
        }
        if (Globals.curentCar == 2)
        {
            cube.SetActive(true);
        }
        if (Globals.curentCar == 3)
        {
            gt86.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
