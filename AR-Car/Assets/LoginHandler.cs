﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour {
    //[SerializeField]
    //private string API_Address = Globals.API+"authenticate";
    public InputField username;
    public InputField password;
    public GameObject message;
    private string json;

    public GameObject camera;
    public GameObject LoginCanvas;
    public GameObject CloudConectivity;


	// Use this for initialization
	void Start () {
		if (Globals.authKey == null) {
                LoginCanvas.SetActive(true);
                camera.GetComponent<Animator>().ResetTrigger("isLoggedIn");
        }
        else {
            LoginCanvas.SetActive(false);
            camera.GetComponent<Animator>().SetTrigger("isLoggedIn");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Test()
    {
        
        

        StartCoroutine(Post());
        

    }

    IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest uwr = UnityWebRequest.Post(Globals.API + "/authenticate", form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            if (JSON.Parse(uwr.downloadHandler.text)["success"])
            {
                Debug.Log("success");
                Debug.Log("Received: " + uwr.downloadHandler.text);
                json = uwr.downloadHandler.text;
                Globals.authKey = JSON.Parse(json)["token"];
                LoginCanvas.SetActive(false);
                camera.GetComponent<Animator>().SetTrigger("isLoggedIn");
                CloudConectivity.GetComponent<downloadHandler>().GetCloudBundles();
            }
            else
            {
                Debug.Log("No success");
                message.GetComponent<Text>().text = JSON.Parse(uwr.downloadHandler.text)["msg"];
                message.GetComponent<Text>().color = Color.red;
                message.SetActive(true);
            }
            
        }
        
    }
}
