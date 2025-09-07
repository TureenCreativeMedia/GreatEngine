using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Handler : MonoBehaviour
{
    [SerializeField] GameUI GameUI;
    [SerializeField] Controls controls;

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (!CONSTANTS.BIRB_ALIVE) return;

        if (trigger.CompareTag("Death"))
        {
            CONSTANTS.BIRB_ALIVE = false;
            SoundManager.Instance.MakeSound("Death");
            SoundManager.Instance.MakeSound("SlowMusic");
            
            controls.SpinOut();

            var pPart = trigger.gameObject.GetComponent<PipePart>();
            if (pPart != null)
            {
                pPart.Bounce();
            }

            if (CONSTANTS.GAME_SCORE > CONSTANTS.GAME_HIGHSCORE)
            {
                PlayerPrefs.SetInt("Highscore", CONSTANTS.GAME_SCORE);
            }
        }

        if (trigger.CompareTag("Score"))
            {
                GameUI.AnimateScore();
                CONSTANTS.GAME_SCORE += 1;
                SoundManager.Instance.MakeSound("Score");
            }
    }
}
