using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class SaveData
{
    public bool offmusic, offsound, offvibra, removeAds, showIntro, rated, requestLoadLevel, FirstTimeInGame, showTutorial;
    public int currentLevel, session, countVideoForRemoveAds, highLevel;
    public List<int> Ls_ID_character = new();

}

public class DataManager : MonoBehaviour
{
    
    public bool enableDebug;
    public SaveData saveData;
    public static DataManager instance;
    public string urlLevelAndroid, urlLevelIOS;


/*    [Header("Data Gameplay")]
    public List<Levelcontroller> Ls_level;
    public List<CharacterInfo> ls_characterInfo;*/

    string strDataLoadPref;
    JsonData jData;
    string urlLevel;

    UnityWebRequest wwwLevel;
    private void Start()
    {

#if UNITY_IOS
        urlLevel = urlLevelIOS;
#else
        urlLevel = urlLevelAndroid;
#endif
        wwwLevel = UnityWebRequest.Get(urlLevel);
        StartCoroutine(WaitForRequestLevel(wwwLevel));
    }

    IEnumerator WaitForRequestLevel(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("======WWW Error Level: " + www.error);
        }
        else
        {
            DataParam.wwwLevel = www.downloadHandler.text;
            Debug.LogError("=====WWW Level!: " + www.downloadHandler.text);
        }


    }
    private void Awake()
    {
        if (instance == null)
        {
            Application.targetFrameRate = 300;
            Debug.unityLogger.logEnabled = enableDebug;
            //Input.multiTouchEnabled = false;
            CultureInfo ci = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllData();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void ReadPlayer(string value)
    {
        strDataLoadPref = null;
        strDataLoadPref = value;
        if (!string.IsNullOrEmpty(strDataLoadPref) && strDataLoadPref != "" && strDataLoadPref != "[]")
        {
            jData = JsonMapper.ToObject(strDataLoadPref);
            if (jData != null)
            {
                saveData = JsonMapper.ToObject<SaveData>(jData.ToJson());
            }
        }

    }
    void LoadData(string value)
    {
        saveData = new SaveData();
        ReadPlayer(value);
    }
    void LoadAllData()
    {
        LoadData(PlayerPrefs.GetString(DataParam.SAVEDATA));
        DataParam.beginShowInter = DataParam.lastShowInter = System.DateTime.Now;

        saveData.session++;
    }
    string tempsaveData, tempsaveMoreGame;
    void SetSaveTemp()
    {
        tempsaveData = JsonMapper.ToJson(saveData);
    }
    void SaveData()
    {
        SetSaveTemp();
        PlayerPrefs.SetString(DataParam.SAVEDATA, tempsaveData);
        PlayerPrefs.Save();
        //Debug.LogError("save");
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }

    public void ShowInter()
    {
        if (saveData.removeAds)
            return;
        DataParam.lastShowInter = System.DateTime.Now;

        if ((DataParam.lastShowInter - DataParam.beginShowInter).TotalSeconds > DataParam.timeDelayShowAds)
        {
            if (AdsController.instance != null)
            {
                AdsController.instance.ShowInter();
                EventController.AF_INTERS_AD_ELIGIBLE();
            }
        }
    }

    public void RemoveAdsFunc()
    {
        saveData.removeAds = true;
        //GameController.gameController.settingPopUp.DisplayBtn();
        AdsController.instance.HideBanner();
        AdsController.instance.HideNativeAds();
    }


    
}
