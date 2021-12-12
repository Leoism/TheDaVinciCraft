using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuyMenuTooltipManager : MonoBehaviour
{
    public static BuyMenuTooltipManager _instance;
    public TextMeshProUGUI textComponent;

    public float lerpTime;
    public float timeStartedLerping;

    public Vector2 start;
    public Vector2 end;
    bool shouldLerpUp = false;
    bool shouldLerpDown = false;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        //gameObject.SetActive(false);
    }

    private void StartLerpingUp()
    {
        timeStartedLerping = Time.time;
        shouldLerpUp = true;
    }

    private void StartLerpingDown()
    {
        timeStartedLerping = Time.time;
        shouldLerpDown = true;
    }
    private void Update()
    {
        if (shouldLerpUp)
        {
            transform.position = Lerp(start, end, timeStartedLerping, lerpTime);
            if (transform.position.x == end.x && transform.position.y == end.y)
            {
                shouldLerpUp = false;
            }
        } else if (shouldLerpDown)
        {
            transform.position = Lerp(end, start, timeStartedLerping, lerpTime);
            if (transform.position.x == start.x && transform.position.y == start.y)
            {
                shouldLerpDown = false;
            }
        }
    }
    public void SetAndShowToolTip(string message)
    {
        if (message == "" || message == null)
            return;
        gameObject.SetActive(true);
        transform.position = new Vector2(0, -160);
        // gameObject.SetActive(true);
        textComponent.text = message.Replace("<br>", "\n");
        textComponent.ForceMeshUpdate();
        StartLerpingUp();
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        transform.position = start;
        textComponent.text = string.Empty;
        //StartLerpingDown();
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var result = Vector3.Lerp(start, end, percentageComplete);
        return result;
    }

}
