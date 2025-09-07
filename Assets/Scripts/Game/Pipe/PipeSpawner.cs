using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipePrefab;

    [SerializeField] float pipeSpawnTimer = 0f;
    [SerializeField] float newPipeTime = 0f;

    public void ResetSpawner()
    {
        pipeSpawnTimer = 99;
        newPipeTime = 3f;
    }

    void Update()
    {
        if (!CONSTANTS.BIRB_ALIVE | CONSTANTS.GAME_PAUSED)
            return;

        pipeSpawnTimer += Time.deltaTime;

        if (pipeSpawnTimer > newPipeTime)
        {
            MakeNewPipe();
        }
    }


    private void MakeNewPipe()
    {
        pipeSpawnTimer = 0f;
        newPipeTime = Random.Range(CONSTANTS.MINIMUM_PIPESPAWN_TIME, CONSTANTS.MAXIMUM_PIPESPAWN_TIME);
        float pipeHeight = Random.Range(-CONSTANTS.PIPE_HEIGHT, CONSTANTS.PIPE_HEIGHT);
        Instantiate(pipePrefab, new Vector2(13, pipeHeight), Quaternion.identity);
    }
}
