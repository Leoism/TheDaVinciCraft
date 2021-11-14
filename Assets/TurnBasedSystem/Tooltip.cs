using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;

/*    private void OnMouseEnter()
    {
        TooltipManager._instance.SetAndShowToolTip(message);
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.HideToolTip();
    }*/
  

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager._instance.SetAndShowToolTip(message);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TooltipManager._instance.HideToolTip();
    }
}
