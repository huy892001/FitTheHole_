using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonX2 : MonoBehaviour
{
    private bool oneClickHandlerX2Coin = false;
    public void HandlerX2Coin()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            if (!oneClickHandlerX2Coin)
            {
                AdsController.instance.ShowVideo(show, "X2_Coin");
                WinPanel.Instance.HandlerX2Coin();
                oneClickHandlerX2Coin = true;
            }
        }
    }

    private void show()
    {
        Debug.Log("Nhận X2 Coin");
    }
}
