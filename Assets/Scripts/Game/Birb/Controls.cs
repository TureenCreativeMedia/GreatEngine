using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    public InputActionReference jumpAction;
    public InputActionReference pauseAction;

    [SerializeField] private Rigidbody2D birb_rigidbody;
    [SerializeField] private float upthrust = 8f;

    public Vector2 savedVelocity;
    float rotationSpin;

    public void SetVars()
    {
        birb_rigidbody.gravityScale = CONSTANTS.BIRB_GRAV;
    }

    public void SpinOut()
    {
        rotationSpin = 450f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;

        if (!CONSTANTS.GAME_PAUSED)
        {
            savedVelocity = birb_rigidbody.velocity;
        }

        if (CONSTANTS.BIRB_ALIVE)
        {
            birb_rigidbody.rotation = 0;
            return;
        }
        if (rotationSpin > 0)
        {
            rotationSpin += 120 * Time.deltaTime;
            birb_rigidbody.rotation -= rotationSpin * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        jumpAction.action.performed += OnJump;
        jumpAction.action.Enable();

        pauseAction.action.performed += OnPause;
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        jumpAction.action.performed -= OnJump;
        jumpAction.action.Disable();

        pauseAction.action.performed += OnPause;
        pauseAction.action.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!CONSTANTS.BIRB_ALIVE) return;
        SoundManager.Instance.MakeSound("JumpSound");
        birb_rigidbody.velocity = Vector2.zero;
        birb_rigidbody.AddForce(Vector2.up * upthrust, ForceMode2D.Impulse);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        CONSTANTS_Set.Pause();
    }
}
