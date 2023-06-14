using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonInHome : Singleton<ButtonInHome>
{
    [SerializeField] private GameObject buttonPlay, buttonSelectLevel, buttonSetting, buttonDecor, buttonCoin;

    public void MoveButtonToOutScene()
    {
        buttonPlay.transform.DOMove(buttonPlay.transform.position + new Vector3(0, -100f, 0), 0.5f);
        buttonSelectLevel.transform.DOMove(buttonSelectLevel.transform.position + new Vector3(-100f, 0, 0), 0.5f);
        buttonSetting.transform.DOMove(buttonSetting.transform.position + new Vector3(100f, 0, 0), 0.5f);
        buttonDecor.transform.DOMove(buttonDecor.transform.position + new Vector3(100f, 0, 0), 0.5f);
        buttonCoin.transform.DOMove(buttonCoin.transform.position + new Vector3(-100f, 0, 0), 0.5f);
    }
}
