using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControl : MonoBehaviour {
    [SerializeField]
    public string carName;
    public GameObject carObject;
    public Sprite carImage;
    public GameObject[] carDoorsArray;
    public GameObject carBody;
    public Vector2 carBodyTextureOffset;
    public Vector2 carDoorTextureOffset;

    Vector3 dist;
    float posX;
    float posY;


    //public SpriteRenderer sr;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   



    public Sprite returnSprite()
    {
        return carImage;
    }

    public void enableCar()
    {
        carObject.SetActive(true);
    }

    public void setCarColour(Material mat)
    {

        Renderer carBodyRenderer = carBody.GetComponent<Renderer>();

        Renderer[] carDoorRenderers = new Renderer[carDoorsArray.Length];

        if (carDoorsArray.Length > 0)
        {
            for (int i = 0; i < carDoorsArray.Length; i++)
            {

                carDoorRenderers[i] = carDoorsArray[i].GetComponent<Renderer>();
            }

            for (int i = 0; i < carDoorRenderers.Length; i++)
            {
                carDoorRenderers[i].material = mat;
                carDoorRenderers[i].material.mainTextureScale = carDoorTextureOffset;
            }
        }
        


        carBodyRenderer.material = mat;
        carBodyRenderer.material.mainTextureScale = carBodyTextureOffset;

        
        


    }

    void OnMouseDown()
    {
        //dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

    }

    void OnMouseDrag()
    {

        Vector3 curPos = new Vector3(Input.mousePosition.x - posX,
                     Input.mousePosition.y - posY, dist.z);
        Debug.Log(posX + "and" + curPos.x);
        carObject.transform.Rotate(0, curPos.x / 300, 0);
    }
}

