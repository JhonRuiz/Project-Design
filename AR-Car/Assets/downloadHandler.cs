using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;

public class downloadHandler : MonoBehaviour {
    [SerializeField]
    private GameObject buttonTemplate;
    public GameObject cars;

    public ButtonListControl btnListCtrl;
    

    #if UNITY_IOS
    string platform = "iOS";
    #endif
    #if UNITY_ANDROID
    string platform = "Android";
    #endif
    #if UNITY_STANDALONE_WIN
    string platform = "Windows";
    #endif
    
    
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
                Debug.Log("The platform is " +platform);
                for (int i = 0; i < JSON.Parse(json)["bundles"].Count; ++i)
            {
                if (JSON.Parse(json)["bundles"][i]["platform"] == platform) {
                    //Create a new button from the buttonTemplate game object
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                //Set the new button to active
                button.SetActive(true);
                //Set the sprite of the button to be a pre-defined image of the car (handled by CarControl.cs attached to each car)
                button.GetComponentInChildren<Text>().text = JSON.Parse(json)["bundles"][i]["title"];
                button.GetComponent<DownloadButtonMgr>().fileName = JSON.Parse(json)["bundles"][i]["fileLocation"];
                button.GetComponent<DownloadButtonMgr>().prefabName = JSON.Parse(json)["bundles"][i]["prefab"];

                //Set the parent of the button to the same parent as the button template.
                button.transform.SetParent(buttonTemplate.transform.parent, false);


                }
                
            }

            }



    }
    

    public void StartDownload(string _FileName, string Prefab)
    {
        string url = Globals.API + "AssetBundleDownload?fileName=" + _FileName;

        var form = new WWWForm();
        Dictionary<string, string> headers = new Dictionary<string,string>();
        headers.Add("Authorization", Globals.authKey);
        
        WWW www = new WWW(url, null, headers);

        StartCoroutine(WaitForReq(www, Prefab));
    
    }


    IEnumerator WaitForReq(WWW www, string prefab)
    {
        StartCoroutine(ShowProgress(www));
        yield return www;
        
        AssetBundle bundle = www.assetBundle;
        
        if (www.error == null)
        {
            Debug.Log("PREFAB IS " + prefab);
            GameObject newCarFromDownload = bundle.LoadAsset<GameObject>(prefab);
            
            GameObject obj = Instantiate(newCarFromDownload);
            obj.transform.parent = cars.transform;
            obj.transform.localPosition = new Vector3(-901.403f,-400.192f,-0.0085f);
            Debug.Log("Added new car");
            btnListCtrl.createMenuItem(obj.GetComponent<CarControl>());
            btnListCtrl.onClickAction(obj);
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
