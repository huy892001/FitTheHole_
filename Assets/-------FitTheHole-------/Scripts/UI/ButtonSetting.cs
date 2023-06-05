using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetting : MonoBehaviour
{
    private bool setActiveButtonSetting = false;
    public void HandlerClichOnButtonSetting()
    {
        Animation component = GetComponentInParent<Animation>();
        if (component != null)
        {
            if (!setActiveButtonSetting)
            {
                component.Play(component.GetClip("AnimationDropdownUiSetting").name);
                setActiveButtonSetting = true;
            }
            else
            {
                component.Play(component.GetClip("AnimationDropdownUISettingOff").name);
                setActiveButtonSetting = false;
            }
        }
    }
}
