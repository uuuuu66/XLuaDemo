using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

public class BuildAssetBundles : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static public void BuildAllAssetBundles()
    {
        string dir =Application.dataPath + "AssetBundles";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        UnityEngine.Debug.Log("AssetBundle资源打包完成" + Application.dataPath + "/AssetBundles");
        
    }
}
