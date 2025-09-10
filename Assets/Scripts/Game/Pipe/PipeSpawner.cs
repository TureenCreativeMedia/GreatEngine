using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public List<GameObject> Pipes;
    [SerializeField] GameObject pipePrefab;

    [SerializeField] float pipeSpawnTimer = 0f;
    [SerializeField] float newPipeTime = 2f;

    public void ResetSpawner()
    {
        // Spawn a pipe after newPipeTime which is by default 2
        pipeSpawnTimer = 0;
        newPipeTime = 2;
    }

    void Update()
    {
        if (CONSTANTS.GAME_PAUSED)
            return;
        
        if (pipeSpawnTimer >= newPipeTime)
        {
            MakeNewPipe();
        }

        pipeSpawnTimer += Time.deltaTime;
    }


    private void MakeNewPipe()
    {
        pipeSpawnTimer = 0f;

        newPipeTime = Random.Range(CONFIG.MIN_PIPE_SPAWN_TIME, CONFIG.MAX_PIPE_SPAWN_TIME);
        float pipeHeight = Random.Range(-CONFIG.PIPE_HEIGHT, CONFIG.PIPE_HEIGHT);
        var newPipe = Instantiate(pipePrefab, new Vector2(13, pipeHeight), Quaternion.identity);
        Pipes.Add(newPipe); // Add the instanciated prefab of the pipe we just referenced to an array which is used to clear them
    }
}
