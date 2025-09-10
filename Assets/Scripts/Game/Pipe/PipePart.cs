using UnityEngine;

public class PipePart : MonoBehaviour
{
    public enum PIPETYPE
    {
        TopPipe,
        BottomPipe
    }

    [SerializeField] private PIPETYPE pipeType;

    private bool isBounced = false;
    private float bounceSpeed = 0f;
    private float bounceDuration = 0.3f;
    private float timer = 0f;

    private float pipeDistance = 5f;

    private float rotationAmount = 10f;
    private float rotationSpeed = 120f;
    private float currentRotation = 0f;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        pipeDistance = CONFIG.PIPE_GAP_SIZE;

        float yOffset = (pipeType == PIPETYPE.TopPipe) ? pipeDistance : -pipeDistance;
        
        Vector3 pos = _transform.localPosition;
        pos.y = yOffset;
        _transform.localPosition = pos;
    }

    public void Bounce()
    {
        if (isBounced) return;

        isBounced = true;
        bounceSpeed = 5f;
        timer = 0f;
        rotationAmount = (Random.value > 0.5f) ? 10f : -10f;
        currentRotation = 0f;
    }

    private void Update()
    {
        if (!isBounced)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, 0f, rotationSpeed * Time.deltaTime);
            _transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
            return;
        }

        timer += Time.deltaTime;
        _transform.localPosition += Vector3.right * bounceSpeed * Time.deltaTime;

        float rotationStep = rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.MoveTowards(currentRotation, rotationAmount, rotationStep);
        _transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);

        bounceSpeed = Mathf.MoveTowards(bounceSpeed, 0f, 10f * Time.deltaTime);

        if (timer >= bounceDuration)
        {
            isBounced = false;
            bounceSpeed = 0f;
        }
    }
}
