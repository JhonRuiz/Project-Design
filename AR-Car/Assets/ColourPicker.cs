using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPicker : MonoBehaviour {

    public Material[] BodyColourMat;
    Material CurrMat;
    Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = this.GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //render blue colour
    public void BlueColour()
    {
        renderer.material = BodyColourMat[0];
        CurrMat = renderer.material;
    }

    //render red colour
    public void RedColour()
    {
        renderer.material = BodyColourMat[1];
        CurrMat = renderer.material;
    }

    //render green colour
    public void GreenColour()
    {
        renderer.material = BodyColourMat[2];
        CurrMat = renderer.material;
    }

    //render yellow colour
    public void YellowColour()
    {
        renderer.material = BodyColourMat[3];
        CurrMat = renderer.material;
    }
}
