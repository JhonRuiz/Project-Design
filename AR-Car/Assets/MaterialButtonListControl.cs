using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialButtonListControl : MonoBehaviour {

    [SerializeField]
    private GameObject buttonTemplate;
    public GameObject cars;
    public Material[] materialList;

    // Use this for initialization
    void Start()
    {


        for (int i = 0; i < materialList.Length; ++i)
        {
            Debug.Log("got here mater");
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            //cars.transform.GetChild(i).GetComponent<CarControl>.carImage;
            button.GetComponent<MaterialButtonMgr>().material = (materialList[i]);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            //Texture test = materialList[i].
            Texture2D t2d = materialList[i].GetTexture("_MainTex") as Texture2D;
            Debug.Log(t2d);
            button.GetComponent<Image>().sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
        }
    }





        public void onClickAction(Material material)
    {
        for (int i = 0; i < cars.transform.childCount; i++)
        {
            cars.transform.GetChild(i).GetComponent<CarControl>().setCarColour(material);
        }
    }
}
