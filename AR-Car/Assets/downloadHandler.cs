using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;

public class downloadHandler : MonoBehaviour {
    [SerializeField]
    private GameObject buttonTemplate;
    
    private void Start() {
        GetCloudBundles();
        //StartDownload();
    }

    private void GetCloudBundles() {
        string API = Globals.API +"getAllAssetBundles";
        var form = new WWWForm();
        Dictionary<string, string> headers = new Dictionary<string,string>();
        headers.Add("Authorization", Globals.authKey);
        WWW www = new WWW(API, null, headers);
        StartCoroutine(getData(www));
        
        
    }

    IEnumerator getData(WWW www) {
        yield return www;
        Debug.Log(www.text);
        if (JSON.Parse(www.text)["success"])
            {
                //Debug.Log("success");
                //Debug.Log("Received: " + www.text);
                string json = www.text;
                for (int i = 0; i < JSON.Parse(json)["bundles"].Count; i++) {
                    Debug.Log(JSON.Parse(json)["bundles"][i]["title"]);
                }

                for (int i = 0; i < JSON.Parse(json)["bundles"].Count; ++i)
            {
                //Create a new button from the buttonTemplate game object
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                //Set the new button to active
                button.SetActive(true);
                //Set the sprite of the button to be a pre-defined image of the car (handled by CarControl.cs attached to each car)
                button.GetComponentInChildren<Text>().text = JSON.Parse(json)["bundles"][i]["title"];
                //Set the car game object this image corresponds to.
                //button.GetComponent<ButtonMgr>().setCar(cars.transform.GetChild(i).GetComponent<CarControl>().carObject);
                //Set the parent of the button to the same parent as the button template.
                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }

            }



    }

    private void StartDownload()
    {
        string url = "http://localhost:8080/api/AssetBundleDownload?fileName=audia6";

        var form = new WWWForm();
        Dictionary<string, string> headers = new Dictionary<string,string>();
        headers.Add("Authorization", Globals.authKey);
        
        WWW www = new WWW(url, null, headers);

        StartCoroutine(WaitForReq(www));
    
    }


    IEnumerator WaitForReq(WWW www)
    {
        StartCoroutine(ShowProgress(www));
        yield return www;
        
        AssetBundle bundle = www.assetBundle;
        
        if (www.error == null)
        {
            GameObject audi = bundle.LoadAsset("Audi A6 AR Test Prefab") as GameObject;
            Instantiate(audi);
        }
        else
        {
            Debug.Log(www.error);
        }
    }


    //Soruce https://stackoverflow.com/questions/34376835/show-progress-bar-in-unity-c-sharp-www
    private IEnumerator ShowProgress(WWW www)
    {
        while (!www.isDone)
        {
            Debug.Log(string.Format("Downloaded {0:P1}", www.progress));
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("Done");
    }


}
