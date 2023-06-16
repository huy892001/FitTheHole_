using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LosePanel : Singleton<LosePanel>
{
    [SerializeField] private TMP_Text textOfCoinDeducted;
    private int currentGold;
    private void Start()
    {
        currentGold = PlayerPrefs.GetInt("Coin");
        textOfCoinDeducted.text = currentGold.ToString();
    }
    public void IncreaseGold(int coin)
    {
        int targetGold = currentGold - coin;

        DOTween.To(() => currentGold, x => currentGold = Mathf.RoundToInt(x), targetGold, 3f)
           .OnUpdate(() =>
           {
               textOfCoinDeducted.text = currentGold.ToString();
           })
            .OnComplete(() =>
            {
                textOfCoinDeducted.text = targetGold.ToString();
                PlayerPrefs.SetInt("Coin", targetGold);
            });
    }
}
