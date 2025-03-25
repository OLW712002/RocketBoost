using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrush;
    [SerializeField] float thrushForce = 10f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrush.Enable();
    }

    void FixedUpdate()
    {
        if (thrush.IsPressed()) rb.AddRelativeForce(Vector3.up * thrushForce);
    }
}
