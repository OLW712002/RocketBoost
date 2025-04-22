using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thrush")]
    [SerializeField] InputAction thrush;
    [SerializeField] float thrushForce = 10f;

    [Header("Rotate")]
    [SerializeField] InputAction rotate;
    [SerializeField] float rotateSpeed = 1f;

    Rigidbody rb;
    AudioSource thrushSFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrushSFX = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrush.Enable();
        rotate.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrush();
        ProcessRotate();
    }

    private void ProcessThrush()
    {
        if (thrush.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrushForce);
            if (thrushSFX != null && !thrushSFX.isPlaying) thrushSFX.Play();
        }
        else
        {
            if (thrushSFX != null && thrushSFX.isPlaying) thrushSFX.Stop();
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
