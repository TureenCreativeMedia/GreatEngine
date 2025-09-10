using UnityEngine;

public class Pipe : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] float moveSpeed;

    [SerializeField] GameObject TopPipe;
    [SerializeField] GameObject BottomPipe;

    private void Awake()
    {
        _transform = transform;
        gameObject.tag = "Pipe";
    }

    void Start()
    {
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

        float gap = CONFIG.PIPE_GAP_SIZE;
        TopPipe.transform.localPosition = new Vector2(0f, gap);
        BottomPipe.transform.localPosition = new Vector2(0f, -gap);
    }

    void Update()
    {
        moveSpeed = CONSTANTS.GAME_PAUSED ? 0 : CONFIG.PIPE_SPEED;
        _transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (_transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}
