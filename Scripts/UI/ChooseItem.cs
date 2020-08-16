using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItem : MonoBehaviour
{
    private RectTransform rectTransform;
    public Text content;
    private EM_SignChooseType sign = EM_SignChooseType.None;
    private Action<EM_SignChooseType,int> onChooseClick;
    private Button btn;
    private int id=0;
    private int functionType = 0;
    public void InitContent(EM_SignChooseType type,Action<EM_SignChooseType,int> cb)
    {
        sign = type;
        onChooseClick = cb;
        btn = GetComponent<Button>();
        if (!btn)
            btn = gameObject.AddComponent<Button>();
        btn.onClick.AddListener(() => {
            //通过id从表中读取具体的类型
            onChooseClick(sign, functionType);
        });
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform)
        {
            float height = this.content.flexibleHeight+SysDefine.Instance.scaleHeight;
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, height);
        }
    }

    public void UpdateContent(string content)
    {
        if (this.content)
        {
            this.content.text = content;
        }
    }

    public void SetFunctionType(int type)
    {
        functionType = type;
    }
}
