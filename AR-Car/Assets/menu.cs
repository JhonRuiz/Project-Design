using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {


    public static SceneManager Instance;
    Scene scene;
    public GameObject car_selector_interface;
    public GameObject colour_selector_interface;
    public GameObject download_selector_interface;
    public CanvasGroup canvasgroup;
    Color colorToFadeTo;
    // Use this for initialization
    void Start () {

        //car_selector_interface = GameObject.Find("car_selector_interface");
        //colour_selector_interface = GameObject.Find("colour_selector_interface");
        scene = SceneManager.GetActiveScene();


        if (scene.name == "3D_SCENE TEST")
        {
            ToggleColourSelector();
        }
        


    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeScene()
    {
        if (scene.name == "3D_SCENE TEST")
        {
            Debug.Log("Changing Scene");
            SceneManager.LoadScene("AR_SCENE");
        }
        else
        {
            Debug.Log("Changing Scene");
            SceneManager.LoadScene("3D_SCENE TEST");
        }
        
    }

    public void ToggleCarSelector()
    {
        if (car_selector_interface.gameObject.activeSelf == true)
        {


            car_selector_interface.gameObject.SetActive(false);
            Debug.Log("Removing interface");
        }
        else
        {
            car_selector_interface.gameObject.SetActive(true);
            Debug.Log("Displaying interface");
        }


    }

    public void ToggleDownloadSelector()
    {
        if (download_selector_interface.gameObject.activeSelf == true)
        {
            download_selector_interface.gameObject.SetActive(false);
            Debug.Log("Removing interface");
        }
        else
        {
            download_selector_interface.gameObject.SetActive(true);
            Debug.Log("Displaying interface");
        }


    }

    public void ToggleColourSelector()
    {
        if (colour_selector_interface.gameObject.activeSelf == true)
        {
            colour_selector_interface.gameObject.SetActive(false);
            Debug.Log("Removing interface");
        }
        else
        {
            colour_selector_interface.gameObject.SetActive(true);
            Debug.Log("Displaying interface");
        }


    }

   
    
}
