using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private Image _blackBg;
    string scenes;

    private static LoadingScene _instance;
    [SerializeField] private Canvas _canvasLoading;

    public static LoadingScene Instance
    {
        get
        {
            if (_instance == null)
            {
                var loading = Resources.Load<LoadingScene>("Canvas loading");
                var goLoading = Instantiate(loading);
                _instance = goLoading;
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }


    private void OnDestroy()
    {
        if (_instance != null)
        {
            _instance = null;
        }
    }

    public void Show(string nameScene)
    {
        scenes = nameScene;
        Loading();
    }


    void Loading()
    {
        //Debug.Log(2);
        StartCoroutine(DelayLoadingGame());
    }

    IEnumerator DelayLoadingGame()
    {
        Debug.Log("truoc khi delay");
        yield return new WaitForSeconds(0.5f);

        Debug.Log("bat dau load scene");
        SceneManager.LoadScene(scenes);

        yield return new WaitForSeconds(0.1f);
        //  aop.allowSceneActivation = true;
#if UNITY_ANDROID
        Resources.UnloadUnusedAssets();
#endif


        
    }
}