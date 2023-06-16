using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private TMP_Text textCoin;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            textCoin.text = PlayerPrefs.GetInt("Coin").ToString();
        }
        else
        {
            textCoin.text = "0";
        }
    }
}
