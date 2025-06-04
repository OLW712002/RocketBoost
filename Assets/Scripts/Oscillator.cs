using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Vector3 movementVector;

    Vector3 StartPosition;
    Vector3 EndPosition;
    float movementFactor;
    

    void Start()
    {
        StartPosition = transform.position;
        EndPosition = transform.position + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * moveSpeed, 1);
        transform.position = Vector3.Lerp(StartPosition, EndPosition, movementFactor);
    }
}
