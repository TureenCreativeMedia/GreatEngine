using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [SerializeField] GameObject TopPipe;
    [SerializeField] GameObject BottomPipe;

    void Start()
    {
        this.tag = "Pipe";

        if (BottomPipe == null || TopPipe == null)
        {
            Debug.LogWarning("One or two pipes are null, attempting reformation");

            try
            {
                if (TopPipe == null) TopPipe = transform.Find("Top Pipe")?.gameObject;
                if (BottomPipe == null) BottomPipe = transform.Find("Bottom Pipe")?.gameObject;
            }
            catch
            {
                Debug.LogError("Could not reform either pipe due to null error");
            }
        }

        TopPipe.transform.localPosition = new Vector2(0, CONSTANTS.PIPE_GAP_SIZE);
        BottomPipe.transform.localPosition = new Vector2(0, -CONSTANTS.PIPE_GAP_SIZE);
    }

    void Update()
    {
        moveSpeed = CONSTANTS.GAME_PAUSED ? 0 : CONSTANTS.PIPE_SPEED;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}
