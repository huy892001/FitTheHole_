using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance = null;


    void Awake()
    {
        if (!PlayerPrefs.HasKey("Playable Chapter"))
        {
            PlayerPrefs.SetInt("Playable Chapter", 1);
            PlayerPrefs.SetInt("Index Anim Icon Chapter", 1);
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
