using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thrush")]
    [SerializeField] InputAction thrush;
    [SerializeField] float thrushForce = 10f;
    [SerializeField] AudioSource thrushSource;

    [Header("Rotate")]
    [SerializeField] InputAction rotate;
    [SerializeField] float rotateSpeed = 1f;

    Rigidbody rb;
    PlayerCollision playerCollision;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollision = GetComponent<PlayerCollision>();
    }

    private void OnEnable()
    {
        thrush.Enable();
        rotate.Enable();
    }

    void FixedUpdate()
    {
        if (playerCollision.CheckControlable())
        {
            ProcessThrush();
            ProcessRotate();
        }
        else if (!playerCollision.CheckControlable())
        {
            Debug.Log("mute");
            thrushSource.Stop();
        }
    }

    private void ProcessThrush()
    {
        if (thrush.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrushForce);
            if (thrushSource != null && !thrushSource.isPlaying) thrushSource.Play();
        }
        else
        {
            if ((thrushSource != null && thrushSource.isPlaying)) thrushSource.Stop();
        }
        
    }

    private void ProcessRotate()
    {
        float rotateValue = rotate.ReadValue<float>();
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateValue * rotateSpeed);
        rb.freezeRotation = false;
    }
}
