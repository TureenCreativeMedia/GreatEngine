using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUI : MonoBehaviour
{
    [SerializeField] GameObject PauseUIElement;
    [SerializeField] TMP_Text TimeText;

    void Update()
    {
        if (PauseUIElement.activeInHierarchy != CONSTANTS.GAME_PAUSED) PauseUIElement.SetActive(CONSTANTS.GAME_PAUSED);

        if (!CONSTANTS.GAME_PAUSED) return;

        TimeText.text = CONSTANTS.GAME_TIME_CONVERTED;
    }
}
