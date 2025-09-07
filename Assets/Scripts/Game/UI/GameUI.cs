using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject DeathScreen;

    public TMP_Text devBuildText;

    [SerializeField] TMP_Text ScoreText;
    [SerializeField] Animator scoreTextAnimator;

    [SerializeField] TMP_Text DeathDetails;


    void Awake()
    {
        devBuildText.gameObject.SetActive(false);
        #if DEVELOPMENT_BUILD || UNITY_EDITOR
            devBuildText.gameObject.SetActive(true);
        #endif
    }

    void Update()
    {
        #region NORMAL UI
        ScoreText.text = $"{CONSTANTS.GAME_SCORE}";
            if (CONSTANTS.GAME_SCORE > CONSTANTS.GAME_HIGHSCORE)
            {
                // COLOR YELLOW
                ScoreText.color = new Color32(255, 195, 0, 255);
            }
            else
            {
                // COLOR WHITE
                ScoreText.color = new Color32(255, 255, 255, 255);
            }
        #endregion

        #region DEATH SCREEN
        DeathScreen.SetActive(!CONSTANTS.BIRB_ALIVE && !CONSTANTS.GAME_PAUSED);
            if (!DeathDetails.gameObject.activeInHierarchy) return;

            if (!(CONSTANTS.GAME_SCORE > CONSTANTS.GAME_HIGHSCORE))
            {
                DeathDetails.text = $"Score: {CONSTANTS.GAME_SCORE}\nTime: {CONSTANTS.GAME_TIME_CONVERTED}";
            }
            else
            {
                DeathDetails.text = $"NEW HIGHSCORE!\n\nSCORE: {CONSTANTS.GAME_SCORE}\nTime: {CONSTANTS.GAME_TIME_CONVERTED}";
            }
        #endregion
    }

    public void AnimateScore()
    {
        scoreTextAnimator.StopPlayback();
        scoreTextAnimator.Play("ScoreIncrease");
    }

}
