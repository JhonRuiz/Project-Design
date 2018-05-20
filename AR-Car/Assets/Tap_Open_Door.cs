using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;


namespace Lean.Touch
{
	// This script calls the OnFingerTap event when a finger taps the screen
	public class Tap_Open_Door : MonoBehaviour
	{





        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                    
                    if (hit.transform.GetComponent<Animator>().GetBool("isOpen") == false) {
                        hit.transform.GetComponent<Animator>().SetTrigger("isOpen");
                    }
                    else {
                        hit.transform.GetComponent<Animator>().ResetTrigger("isOpen");
                    }
                    
                    

                }



                }
        }
    
        

        
        
    }
}