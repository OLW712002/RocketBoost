using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thrush")]
    [SerializeField] InputAction thrush;
    [SerializeField] float thrushForce = 10f;
    [SerializeField] AudioSource thrushSource;
    [SerializeField] ParticleSystem thrushParticle;

    [Header("Rotate")]
    [SerializeField] InputAction rotate;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] ParticleSystem leftSideParticle;
    [SerializeField] ParticleSystem rightSideParticle;

    Rigidbody rb;
    PlayerCollision playerCollision;

    bool isRotating = false;

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
            thrushSource.Stop();
            StopRotateParticle();
            thrushParticle.Stop();
        }
    }

    private void ProcessThrush()
    {
        if (thrush.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrushForce);
            if (thrushParticle != null && !thrushParticle.isPlaying) thrushParticle.Play();
            if (thrushSource != null && !thrushSource.isPlaying) thrushSource.Play();
        }
        else
        {
            thrushParticle.Stop();
            if ((thrushSource != null && thrushSource.isPlaying)) thrushSource.Stop();
        }
        
    }

    private void ProcessRotate()
    {
        float rotateValue = rotate.ReadValue<float>();
        if (rotateValue != 0) isRotating = true;
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateValue * rotateSpeed);
        rb.freezeRotation = false;
        ProcessRotateParticle(rotateValue);
    }

    private void ProcessRotateParticle(float rotateValue)
    {
        if (isRotating) switch (rotateValue)
            {
                case -1:
                    LeftSideParticle();
                    break;
                case 1:
                    RightSideParticle();
                    break;
                default:
                    StopRotateParticle();
                    break;
            }
    }

    private void LeftSideParticle()
    {
        if (leftSideParticle != null && !leftSideParticle.isPlaying)
        {
            leftSideParticle.Play();
            rightSideParticle.Stop();
        }
    }

    private void RightSideParticle()
    {
        if (rightSideParticle != null && !rightSideParticle.isPlaying)
        {
            rightSideParticle.Play();
            leftSideParticle.Stop();
        }
    }

    private void StopRotateParticle()
    {
        leftSideParticle.Stop();
        rightSideParticle.Stop();
        isRotating = false;
    }
}
