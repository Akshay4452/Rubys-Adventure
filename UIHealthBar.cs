using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //Static members are shared by all objects of that type
    public static UIHealthBar instance {get; private set; }
    // set property is private so other scripts cant edit it
    public Image mask;
    float originalSize;
    void Awake()
    {
        // your Health bar script get its Awake function call and stores itself in the static member called “instance”. 
        // So, if in any other script you call UIHealthBar.instance, then the value it will return to that script is the Health bar in our Scene.
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
