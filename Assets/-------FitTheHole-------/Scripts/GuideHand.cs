using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class GuideHand : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation guideHandForTutorialLevel;
    [SerializeField] private CharacterManager characterForInstructions;
    [SerializeField] private UnityEngine.Animation moveGuideHandToShape;
    [SerializeField] private GameObject correctObjectForStateGuideHand;
    private bool oneMoreTimeForMoveGuideHand = true;
    private void Awake()
    {
        if(PlayerPrefs.HasKey("Text Guide Hand"))
        {
            if (PlayerPrefs.GetInt("Text Guide Hand") == 0)
            {
                gameObject.SetActive(false);
                PlayerPrefs.SetInt("Text Guide Hand", 1);
            }
        }
    }
    private void Start()
    {
        guideHandForTutorialLevel.state.SetAnimation(0, "animation", true);
        TextGuidHand.Instance.textTapToChange.SetActive(true);
    }
    private void Update()
    {
        if(GameManager.Instance.gameState == GameState.Lose)
        {
            gameObject.SetActive(false);
        }
        if (characterForInstructions.characterState == CharacterState.Change && correctObjectForStateGuideHand.activeSelf)
        {
            if (oneMoreTimeForMoveGuideHand)
            {
                TextGuidHand.Instance.textTapToChange.SetActive(false);
                TextGuidHand.Instance.textMoveToFitIt.SetActive(true);
                //float animationDuration = guideHandForTutorialLevel.Skeleton.Data.FindAnimation("animation").Duration; // tìm thời gian chạy của 1 animation trong skeletonanimation
                StartCoroutine(HanderMoveGuideHandToShapeAfter());
                oneMoreTimeForMoveGuideHand = false;
            }
        }
    }

    private IEnumerator HanderMoveGuideHandToShapeAfter()
    {
        while (true)
        {
            if (characterForInstructions.characterState == CharacterState.Complete)
            {
                gameObject.SetActive(false);
            }
            guideHandForTutorialLevel.state.SetAnimation(0, "animation", false);
            yield return new WaitForSeconds(moveGuideHandToShape.GetClip("AnimationMoveGuideHandToShape").length);
            moveGuideHandToShape.Play();
        }
    }
}


