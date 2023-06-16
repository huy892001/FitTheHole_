using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSkip : MonoBehaviour
{
    [SerializeField] private int coinDeducted;
    private bool oneClickHandlerX2Coin = false;
    public void HandlerCoinDeducted()
    {
        if (!oneClickHandlerX2Coin)
        {
            AdsController.instance.ShowVideo(show, "Coin Deducted");
            PlayerPrefs.SetInt("Playable Level", (PlayerPrefs.GetInt("Level") + 1));
            LosePanel.Instance.IncreaseGold(coinDeducted);
            oneClickHandlerX2Coin = true;
        }
    }

    private void show()
    {
        Debug.Log("Trừ Coin");
    }
}
