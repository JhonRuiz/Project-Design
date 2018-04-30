using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour {
    [SerializeField]
    private string API_Address = "https://arcarnodewebserver.azurewebsites.net/users/authenticate";
    public InputField username;
    public InputField password;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Test()
    {
        
        string json = "{'username' : 'Micah2', 'password' : '123456'";

        StartCoroutine(Post(API_Address));
    }

    IEnumerator Post(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest uwr = UnityWebRequest.Post(API_Address, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
}
