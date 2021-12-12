using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string message;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager._instance.SetAndShowToolTip(message);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TooltipManager._instance.HideToolTip();
    }

    public void HideToolTip()
    {
        TooltipManager._instance.HideToolTip();
    }
    public void setMessage(string msg)
    {
        message = msg;
    }
}
