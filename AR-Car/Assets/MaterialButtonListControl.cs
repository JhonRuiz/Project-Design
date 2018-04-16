using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialButtonListControl : MonoBehaviour {

    public GameObject cars;


    // Use this for initialization
    public void onClickAction(Material material)
    {
        for (int i = 0; i < cars.transform.childCount; i++)
        {
            cars.transform.GetChild(i).GetComponent<CarControl>().setCarColour(material);
        }
    }
}
