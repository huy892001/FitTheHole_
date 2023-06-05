using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeCharacter : MonoBehaviour
{
    private bool checkConditionToComplete = false, checkConditionToRunOnly1Time = false;
    void Update()
    {
        checkConditionToComplete = CheckAllChildrenBlur();
        if (checkConditionToComplete && !checkConditionToRunOnly1Time)
        {
            GameManager.Instance.UpdateState(GameState.Win);
            checkConditionToRunOnly1Time = true;
        }
    }
    private bool CheckAllChildrenBlur()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject;
            float index = childObject.GetComponent<SpriteRenderer>().color.a;

            if (index == 0)
            {
                return false;
            }
        }
        return true;
    }
}
