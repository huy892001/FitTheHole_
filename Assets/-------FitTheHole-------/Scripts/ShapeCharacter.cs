using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCharacter : MonoBehaviour
{
    private bool checkConditionToComplete = false, checkConditionToRunOnlyTime = false;
    [SerializeField] private List<MyCharacter> listOfShapeCharacter = new List<MyCharacter>();
    void Update()
    {
        checkConditionToComplete = CheckAllChildrenBlur();
        if (checkConditionToComplete && !checkConditionToRunOnlyTime)
        {
            GameManager.Instance.UpdateState(GameState.Win);
            checkConditionToRunOnlyTime = true;
        }
    }
    private bool CheckAllChildrenBlur()
    {
        for (int i = 0; i < listOfShapeCharacter.Count; i++)
        {
            //GameObject childObject = transform.GetChild(i).gameObject;
            float index = listOfShapeCharacter[i].shapeOfCharacter.transform.GetComponent<SpriteRenderer>().color.a;

            if (index == 0)
            {
                return (false);
            }
        }
        return true;

    }
    [Serializable]
    public class MyCharacter
    {
        [PreviewField(Height = 20)]
        [TableColumnWidth(30, Resizable = false)]
        public Texture2D Icon;

        [TableColumnWidth(60)]
        public string nameOfCharacter;

        public GameObject shapeOfCharacter;

    }
}
