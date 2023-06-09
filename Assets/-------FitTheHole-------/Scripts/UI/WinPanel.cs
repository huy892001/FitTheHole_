using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject iconChapter, backgroundImageOfCompleteChapter;
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Level") >= PlayerPrefs.GetInt("Playable Level"))
        {
            PlayerPrefs.SetInt("Playable Level", (PlayerPrefs.GetInt("Level") + 1));
        }
        if (PlayerPrefs.HasKey("Chapter"))
        {
            if (PlayerPrefs.GetInt("Chapter") >= PlayerPrefs.GetInt("Playable Chapter"))
            {
                IconOfBackgroundChapter backgroundChapter = Resources.Load<IconOfBackgroundChapter>("ScriptsTableObject/Chapter " + (PlayerPrefs.GetInt("Chapter") + 1));
                if (backgroundChapter != null)
                {
                    iconChapter.transform.gameObject.GetComponent<Image>().sprite = backgroundChapter.Icon;
                    if (PlayerPrefs.HasKey("Level"))
                    {
                        int saveIndex = PlayerPrefs.GetInt("Level") - (6 * (PlayerPrefs.GetInt("Playable Chapter") - 1));
                        if (PlayerPrefs.HasKey("Index Anim Icon Chapter"))
                        {
                            if (saveIndex >= PlayerPrefs.GetInt("Index Anim Icon Chapter"))
                            {
                                iconChapter.transform.GetChild(0).GetComponent<Animation>().Play("AnimationIconBackground" + saveIndex);
                                if (saveIndex >= 6)
                                {
                                    PlayerPrefs.SetInt("Index Anim Icon Chapter", 1);
                                    PlayerPrefs.SetInt("Playable Chapter", PlayerPrefs.GetInt("Playable Chapter") + 1);
                                    float time = iconChapter.transform.GetChild(0).GetComponent<Animation>().GetClip("AnimationIconBackground6").length;
                                    StartCoroutine(WaitForSecondsActiveCompleteChapter(time));
                                    backgroundImageOfCompleteChapter.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = backgroundChapter.backgroundImageChapter;
                                }
                                else
                                {
                                    PlayerPrefs.SetInt("Index Anim Icon Chapter", saveIndex);
                                }
                            }
                            else
                            {
                                iconChapter.transform.GetChild(0).GetComponent<Animation>().Play("AnimationIconBackground" + PlayerPrefs.GetInt("Index Anim Icon Chapter"));
                            }
                        }
                    }
                }
                else
                {
                    iconChapter.SetActive(false);
                    StartCoroutine(WaitEndOfAnimationOfChapter(0));
                    //Debug.LogError("Don't find ScriptTableobject");                    
                }
                float index = iconChapter.transform.GetChild(0).GetComponent<Animation>().GetClip("AnimationIconBackground1").length;
                StartCoroutine(WaitEndOfAnimationOfChapter(index));
            }
            else
            {
                if (PlayerPrefs.GetInt("Level") % 6 == 0)
                {
                    PlayerPrefs.SetInt("Chapter", PlayerPrefs.GetInt("Chapter") + 1);
                }
                iconChapter.SetActive(false);
                StartCoroutine(WaitEndOfAnimationOfChapter(0));
            }
        }
    }

    private IEnumerator WaitEndOfAnimationOfChapter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }

    private IEnumerator WaitForSecondsActiveCompleteChapter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.transform.GetChild(4).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        backgroundImageOfCompleteChapter.SetActive(true);
    }
}
