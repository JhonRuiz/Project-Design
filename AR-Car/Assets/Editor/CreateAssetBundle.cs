using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateAssetBundles : MonoBehaviour
{
    [MenuItem("Assets/Build Windows AssetBundles")]
    static void BuildWindowsAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Windows", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
    [MenuItem("Assets/Build iOS AssetBundles")]
    static void BuildiOSAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/iOS", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
    [MenuItem("Assets/Build Android AssetBundles")]
    static void BuildAndroidAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}