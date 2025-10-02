using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamemodeChanges
{
    public static void ChangeGamemodeTypes()
    {
        switch (CONSTANTS.GAME_TYPE)
        {
            default:
                {
                    CONSTANTS.BIRB_GRAV = 1.2f;
                    CONSTANTS.BACKGROUND_COLOR = "36A0E2";
                    CONFIG.PIPE_HEIGHT = 1f;
                    CONFIG.MAX_PIPE_SPAWN_TIME = 5f;
                    CONFIG.MIN_PIPE_SPAWN_TIME = 3f;
                    CONFIG.PIPE_GAP_SIZE = 4.5f;
                    CONFIG.PIPE_SPEED = 3f;
                    break;
                }
            case "hard":
                {
                    CONSTANTS.BIRB_GRAV = 1.2f;
                    CONSTANTS.BACKGROUND_COLOR = "355555";
                    CONFIG.PIPE_HEIGHT = 0.9f;
                    CONFIG.MAX_PIPE_SPAWN_TIME = 4f;
                    CONFIG.MIN_PIPE_SPAWN_TIME = 2.65f;
                    CONFIG.PIPE_GAP_SIZE = 4.25f;
                    CONFIG.PIPE_SPEED = 4f;
                    break;
                }
            }
    }
}
