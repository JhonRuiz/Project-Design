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
