using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS_Display : MonoBehaviour
{
    TMP_Text FPS_Text;

    float refreshRate = 1f;
    float timer;
    int frameCount;

    void Start()
    {
        FPS_Text = transform.Find("FPS_Text_Child").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("showFPS") == 0)
        {
            FPS_Text.gameObject.SetActive(false);
            return;
        }

        FPS_Text.gameObject.SetActive(true);

        frameCount++;
        timer += Time.unscaledDeltaTime;

        if (timer >= refreshRate)
        {
            int fps = Mathf.RoundToInt(frameCount / timer);
            FPS_Text.text = $"FPS: {fps}";
            frameCount = 0;
            timer = 0f;
        }
    }
}
