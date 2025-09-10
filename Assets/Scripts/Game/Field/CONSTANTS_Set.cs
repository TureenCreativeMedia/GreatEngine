using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public static class CONFIG
{
    public static float PIPE_SCORE_COLLISION_WIDTH = 0.45f;
    public static float MIN_PIPE_SPAWN_TIME = 3f;
    public static float MAX_PIPE_SPAWN_TIME = 5f;
    public static float PIPE_SPEED = 3f;
    public static float PIPE_GAP_SIZE = 5f;
    public static float PIPE_HEIGHT = 2f;
}


public static class CONSTANTS
{
    // == GAME == //
    public static string GAME_TYPE = "default";
    public static int GAME_SCORE = 0;
    public static int GAME_HIGHSCORE = 0;

    public static bool GAME_PAUSED = false;

    public static float GAME_TIME = 0;
    public static string GAME_TIME_CONVERTED = "00:00";

    public static string BACKGROUND_COLOR;

    public static GameObject PIPE_SPAWNER;

    // == MUSIC == //
    public static GameObject MUSIC_OBJ;

    // == BIRB == //
    public static GameObject BIRB_OBJ;
    public static Rigidbody2D BIRB_RB;
    public static float BIRB_GRAV = 1.2f;
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
        // Null checks and such - to make sure no errors occur whilst running
        // But we preform this risky code with try-catch to StackTrace and can debug easily

        try
        {
            if (CONSTANTS.PIPE_SPAWNER == null)
                CONSTANTS.PIPE_SPAWNER = GameObject.Find("Pipe Spawner");

            if (CONSTANTS.MUSIC_OBJ == null)
                CONSTANTS.MUSIC_OBJ = GameObject.Find("Music");

            if (CONSTANTS.BIRB_OBJ == null)
                CONSTANTS.BIRB_OBJ = GameObject.FindGameObjectWithTag("Player");

            if (CONSTANTS.BIRB_OBJ != null && CONSTANTS.BIRB_RB == null)
                CONSTANTS.BIRB_RB = CONSTANTS.BIRB_OBJ.GetComponent<Rigidbody2D>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error when preforming null checks: {ex.Message} at {ex.StackTrace}");
        }

        GameRestart();
    }

    public void Update()
    {
        if (CONSTANTS.GAME_PAUSED || !CONSTANTS.BIRB_ALIVE) return;
        CONSTANTS.GAME_TIME += Time.deltaTime;

        // We use TimeSpan for ease over the GAME_TIME, instead of using the old method of 
        // minutes = CONSTANTS.GAME_TIME / 60
        // and seconds = CONSTANTS.GAME_TIME - (minutes * 60)

        TimeSpan timeSpan = TimeSpan.FromSeconds(CONSTANTS.GAME_TIME);
        CONSTANTS.GAME_TIME_CONVERTED = TimeConversion(timeSpan);
    }

    private string TimeConversion(TimeSpan timeSpan)
    {
        if (timeSpan.Hours > 0)
        {
            // We check for hours beforehand to make sure useless conversions don't happen:
            return string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            return string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }     
    }


    public void GameRestart()
    {
        music = CONSTANTS.MUSIC_OBJ.GetComponent<Music>();
        controls = CONSTANTS.BIRB_OBJ.GetComponent<Controls>();

        // If the Music or (then) Controls aren't found, don't continue
        if (controls == null || music == null) return;


        #region Main Stack
        CONSTANTS.GAME_HIGHSCORE = PlayerPrefs.GetInt("Highscore", 0);
        GamemodeChanges.ChangeGamemodeTypes();
        controls.SetVars();
        music.StopAllCoroutines();  // Stop attempting to slow or speed up music
        music.StartNewMusic();      // Then begin to speed up music in this function if it isn't at pitch value 1

        CONSTANTS.GAME_PAUSED = false;
        CONSTANTS.GAME_TIME = 0f;
        CONSTANTS.GAME_SCORE = 0;
        CONSTANTS.BIRB_RB.isKinematic = false; // Start calculating the Rigidbody physics again
        CONSTANTS.BIRB_RB.velocity = new Vector2(0, 0);
        CONSTANTS.BIRB_ALIVE = true;
        #endregion

        if (CONSTANTS.BIRB_OBJ != null)
        {
            CONSTANTS.BIRB_OBJ.transform.localPosition = new Vector2(-4, 0);
        }
        else
        {
            Debug.LogWarning("BIRB_OBJ is not set");
        }

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

        if (CONSTANTS.GAME_PAUSED)
        {
            controls.savedVelocity = CONSTANTS.BIRB_RB.velocity;
            CONSTANTS.BIRB_RB.velocity = Vector2.zero;
            CONSTANTS.BIRB_RB.isKinematic = true;
        }
        else
        {
            CONSTANTS.BIRB_RB.isKinematic = false; 

            // If our savedVelocity when we Unpause the game is (0,0) we should attempt to make it (0,-2)
            if (controls.savedVelocity == Vector2.zero) controls.savedVelocity = new Vector2(0, -2);

            // This is because our velocity is fed savedVelocity when we unpause
            // Meaning if it was (0,0) on Unpause, we wouldn't change our velocity until we pressed anything
            // As it continues to be overridden in Update() for Controls
            CONSTANTS.BIRB_RB.velocity = controls.savedVelocity;
        }
    }

    public void ClearPipes()
    {
        // Check if PIPE_SPAWNER is null, return if true
        if (CONSTANTS.PIPE_SPAWNER == null)
        {
            Debug.LogError($"CONSTANTS.PIPE_SPAWNER is null for ClearPipes");
            return;
        }

        PipeSpawner pipeSpawner = CONSTANTS.PIPE_SPAWNER.GetComponent<PipeSpawner>();

        // Check if the SCRIPT PipeSpawner is null on the object, return if true
        if (pipeSpawner == null)
        {
            Debug.LogError("PipeSpawner component not found on PIPE_SPAWNER GameObject.");
            return;
        }

        // If we pass these without problems: Destroy each pipe in pipeSpawner that gets added
        foreach (GameObject pipe in pipeSpawner.Pipes)
        {
            Destroy(pipe);
        }

        pipeSpawner.Pipes.Clear();  // Clear the GameObject list for Pipes
        pipeSpawner.ResetSpawner(); 
    }
}