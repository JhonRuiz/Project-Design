using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadButtonMgr : MonoBehaviour {
	[SerializeField]
    private DownloadButtonListControl downloadButtonControl;
    public Button yourButton;
	public downloadHandler download;
    public string fileName;
	public string prefabName;


	// Use this for initialization
	void Start () {
		Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	void TaskOnClick() {
		download.StartDownload(fileName, prefabName);
	}
}
