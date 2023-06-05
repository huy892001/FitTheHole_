using Spine.Unity;
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
        if (PlayerPrefs.HasKey("Chapter"))
        {
            Instantiate(Resources.Load<GameObject>("Character/Chapter " + PlayerPrefs.GetInt("Chapter") + "/Level " + PlayerPrefs.GetInt("Level")));
            animationOfHuntCharacter = Instantiate(Resources.Load<SkeletonAnimation>("Hunter/Chapter " + PlayerPrefs.GetInt("Chapter")));
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Character/Chapter 1/Level 1"));
            animationOfHuntCharacter = Instantiate(Resources.Load<SkeletonAnimation>("Hunter/Chapter 1"));
        }
        animationOfHuntCharacter.transform.position = new Vector3(-3,-6,1);
        UpdateState(GameState.Start);
    }
    public void UpdateState(GameState newstate)
    {
        gameState = newstate;
        switch (newstate)
        {
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
    private void HandleStart()
    {

    }

    private void HandleWin()
    {
        PlayAnimationHuntCharacterComplete();
    }

    private void HandleLose()
    {
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
    Start,
    Win,
    Lose
}
