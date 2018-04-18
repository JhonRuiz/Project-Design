using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAR : MonoBehaviour {

    public GameObject cars;
    public Material[] BodyColourMat;
    Material CurrMat;
    Renderer renderer;


    // Use this for initialization
    void Start () {

        for (int i = 0; i < cars.transform.childCount; i++)
        {
            //Disable each car
            Debug.Log(Globals.curentCar);

            if (i != Globals.curentCar)
            {
                cars.transform.GetChild(i).gameObject.SetActive(false);
                
            }
            else
            {
                cars.transform.GetChild(i).gameObject.SetActive(true);
                cars.transform.GetChild(i).GetComponent<CarControl>().setCarColour(Globals.currentMaterial);
            }
            
            
        }

          
            
        

    }

    // Update is called once per frame
    void Update () {
		
	}
}
