using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "BackgroundChapter")]
public class IconOfBackgroundChapter : ScriptableObject
{
    [HorizontalGroup("Background Chapter", 75)]
    [PreviewField(75)]
    [HideLabel]
    public Sprite Icon;
    [VerticalGroup("Background Chapter/Chapter")]
    [LabelWidth(100)]
    public string nameOfChapter;
}


