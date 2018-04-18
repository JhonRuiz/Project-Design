/////////////////////////////////////////////////////////
// This class is the manager of each button created for selection and changing of cars.
// It manages onClick events and passes it to the ButtonListControl.cs to handle all tasks.
// Buttons should not have any logic to do events themselves, but instead pass them to a central object to handle.
////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMgr : MonoBehaviour {

    [SerializeField]
    private ButtonListControl buttonControl;
    public Image sr;
    public Button yourButton;
    public GameObject car;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    public void setSprite(Sprite sprite)
    {
        sr.sprite = sprite;
    }

    public void setCar(GameObject newCar)
    {
        Debug.Log(newCar);
        car = newCar;
    }

    void TaskOnClick()
    {
        buttonControl.onClickAction(car);
    }


}
