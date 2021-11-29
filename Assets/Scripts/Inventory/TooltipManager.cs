using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
   
    public static TooltipManager _instance;
    public TextMeshProUGUI textComponent;

    [SerializeField]
    private RectTransform canvasRectTransform;

    private RectTransform backgroundRectTransform;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
        backgroundRectTransform = transform.GetComponent<RectTransform>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition;

        // Tooltip left screen right side
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        transform.position = anchoredPosition;

    }

    public void SetAndShowToolTip(string message)
    {

        if (message == "" || message == null)
            return;

        gameObject.SetActive(true);
        textComponent.text = message;
        textComponent.ForceMeshUpdate();

        Vector2 textSize = textComponent.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;

    }

    public void renderToolTipSize(string message)
    {
        textComponent.ForceMeshUpdate();

        Vector2 textSize = textComponent.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;

    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
