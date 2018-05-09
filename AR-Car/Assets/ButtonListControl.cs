/////////////////////////////////////////////////////////
// This class controls the creation of the UI buttons for the car selector menu.
// It loops through the children of gameObject cars and creates a UI button for each
// along with setting the button image to represent the car.
////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour {

    //Set a template button that will be used to create new buttons.
    [SerializeField]
    private GameObject buttonTemplate;
    //Game Object that contains all the car models
    public GameObject cars;
    //Canvas game object that hold logic to disable car menu
    public GameObject canvas;
    public GameObject content;

    //On start
    void Start()
    {
        initMenu();
    }

    public void initMenu() {
        //Number of cars in cars game object
        int numChildren = cars.transform.childCount;
        //For each car...
       

        for (int i = 0; i < numChildren; ++i)
        {
            createMenuItem(cars.transform.GetChild(i).GetComponent<CarControl>());
        }

        //This for loop is just to demonstrate the menu working with multiple cars. Logic is the same as above, just in a different loop.
        /*
        for (int i = 1; i <= 20; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonMgr>().setCar(cars.transform.GetChild(0).GetComponent<CarControl>().carObject);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
         */
    }
    
    public void createMenuItem(CarControl ctrl) {
        //Create a new button from the buttonTemplate game object
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            //Set the new button to active
            button.SetActive(true);
            //Set the sprite of the button to be a pre-defined image of the car (handled by CarControl.cs attached to each car)
            button.GetComponent<ButtonMgr>().setSprite(ctrl.carImage);
            //Set the car game object this image corresponds to.
            button.GetComponent<ButtonMgr>().setCar(ctrl.carObject);
            //Set the parent of the button to the same parent as the button template.
            button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

    //Function to handle the action when menu button is selected. Called from ButtonMgr.cs - requires the car to be passed through
    public void onClickAction(GameObject car)
    {
        //Loop through each car in game object cars
        for (int i = 0; i < cars.transform.childCount; i++)
        {
            //Disable each car
            cars.transform.GetChild(i).gameObject.SetActive(false);
            if (car == cars.transform.GetChild(i).gameObject)
            {
                Globals.curentCar = i;
            }
        }
        //Set the car that was passed through to active.
        car.SetActive(true);
        //Set the global variable for selected car (for passthrough to AR)
       
        Debug.Log("Setting car to");
        Debug.Log(Globals.curentCar);
        //Remove the car selection menu now that a car has been selected.
        canvas.GetComponent<menu>().ToggleCarSelector();
    }

   
}
