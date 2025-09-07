using UnityEngine;

public class PipePart : MonoBehaviour
{
    [SerializeField]
    public enum PIPETYPE
    {
        TopPipe,
        BottomPipe
    }

    public PIPETYPE this_PipeType;

    bool isBounced = false;
    float bounceSpeed = 0f;
    float bounceDuration = 0.3f;
    float timer = 0f;
    float pipeDistance = 5;

    float rotationAmount = 10f;
    float rotationSpeed = 120f;
    float currentRotation = 0f;

    public void Bounce()
    {
        if (isBounced) return;

        isBounced = true;
        bounceSpeed = 5f;
        timer = 0f;

        rotationAmount = Random.value > 0.5f ? 10f : -10f;
        currentRotation = 0f;
    }

    void Update()
    {
        if (!isBounced)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, 0f, rotationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0, 0, currentRotation);
            return;
        }

        timer += Time.deltaTime;
        transform.localPosition += Vector3.right * bounceSpeed * Time.deltaTime;

        float rotationStep = rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.MoveTowards(currentRotation, rotationAmount, rotationStep);
        transform.localRotation = Quaternion.Euler(0, 0, currentRotation);

        bounceSpeed = Mathf.MoveTowards(bounceSpeed, 0f, 10f * Time.deltaTime);

        if (timer > bounceDuration)
        {
            isBounced = false;
            bounceSpeed = 0f;
        }
    }

    void Start()
    {
        pipeDistance = CONSTANTS.PIPE_GAP_SIZE;
        if (this_PipeType == PIPETYPE.TopPipe)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, pipeDistance);
        } 
        if (this_PipeType == PIPETYPE.BottomPipe)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, -pipeDistance);
        } 
    }
}
