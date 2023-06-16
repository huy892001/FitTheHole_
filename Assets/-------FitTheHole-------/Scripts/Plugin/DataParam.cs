using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataParam
{
    public enum GamePanel
    {
        Gameplay,
        Home,
        Collection
    }

    public static string SAVEDATA = "savedata";
    public static DateTime beginShowInter, lastShowInter;
    public static float timeDelayShowAds = 25;                                        // test
    public static bool ShowOpenAds;
    public static int countShowInter;
    public static int levelHintOrSkip;
    public static int countShowVideo;
    public static string packBuyIAP;
    public static bool isShowAppOpen;
    static string path;
    static TextAsset textAsset;
    public static CreateLevel createLevel = new CreateLevel();

    public static bool warningInternetRunTime = false;

    public static bool on_hint_button = true;

    public static string wwwLevel;
    static JsonData json;
    static bool jsonError = false;

    public static void LoadInfoLevel()
    {
        if (!string.IsNullOrEmpty(wwwLevel) && wwwLevel != "" && wwwLevel != "[]")
        {
            try
            {
                json = JsonMapper.ToObject(wwwLevel.ToString());
                Debug.Log("json: " + json);
            }
            catch
            {
                jsonError = true;
            }

            if (jsonError)
            {
                Debug.LogError("loi");
                ReadFromLocal();
            }
            else
            {
                Debug.LogError("ko  loi");
                createLevel = JsonMapper.ToObject<CreateLevel>(json.ToJson());
                loaddonelevel = true;
            }
        }
        else
        {
            Debug.LogError("=======thieu text asset");
            ReadFromLocal();
        }
    }

    public static bool loaddonelevel;
    static void ReadFromLocal()
    {
        path = "Level";
        /*#if UNITY_ANDROID
                path = "LevelAndroid";
        #elif UNITY_IOS
             path = "LevelIOS";
        #endif*/
        textAsset = Resources.Load<TextAsset>("TextAsset/" + path);
        if (textAsset == null)
            return;
        path = textAsset.ToString();

        if (!string.IsNullOrEmpty(path) && path != "" && path != "[]")
        {

            createLevel = JsonMapper.ToObject<CreateLevel>(path);
        }

        //createLevel.version = "ver" + Application.version;
        Debug.LogError("=======Load info level from local");
        Debug.LogError("======= json local:  " + path);
        loaddonelevel = true;
    }
}

