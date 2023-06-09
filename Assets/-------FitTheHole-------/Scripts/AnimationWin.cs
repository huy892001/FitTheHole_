using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AnimationWin : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animationOfWinContext;
    private void Start()
    {
        animationOfWinContext.state.SetAnimation(0, "Win", false);
        float animationDuration = animationOfWinContext.Skeleton.Data.FindAnimation("Win").Duration;
        StartCoroutine(HandlerWaitWinPanelStart(animationDuration));
    }

    private IEnumerator HandlerWaitWinPanelStart(float time)
    {
        yield return new WaitForSeconds(time);
        animationOfWinContext.state.SetAnimation(0, "Win2", true);
    }
}
