using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonReceive : MonoBehaviour
{
    public void HandlerReceiveToSceneSelectLevel()
    {
        SceneManager.LoadSceneAsync("SelectLevel");
    }
}
