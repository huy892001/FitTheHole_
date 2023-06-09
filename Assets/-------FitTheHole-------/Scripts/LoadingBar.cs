using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider _SliderLoading;
    [SerializeField] private GameObject _Slider;
    public static bool firstTime;
    void Start()
    {
        StartCoroutine(LoadScene_Coroutine("Home", 0.4f));
    }
    IEnumerator LoadScene_Coroutine(string SceneName, float speed)
    {
        yield return null;
        _SliderLoading.value = 0;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;

        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, speed * Time.deltaTime);
            _SliderLoading.value = progress;
            if (progress >= 0.9f)
            {
                _SliderLoading.value = 1;
                asyncOperation.allowSceneActivation = true;

                firstTime = true;
            }
            yield return null;
        }
    }
}
