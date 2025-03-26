using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrush;
    [SerializeField] InputAction rotate;
    [SerializeField] float thrushForce = 10f;
    [SerializeField] float rotateSpeed = 1f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (thrush.IsPressed()) rb.AddRelativeForce(Vector3.up * thrushForce);
    }

    private void ProcessRotate()
    {
        float rotateValue = rotate.ReadValue<float>();
        rb.transform.Rotate(0, 0, rotateValue * rotateSpeed); 
    }
}
