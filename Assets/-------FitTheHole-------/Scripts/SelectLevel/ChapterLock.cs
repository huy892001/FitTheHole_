using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLock : MonoBehaviour
{
    [SerializeField] private int chapterNumber;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Playable Chapter"))
        {
            PlayerPrefs.SetInt("Playable Chapter", 1);
            PlayerPrefs.SetInt("Index Anim Icon Chapter", 1);
            PlayerPrefs.SetInt("Unlock Chapter " + chapterNumber, 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("Playable Chapter") == chapterNumber)
            {
                if (PlayerPrefs.GetInt("Unlock Chapter " + chapterNumber) == 0)
                {
                    gameObject.GetComponent<Animation>().Play();
                    float index = gameObject.transform.GetComponent<Animation>().GetClip("AnimationOfChapterLock").length;
                    StartCoroutine(CountdownAnimationOfChapterLock(index));
                    Debug.Log(1);
                    PlayerPrefs.SetInt("Unlock Chapter " + chapterNumber, 1);
                    Debug.Log(PlayerPrefs.GetInt("Unlock Chapter " + chapterNumber));
                }
                else if (PlayerPrefs.GetInt("Unlock Chapter " + chapterNumber) == 1)
                {
                    gameObject.SetActive(false);
                }
            }
            
        }
    }

    private IEnumerator CountdownAnimationOfChapterLock(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
