using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevText : MonoBehaviour
{
    // Build text

    [SerializeField] GameUI referenceUI;

    void Update()
    {
        if (referenceUI.devBuildText != null)
        {
            referenceUI.devBuildText.text = $"DEVELOPMENT BUILD - GREAT ENGINE v{AppSettings.appVersion}";
        }
    }
}
