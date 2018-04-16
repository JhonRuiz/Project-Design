using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialButtonMgr : MonoBehaviour {
    public Button yourButton;
    public MaterialButtonListControl materialController;
    public Material material;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        materialController.onClickAction(material);
    }
}
