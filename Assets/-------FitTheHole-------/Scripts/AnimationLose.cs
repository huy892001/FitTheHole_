using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLose : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animationOfLoseContext;
    private void Start()
    {
        animationOfLoseContext.state.SetAnimation(0, "Lose", false);
        float animationDuration = animationOfLoseContext.Skeleton.Data.FindAnimation("Lose").Duration;
        StartCoroutine(HandlerWaitWinPanelStart(animationDuration));
    }

    private IEnumerator HandlerWaitWinPanelStart(float time)
    {
        yield return new WaitForSeconds(time);
        animationOfLoseContext.state.SetAnimation(0, "Lose2", true);
    }
}
