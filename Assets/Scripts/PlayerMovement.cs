using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thrush")]
    [SerializeField] InputAction thrush;
    [SerializeField] float thrushForce = 10f;
    [SerializeField] AudioClip thrushSFX;

    [Header("Rotate")]
    [SerializeField] InputAction rotate;
    [SerializeField] float rotateSpeed = 1f;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if (audioSource != null && !audioSource.isPlaying) audioSource.PlayOneShot(thrushSFX);
        }
        else
        {
            //if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
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
