using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] Outline outline;

    public void SetOutlineColor(string hexColor)
    {
        Color newColor;

        ColorUtility.TryParseHtmlString(hexColor, out newColor);
        outline.effectColor = newColor;
    }

    public void SetOutlineThickness(float changeThickness)
    {
        outline.effectDistance = new Vector2(changeThickness, changeThickness);
    }
}
