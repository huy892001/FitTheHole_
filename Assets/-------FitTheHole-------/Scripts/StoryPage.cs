using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoryPage : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("Page Story"))
        {
            if (PlayerPrefs.GetInt("Page Story") == 0)
            {
                gameObject.SetActive(false);
                PlayerPrefs.SetInt("Page Story", 1);
            }
        }
        else
        {
            GameManager.Instance.gameState = GameState.Story;
            StartCoroutine(PageStoryIsEnable());
        }
    }
    
    private IEnumerator PageStoryIsEnable()
    {
        yield return new WaitForSeconds(4);
        gameObject.transform.GetComponent<SpriteRenderer>().color = Color.black;
        gameObject.transform.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.7f);
        yield return new WaitForSeconds(0.7f);
        GameManager.Instance.gameState = GameState.Start;
        gameObject.SetActive(false);
    }
}
