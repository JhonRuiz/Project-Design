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
        a6.SetActive(true);
        cube.SetActive(false);
        gt86.SetActive(false);

        carSelected = 1;
   
	}

    public void loadA6()
    {
        a6.SetActive(true);
        cube.SetActive(false);
        gt86.SetActive(false);

        carSelected = 1;
    }

    public void loadCube()
    {
        a6.SetActive(false);
        cube.SetActive(true);
        gt86.SetActive(false);

        carSelected = 2;
    }

    public void loadGT86()
    {
        a6.SetActive(false);
        cube.SetActive(false);
        gt86.SetActive(true);

        carSelected = 3;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
