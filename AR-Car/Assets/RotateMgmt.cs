using UnityEngine;
using System.Collections;

public class RotateMgmt : MonoBehaviour
{

    Vector3 dist;
    float posX;
    float posY;
    float originalX;
    public Transform car;

    void OnMouseDown()
    {
        //dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

    }

    void OnMouseDrag()
    {
        
        Vector3 curPos =   new Vector3(Input.mousePosition.x - posX,
                     Input.mousePosition.y - posY, dist.z);
        Debug.Log(posX + "and" + curPos.x);
        car.Rotate(0, curPos.x /300, 0);
    }
}