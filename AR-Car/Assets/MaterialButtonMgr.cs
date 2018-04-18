/////////////////////////////////////////////////////////
// This class is the manager of each button created for selection and changing of materials.
// It manages onClick events and passes it to the MaterialButtonListControl to handle all tasks.
// Buttons should not have any logic to do events themselves, but instead pass them to a central object to handle.
////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UnityEngine.UI is required to interact with Button
using UnityEngine.UI;

public class MaterialButtonMgr : MonoBehaviour {

    //Variable identifying current button
    public Button thisButton;
    //Variable identifing the Material Button Controller game object (MaterialButtonListControl.cs attached to ButtonScrollList)
    public MaterialButtonListControl materialController;
    //The material this button correlates to. 
    public Material material;

    //On start
    void Start()
    {
        //Create an onClick event listener which calls TaskOnClick()
        thisButton.onClick.AddListener(TaskOnClick);
        
    }

    //Function to send the material to the materialController to change the cars' textures.
    void TaskOnClick()
    {
        //Calls the onClickAction of materialController and passes through the material.
        materialController.onClickAction(material);
    }
}
