﻿using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Spine;
public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public SkeletonAnimation animationOfHuntCharacter;
    public GameObject WinPanel, LosePanel;
    public static event Action<GameState> OnGamestateChanged;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Hunter"))
        {
            Instantiate(Resources.Load<GameObject>("Character/Chapter " + PlayerPrefs.GetInt("Chapter") + "/Level " + PlayerPrefs.GetInt("Level")));
            animationOfHuntCharacter = Instantiate(Resources.Load<SkeletonAnimation>("Hunter/Hunter " + PlayerPrefs.GetInt("Hunter")));
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Character/Chapter 1/Level 1"));
            animationOfHuntCharacter = Instantiate(Resources.Load<SkeletonAnimation>("Hunter/Hunter 1"));
            PlayerPrefs.SetInt("Chapter", 1);
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Hunter", 1);
        }
        animationOfHuntCharacter.transform.position = new Vector3(-2, -6, 1);
        UpdateState(GameState.Start);
    }
    public void UpdateState(GameState newstate)
    {
        gameState = newstate;
        switch (newstate)
        {
            case GameState.Story:
                HandleStory();
                break;
            case GameState.Start:
                HandleStart();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                break;
        }
        OnGamestateChanged?.Invoke(newstate);
    }

    private void HandleStory()
    {

    }
    private void HandleStart()
    {

    }

    private void HandleWin()
    {
        if (!PlayerPrefs.HasKey("Text Guide Hand"))
        {
            PlayerPrefs.SetInt("Text Guide Hand", 0);
        }
        if (!PlayerPrefs.HasKey("Page Story"))
        {
            PlayerPrefs.SetInt("Page Story", 0);
        }
        PlayAnimationHuntCharacterComplete();
    }

    private void HandleLose()
    {
        TextGuidHand.Instance.textTapToChange.SetActive(false);
        PlayAnimationHuntCharacterLose();
    }
    public void PlayAnimationHuntCharacterComplete()
    {
        animationOfHuntCharacter.state.SetAnimation(0, "Win", false);
    }


    public void PlayAnimationHuntCharacterLose()
    {
        animationOfHuntCharacter.state.SetAnimation(0, "Lose", false);
    }
}

public enum GameState
{
    Story,
    Start,
    Win,
    Lose
}
