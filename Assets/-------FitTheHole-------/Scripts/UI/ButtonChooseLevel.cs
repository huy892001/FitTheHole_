using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonChooseLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text textOfLevelNumber;
    private void Start()
    {

        if (PlayerPrefs.HasKey("Playable Level"))
        {
            if(int.Parse(textOfLevelNumber.text) <= PlayerPrefs.GetInt("Level"))
            {
                gameObject.transform.GetComponent<Button>().interactable = true;
            }
            else if(int.Parse(textOfLevelNumber.text) > PlayerPrefs.GetInt("Level"))
            {
                gameObject.transform.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            if (int.Parse(textOfLevelNumber.text) == 1)
            {
                gameObject.transform.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("Playable Level", 1);
            }
        }
    }
    public void HandlerChooseLevelFowardSceneGameplay(int chapter)
    {
        SoundManager.Instance.PlaySoundButton();
        PlayerPrefs.SetInt("Level", int.Parse(textOfLevelNumber.text));
        PlayerPrefs.SetInt("Chapter", chapter);
        SceneManager.LoadSceneAsync("Gameplay");
    }
}
