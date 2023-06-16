using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonInHome : Singleton<ButtonInHome>
{
    public void ButtonMoveOutHomeScene()
    {
        gameObject.transform.GetComponent<Animation>().Play("AnimationButtonMoveOutHome");
    }
}
