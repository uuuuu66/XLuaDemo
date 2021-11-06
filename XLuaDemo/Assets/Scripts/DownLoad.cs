using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System;

public class DownLoad : MonoBehaviour
{
    private string path = @"http://localhost/AssetBundles/newobj.u3d";
    public AssetBundle assetBundle;

    public void StartDownLoad()
    {
        Debug.Log("开始下载更新");
        StartCoroutine(GatAssetBundle(ExcuteHotFix));
    }


    public Slider slider;
    public Text progressText;
    IEnumerator GatAssetBundle(Action callBack)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        www.SendWebRequest();
        while (!www.isDone)
        {
            Debug.Log(www.downloadProgress);
            slider.value = www.downloadProgress;
            progressText.text = Math.Floor(www.downloadProgress * 100) + "%";
            yield return 1;
        }

        if (www.isDone)
        {
            progressText.text = 100 + "%";
            slider.value = 1;
            yield return new WaitForSeconds(1);
            GameObject.Find("Canvas").SetActive(false);
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("DownLoad Err:" + www.error);
        }
        else
        {
            assetBundle = DownloadHandlerAssetBundle.GetContent(www);
            TextAsset hot = assetBundle.LoadAsset<TextAsset>("luaScript.lua.txt");

            string newPath = Application.persistentDataPath + @"/luaScript.lua.txt";
            if (!File.Exists(newPath))
            {
                File.Create(newPath).Dispose();
            }

            File.WriteAllText(newPath, hot.text);
            Debug.Log("下载资源成功！new path:" + newPath);
            callBack();
        }
    }

    public void ExcuteHotFix()
    {
        Debug.Log("开始执行热更新脚本 luaScript");
        LuaEnv luaenv = new LuaEnv();
        luaenv.AddLoader(MyLoader);
        luaenv.DoString("require 'luaScript'");
    }

    public byte[] MyLoader(ref string filePath)
    {
        string newPath = Application.persistentDataPath + @"/" + filePath + ".lua.txt";
        Debug.Log("执行脚本路径：" + newPath);
        string txtString = File.ReadAllText(newPath);
        return System.Text.Encoding.UTF8.GetBytes(txtString);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
