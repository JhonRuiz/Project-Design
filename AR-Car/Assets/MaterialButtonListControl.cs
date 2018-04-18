/////////////////////////////////////////////////////////
// This class controls the creation of the UI buttons for the material selector menu.
// It loops through the array of materials (mateterialList) and creates a UI button for each
// along with setting the button image to represent the material.
////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UnityEngine.UI is required to interact with Sprite and Texture2D
using UnityEngine.UI;

public class MaterialButtonListControl : MonoBehaviour {

    //Set a template button that will be used to create new buttons.
    [SerializeField]
    private GameObject buttonTemplate;
    //Variable to hold the cars game object. This is used to call CarControl() for each car to set the material.
    public GameObject cars;
    //Array containing all the materials that will be looped through to create menu items.
    public Material[] materialList;

    // On start
    void Start()
    {
        //Loop through materialList array
        for (int i = 0; i < materialList.Length; ++i)
        {
            //Create a new UI button based on the template button
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            //Set the button to active (the template button is not active)
            button.SetActive(true);
            //Set the Material variable on the button to be i
            button.GetComponent<MaterialButtonMgr>().material = (materialList[i]);
            //Set the parent of the new button to be the parent of the template (ButtonListContent)
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            //Get the texture used in the material
            Texture2D t2d = materialList[i].GetTexture("_MainTex") as Texture2D;
            //Set this texture as the sprite for the UI button
            button.GetComponent<Image>().sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
        }
    }

    //Function to change the car materials when clicked/tapped
    public void onClickAction(Material material)
    {
        Globals.currentMaterial = material;
        //For each car in cars array...
        for (int i = 0; i < cars.transform.childCount; i++)
        {
            //...call the setColourCar() method of CarControl (class attached to every car) and pass through the material.
            //This function is called by MaterialButtonMgr
            cars.transform.GetChild(i).GetComponent<CarControl>().setCarColour(material);
        }
    }
}
