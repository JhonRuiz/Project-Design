using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour {
    public GameObject a6;
    public GameObject cube;
    public GameObject gt86;

    public int carSelected;

	// Use this for initialization
	void Start () {

        a6 = GameObject.Find("Audi A6 AR Test");
        cube = GameObject.Find("Cube");
        gt86 = GameObject.Find("GT86 AR");
        a6.SetActive(false);
        cube.SetActive(false);
        gt86.SetActive(false);

        carSelected = 1;
   
	}

    public void loadA6()
    {
        Globals.curentCar = 1;
        a6.SetActive(true);
        cube.SetActive(false);
        gt86.SetActive(false);

        carSelected = 1;
    }

    public void loadCube()
    {
        Globals.curentCar = 2;
        a6.SetActive(false);
        cube.SetActive(true);
        gt86.SetActive(false);

        carSelected = 2;
    }

    public void loadGT86()
    {
        Globals.curentCar = 3;
        a6.SetActive(false);
        cube.SetActive(false);
        gt86.SetActive(true);

        carSelected = 3;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
