using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CONSTANTS
{
    // == PIPES == //
    public static float MINIMUM_PIPESPAWN_TIME = 3f;
    public static float MAXIMUM_PIPESPAWN_TIME = 5f;
    public static float PIPE_SPEED = 3f;
    public static float PIPE_GAP_SIZE = 4.5f;
    public static float PIPE_HEIGHT = 2f;

    // == GAME == //
    public static string GAME_TYPE = "default";
    public static int GAME_SCORE = 0;
    public static int GAME_HIGHSCORE = 0;
    public static int GAME_COINS = 0;

    public static bool GAME_PAUSED = false;

    public static float GAME_TIME = 0;
    public static string GAME_TIME_CONVERTED = "00:00";

    public static string BACKGROUND_COLOR;

    // == MUSIC == //
    public static GameObject MUSIC_OBJ;

    // == BIRB == //
    public static float BIRB_GRAV = 1.2f;
    public static GameObject BIRB_OBJ; 
    public static bool BIRB_ALIVE = true;
}

[DefaultExecutionOrder(0)]
public class CONSTANTS_Set : MonoBehaviour
{
    static Music music;
    static Controls controls;
    [SerializeField] Camera game_cam;

    void Awake()
    {
        if (CONSTANTS.MUSIC_OBJ == null) CONSTANTS.MUSIC_OBJ = GameObject.Find("Music");
        if (CONSTANTS.BIRB_OBJ == null) CONSTANTS.BIRB_OBJ = GameObject.FindGameObjectWithTag("Player");
        GameRestart();
    }

    public void Update()
    {
        if (!CONSTANTS.BIRB_ALIVE | CONSTANTS.GAME_PAUSED) return;

        CONSTANTS.GAME_TIME += Time.deltaTime;

        int minutes = (int)(CONSTANTS.GAME_TIME / 60);
        int seconds = (int)CONSTANTS.GAME_TIME - (minutes * 60);

        CONSTANTS.GAME_TIME_CONVERTED = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameRestart()
    {
        music = CONSTANTS.MUSIC_OBJ.GetComponent<Music>();
        controls = CONSTANTS.BIRB_OBJ.GetComponent<Controls>();

        if (controls == null || music == null) return;

        GamemodeChanges.ChangeGamemodeTypes();
        controls.SetVars();
        music.StopAllCoroutines();
        music.StartNewMusic();

        CONSTANTS.GAME_PAUSED = false;
        CONSTANTS.GAME_TIME = 0f;
        CONSTANTS.GAME_SCORE = 0;

        var rb = CONSTANTS.BIRB_OBJ.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.velocity = new Vector2(0, 0);

        CONSTANTS.BIRB_ALIVE = true;

        if (CONSTANTS.BIRB_OBJ != null)
        {
            CONSTANTS.BIRB_OBJ.transform.localPosition = new Vector2(-4, 0);
        }
        else
        {
            Debug.LogWarning("BIRB_OBJ is not set");
        }

        CONSTANTS.GAME_HIGHSCORE = PlayerPrefs.GetInt("Highscore", 0);

        if (Camera.main != null && ColorUtility.TryParseHtmlString("#" + CONSTANTS.BACKGROUND_COLOR, out Color bgColor))
        {
            Camera.main.backgroundColor = bgColor;
        }

        ClearPipes();
    }

    public static void Pause()
    {
        if (!CONSTANTS.BIRB_ALIVE) return;
        CONSTANTS.GAME_PAUSED = !CONSTANTS.GAME_PAUSED;

        Rigidbody2D rb = CONSTANTS.BIRB_OBJ.GetComponent<Rigidbody2D>();

        if (CONSTANTS.GAME_PAUSED)
        {
            controls.savedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;

            if (controls.savedVelocity == new Vector2(0, 0)) controls.savedVelocity = new Vector2(0, -2);
            print(controls.savedVelocity);
            rb.velocity = controls.savedVelocity;
        }
    }


    public void ClearPipes()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }

        GameObject spawnerObj = GameObject.Find("Pipe Spawner");
        if (spawnerObj != null)
        {
            PipeSpawner spawner = spawnerObj.GetComponent<PipeSpawner>();
            if (spawner != null)
            {
                spawner.ResetSpawner();
            }
        }
    }

}
