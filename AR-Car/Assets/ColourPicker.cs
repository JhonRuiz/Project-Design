using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColourPicker : MonoBehaviour {

    public Material[] BodyColourMat;
    Material CurrMat;
    Renderer renderer;
    Scene scene;

    // Use this for initialization
    void Start () {
        scene = SceneManager.GetActiveScene();
        
        if (scene.name == "AR_SCENE")
        {
            if (Globals.currentColour == 0)
            {
                BlueColour();
            }
            else if (Globals.currentColour == 1)
            {
                RedColour();
            }
            else if (Globals.currentColour == 2)
            {
                GreenColour();
            }
            else if (Globals.currentColour == 3)
            {
                YellowColour();
            }


        }
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }

    void UpdateRenderer()
    {
        renderer = this.GetComponent<Renderer>();
    }
    
    //render blue colour
    public void BlueColour()
    {
        
        Globals.currentColour = 0;
        UpdateRenderer();
        renderer.material = BodyColourMat[0];
        CurrMat = renderer.material;
    }

    //render red colour
    public void RedColour()
    {
        Globals.currentColour = 1;
        UpdateRenderer();
        renderer.material = BodyColourMat[1];
        CurrMat = renderer.material;
    }

    //render green colour
    public void GreenColour()
    {
        Globals.currentColour = 2;
        UpdateRenderer();
        renderer.material = BodyColourMat[2];
        CurrMat = renderer.material;
    }

    //render yellow colour
    public void YellowColour()
    {
        Globals.currentColour = 3;
        UpdateRenderer();
        renderer.material = BodyColourMat[3];
        CurrMat = renderer.material;
    }
}
