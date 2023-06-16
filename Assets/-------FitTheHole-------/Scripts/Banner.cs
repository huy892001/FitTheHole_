using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour
{
    void Start()
    {
        AppOpenAdManager.Instance.ShowAdIfAvailable();
        AdsController.instance.ShowBanner();
    }
}
