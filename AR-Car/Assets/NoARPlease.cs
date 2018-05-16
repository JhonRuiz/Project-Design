using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;


public class NoARPlease : MonoBehaviour
{

    private List<Camera> _cameras = new List<Camera>();

    private VuforiaBehaviour _vuforiaBehaviour;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FixCameras;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FixCameras;
    }

    void FixCameras(Scene scene, LoadSceneMode mode)
    {
        _cameras.AddRange(Camera.allCameras);

        foreach (var c in _cameras)
        {
            _vuforiaBehaviour = c.GetComponent<VuforiaBehaviour>();

            if (_vuforiaBehaviour != null && !_vuforiaBehaviour.enabled)
            {
                Destroy(_vuforiaBehaviour);
            }

            _vuforiaBehaviour = null;
        }

        _cameras.Clear();
    }
}