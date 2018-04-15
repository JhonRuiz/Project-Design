using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControl : MonoBehaviour {
    [SerializeField]
    public string carName;
    public GameObject carObject;
    public Sprite carImage;

    //public SpriteRenderer sr;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Sprite returnSprite()
    {
        return carImage;
    }

    public void enableCar()
    {
        carObject.SetActive(true);
    }
}
