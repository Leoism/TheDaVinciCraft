using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyMenuToolTip : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string description;
    // Start is called before the first frame update
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        BuyMenuTooltipManager._instance.SetAndShowToolTip(description);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        BuyMenuTooltipManager._instance.HideToolTip();
    }
}
