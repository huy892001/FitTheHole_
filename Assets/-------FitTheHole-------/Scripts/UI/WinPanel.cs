using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject iconChapter;
    private void OnEnable()
    {
        PlayerPrefs.SetInt("Playable Level",(PlayerPrefs.GetInt("Playable Level") +1));
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
                    Debug.LogError("Don't find ScriptTableobject");
                }
                float index = iconChapter.transform.GetChild(0).GetComponent<Animation>().GetClip("AnimationIconBackground1").length;
                StartCoroutine(WaitEndOfAnimationOfChapter(index));
            }
            else
            {
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
}
