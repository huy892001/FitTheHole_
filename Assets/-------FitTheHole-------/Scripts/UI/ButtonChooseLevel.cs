using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonChooseLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text textOfLevelNumber;
    public void HandlerChooseLevelFowardSceneGameplay(int chapter)
    {
        SoundManager.Instance.PlaySoundButton();
        PlayerPrefs.SetInt("Level", int.Parse(textOfLevelNumber.text));
        PlayerPrefs.SetInt("Chapter", chapter);
        SceneManager.LoadSceneAsync("Gameplay");
    }
}
