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
        switch (AppSettings.DevelopmentBuildText.ToString())
        {
            default:
                {
                    Destroy(devBuildText);
                    break;
                }
            case "EditorOnly":
                {
#if UNITY_EDITOR
                    devBuildText.gameObject.SetActive(true);
#else
                    Destroy(devBuildText);
#endif

                    break;
                }
            case "DevelopmentBuildOnly":
                {
#if DEVELOPMENT_BUILD
                    devBuildText.gameObject.SetActive(true);
#else
                    Destroy(devBuildText);
#endif

                    break;
                }
            case "Both":
                {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    devBuildText.gameObject.SetActive(true);
#else
                    Destroy(devBuildText);
#endif

                    break;
                }
        }
    }

    void Update()
    {
        DeathScreen.SetActive(!CONSTANTS.BIRB_ALIVE && !CONSTANTS.GAME_PAUSED);

        // Score Text
        // Preform a check to make sure we aren't uselessly setting these
        if (!ScoreText.gameObject.activeInHierarchy) return;
        ScoreText.text = $"{CONSTANTS.GAME_SCORE}";

        // Death Screen
        if (!DeathScreen.activeInHierarchy) return;
        if (!DeathDetails.gameObject.activeInHierarchy) return;

#if !DEMO_MODE
        if (!(CONSTANTS.GAME_SCORE > CONSTANTS.GAME_HIGHSCORE))
        {
            DeathDetails.text = $"Score: {CONSTANTS.GAME_SCORE}\nTime: {CONSTANTS.GAME_TIME_CONVERTED}";
        }
        else
        {
            DeathDetails.text = $"<b>NEW HIGHSCORE!</b>\n\nScore: {CONSTANTS.GAME_SCORE}\nTime: {CONSTANTS.GAME_TIME_CONVERTED}";
        }
#else
        if (!(CONSTANTS.GAME_SCORE > CONSTANTS.GAME_HIGHSCORE))
        {
            DeathDetails.text = $"Score: {CONSTANTS.GAME_SCORE}\nTime: {CONSTANTS.GAME_TIME_CONVERTED}\nXP NOT IN DEMO";
        }
        else
        {
            DeathDetails.text = $"<b>NEW HIGHSCORE!</b>\n\nScore: {CONSTANTS.GAME_SCORE}\nTime: {CONSTANTS.GAME_TIME_CONVERTED}\nXP NOT IN DEMO";
        }
#endif
    }

    public void AnimateScore()
    {
        // Because this gets called everytime we enter a new pipe: Change the color here for safety
        if (CONSTANTS.GAME_SCORE > CONSTANTS.GAME_HIGHSCORE)
        {
            ScoreText.color = new Color32(255, 195, 0, 255);
        }
        else
        {
            ScoreText.color = new Color32(255, 255, 255, 255);
        }

        scoreTextAnimator.StopPlayback();

        // Check if we didn't just start the game so we don't get an auto animation
        if(CONSTANTS.GAME_TIME>2) scoreTextAnimator.Play("ScoreIncrease");
    }

}
