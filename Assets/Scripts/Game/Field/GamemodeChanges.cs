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
                    CONSTANTS.BIRB_GRAV = 1.2f;
                    CONSTANTS.BACKGROUND_COLOR = "36A0E2";
                    CONSTANTS.PIPE_HEIGHT = 1f;
                    CONSTANTS.MAXIMUM_PIPESPAWN_TIME = 5f;
                    CONSTANTS.MINIMUM_PIPESPAWN_TIME = 3f;
                    CONSTANTS.PIPE_GAP_SIZE = 4f;
                    CONSTANTS.PIPE_SPEED = 3f;
                    break;
            }
    }
}
