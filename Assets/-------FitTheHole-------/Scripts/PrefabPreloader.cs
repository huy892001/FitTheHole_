using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefabPreloader : Singleton<PrefabPreloader>
{
    public void LoadSceneOver()
    {
        StartCoroutine(DelayLoadingGame());
    }

    IEnumerator DelayLoadingGame()
    {
        yield return new WaitForSeconds(0.7f);
        //   anim.Play("IdleLoad");
        /*    AsyncOperation aop = SceneManager.LoadSceneAsync(scenes);
    
            while (!aop.isDone)
            {
                yield return null;
            }*/
        //Debug.Log(3);
        Debug.Log("bat dau load scene");
        LoadingScene.Instance.Show("Gameplay 1");



        yield return new WaitForSeconds(0.1f);
        //  aop.allowSceneActivation = true;
#if UNITY_ANDROID
        Resources.UnloadUnusedAssets();
#endif
    }

}
