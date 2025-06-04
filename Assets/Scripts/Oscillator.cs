using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Transform StartPosition;
    [SerializeField] Transform EndPosition;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(StartPosition.position,EndPosition.position, Mathf.PingPong(Time.time * moveSpeed, 1));
    }
}
