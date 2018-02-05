using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class Tap_Open_Door_New : MonoBehaviour, IPointerDownHandler, IPointerUpHandler // required interface when using the OnPointerDown method.

{
    bool pointerDown = false;
    public Transform car;
    int direction;

    // Use this for initialization
    void Start () {
    
        }

// Update is called once per frame
       void Update () {
        
        if (pointerDown == true)
        {
            foreach (Transform child in car)
            {
               
                    child.transform.Rotate(0,direction, 0);
                
                
            }
        }

        
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        Debug.Log(eventData);
        pointerDown = true;
        if (this.gameObject.name == "btn_left_arrow")
        {
            direction = 1;
        }
        else if (this.gameObject.name == "btn_right_arrow")
        {
            direction = -1;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }
}
