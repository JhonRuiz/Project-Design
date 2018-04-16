using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListControl : MonoBehaviour {

    [SerializeField]
    private GameObject buttonTemplate;
    //private List<int> intList;
    public GameObject cars;
    public GameObject canvas;

    void Start()
    {
        int numChildren = cars.transform.childCount;
        Debug.Log(numChildren);
        for (int i = 0; i < numChildren; ++i)
        {
            Debug.Log("working on child" + i);
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            //cars.transform.GetChild(i).GetComponent<CarControl>.carImage;
            button.GetComponent<ButtonMgr>().setSprite(cars.transform.GetChild(i).GetComponent<CarControl>().carImage);

            button.GetComponent<ButtonMgr>().setCar(cars.transform.GetChild(i).GetComponent<CarControl>().carObject);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
            
        }



        for (int i = 1; i <= 20; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            //button.GetComponent<ButtonListButton>().setText("Button #" + i);
            Debug.Log(button);
            button.GetComponent<ButtonMgr>().setCar(cars.transform.GetChild(0).GetComponent<CarControl>().carObject);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
            Debug.Log("test2");
        }
    }

    public void onClickAction(GameObject car)
    {
        Debug.Log("this worked");
        for (int i = 0; i < cars.transform.childCount; i++)
        {
            cars.transform.GetChild(i).gameObject.SetActive(false);
        }
        car.SetActive(true);
        canvas.GetComponent<menu>().ToggleCarSelector();
    }

   
}
